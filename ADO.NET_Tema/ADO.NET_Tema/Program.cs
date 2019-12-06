using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SQLUtils;

namespace InsertPublisherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLManager manager = new SQLManager();
           
            Console.WriteLine("Enter a new publisher name");

            var publisherName = Console.ReadLine();

            int id = manager.InsertPublisher(publisherName);

            Console.WriteLine($"Publisher was inserted in Publisher table with the ID {id}");

        }

        

    }
}
