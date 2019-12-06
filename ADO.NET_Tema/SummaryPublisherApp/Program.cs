using SQLUtils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryPublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLManager manager = new SQLManager();

            int noOfRows = manager.PrintNumberOfRowsPublisher();
            Console.WriteLine($"Publisher has {noOfRows} rows");
            manager.ReadRowsFromPublisher();
            manager.NoOfBooksForEachPublisher();
            manager.PriceOfBooksPerEachPublisher();
            

        }
    }
}
