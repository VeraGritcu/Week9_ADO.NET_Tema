using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLUtils;


namespace CrudBookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLManager manager = new SQLManager();
            manager.InsertBookAndPrintID("Some Title", 1, 2015, 15.2m);
            manager.UpdateBook("New Title");
            manager.SelectBookAndRead();
        }
    }
}
