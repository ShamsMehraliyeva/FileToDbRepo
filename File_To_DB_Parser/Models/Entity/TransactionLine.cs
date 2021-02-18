using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_To_DB_Parser.Models.Entity
{
    public class TransactionLine:EntityBase
    {
        public string SettlementCategory { get; set; }
        public double TransactionAmountCredit { get; set; }
        public double ReconciliationAmnt { get; set; }
        public double FeeAmountCredit { get; set; }
        public double TransactionAmountDebit { get; set; }
        public double ReconciliationAmntDebit { get; set; }
        public double FeeAmountDebit { get; set; }
        public double CountTotal { get; set; }
        public double NetValue { get; set; }
  
        public int TransactionID { get; set; }
        public  Transaction Transaction { get; set; }
    }
}