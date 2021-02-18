using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_To_DB_Parser.Models.Entity
{
    public class Transaction:EntityBase
    {
        public string FinancialInstitution { get; set; }
        public string FXSettlementDate { get; set; }
        public string ReconciliationFileID { get; set; }
        public string TransactionCurrency { get; set; }
        public string ReconciliationCurrency { get; set; }

        public virtual ICollection<TransactionLine> TransactionLines { get; set; }


        public Transaction()
        {
            TransactionLines = new List<TransactionLine>();
        }
    }
}