using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC
{
    class Saving
    {
        private DatabaseConn _savingsConn;
        public Saving()
        {
            _savingsConn=new DatabaseConn();            
        }

        public void Deposit(int memberid, float amount)
        {
            Transaction(memberid,amount,0);
        }

        public void Withdraw(int memberid, float amount)
        {
            Transaction(memberid,amount,1);
        }

        public void Transaction(int memberid, float amount,int tae)
        {
            var asd = _savingsConn.Select("savings", "savings_account_id").Where("member_id", memberid.ToString()).GetQueryData().Rows[0][0].ToString();
            _savingsConn.Insert("savings_transaction", "savings_account_id", asd, "transaction_type", tae.ToString(),
                "date", DateTime.Now.ToString(), "total_amount", amount.ToString());//, "encoded_by", higher.user);
        }

        public DataTable GetSavings(int memberid)
        {
            return _savingsConn.Select("savings", "*").Where("member_id", memberid.ToString()).GetQueryData();
        }
    }
}
