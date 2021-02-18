using File_To_DB_Parser.Models;
using File_To_DB_Parser.Models.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace File_To_DB_Parser.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            try
            {
                if (file == null)
                {
                    return Json(new { status = "error",message="Fayl tapılmadı.Zəhmət olmasa fayl yüklyəin!"});
                }
                using (var context = new AppDbContext())
                {

                    var transactions = new List<Transaction>();
                    var columnBorders = new List<int>();

                    Transaction transaction = null;

                    //read the file
                    var fileLines = new List<string>();
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(file.InputStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            fileLines.Add(reader.ReadLine());
                        }
                    }


                    for (int i = 0; i < fileLines.Count; i++)
                    {
                        //removal of spaces at the beginning and at the end of the line
                        fileLines[i] = fileLines[i].Trim();

                        //parse for the transaction header and adding a row to the database
                        if (fileLines[i].StartsWith("Financial Institution"))
                        {
                            transaction = null;
                            transaction = new Transaction()
                            {
                                CreateDatetime = DateTime.Now,
                                FinancialInstitution = fileLines[i].ToString().Split(':')[1].Trim(),
                                FXSettlementDate = fileLines[i + 1].ToString().Split(':')[1].Trim(),
                                ReconciliationFileID = fileLines[i + 2].ToString().Split(':')[1].Trim(),
                                TransactionCurrency = fileLines[i + 3].ToString().Split(':')[1].Trim(),
                                ReconciliationCurrency = fileLines[i + 4].ToString().Split(':')[1].Trim()
                            };
                            context.Transactions.Add(transaction);
                            context.SaveChanges();

                            i += 4;
                        }
                        //finding column indixes for transaction lines
                        else if (fileLines[i].StartsWith("+") && columnBorders.Count == 0)
                        {
                            columnBorders = Utils.StringHelpers.AllIndexesOf(fileLines[i].Trim(), "+");
                        }
                        //finding transaction lines and adding to the database
                        else if (fileLines[i].StartsWith("!") && columnBorders.Count > 0 && fileLines[i].Count(c => c == '!') != columnBorders.Count && transaction != null)
                        {
                            List<string> lineColumns = Utils.StringHelpers.SplitByIndexes(fileLines[i].Trim(), columnBorders.ToArray());

                            var transactionLine = new TransactionLine()
                            {
                                CreateDatetime = DateTime.Now,
                                SettlementCategory = lineColumns[0].Trim(),
                                TransactionAmountCredit = double.Parse(lineColumns[1]),
                                ReconciliationAmnt = double.Parse(lineColumns[2]),
                                FeeAmountCredit = double.Parse(lineColumns[3]),
                                TransactionAmountDebit = double.Parse(lineColumns[4]),
                                ReconciliationAmntDebit = double.Parse(lineColumns[5]),
                                FeeAmountDebit = double.Parse(lineColumns[6]),
                                CountTotal = double.Parse(lineColumns[7]),
                                NetValue = double.Parse(lineColumns[8]),
                                Transaction = transaction
                            };

                            transaction.TransactionLines.Add(transactionLine);

                            context.SaveChanges();
                        }
                        else
                        {
                            continue;
                        }
                    }

                    return RedirectToAction("TransactionsPartial");
                }
            }
            catch (Exception)
            {

                return Json(new { status = "error", message = "Məlumat uğurla yüklənə bilmədi!" });
            }
           
        }


        public ActionResult TransactionsPartial()
        {     
            return PartialView();
        }

        public object GetTrasactionData()
        {
            using (AppDbContext context = new AppDbContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                context.Configuration.LazyLoadingEnabled = true;

                var list = context.Transactions.ToList();

                return Json(new { data = list, status = "success", message = "Məlumat yükləndi!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TransactionDetailsPartial()
        {
            return PartialView();
        }


        public ActionResult GetTrasactionDetailsData(int id)
        {
            
            using (AppDbContext context = new AppDbContext())
            {
                context.Configuration.ProxyCreationEnabled = false;
                context.Configuration.LazyLoadingEnabled = true;

                var list = context.TransactionLines.Where(x=>x.TransactionID==id).ToList();
                if (list == null)
                {
                    return Json(new { status = "error", message = "Məlumat tapılmadı!" });
                }
                return Json(new { data = list}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}