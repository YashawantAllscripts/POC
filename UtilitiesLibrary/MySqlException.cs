using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace UtilitiesLibrary
{
    public class MySqlException : DbException
    {
        //Method to StackTrace
        public void ExceptionStackTrace(Exception ex)
        {
            Console.WriteLine("In MySqlException class, StackTrace Exception: " + ex.StackTrace);
        }
        //Method to GetBaseException
        public void GetBaseException(Exception ex)
        {
            Console.WriteLine("In MySqlException class,GetBaseException" + ex.GetBaseException());
        }
        //Method to Get InnerException
        public void MyInnerException(Exception ex)
        {
            Console.WriteLine("In MySqlException class,InnerException" + ex.InnerException);
        }
        //Method to get Error Message
        public void MyExceptionMessage(Exception ex)
        {
            Console.WriteLine("In MySqlException class, Message", ex.Message);
        }
    }
}
