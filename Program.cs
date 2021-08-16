using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//Student Name: Yang Beng Ng(Ivan)
//Student ID: 30031552
//Date 16/08/2021
/*Description:You are required to make a list of different annual salaries for payroll
in whole numbers (integers) that will then need to be sorted, you should have
alternate methods of sorting so that payroll can decide on which method they would
like to use.
You need to create an application that creates lists of integer values between 10K
and 10 million. Your application must have the ability to sort in three different styles
with timers to indicate the speed at which this happens you must have at least 1
million items in your list as this the future business strategy to employ at least this
many staff. The current system is only able to handle 12 staff. Only 1 sorting
technique may use the inbuilt sorting the rest you must write yourself.
 */

namespace PayrollSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Sorter sorter = new Sorter(1, 100000000);
            Console.WriteLine("Starting up payroll sorter...");
            Thread.Sleep(5000);
            sorter.Run(100, 1000);
            sorter.Run(100, 10000);
            sorter.Run(100, 50000);
            sorter.Run(100, 100000);
            Console.Read();
        }
    }
}
