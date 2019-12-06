using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLUtils
{
    public class SQLManager
    {
        public SqlConnection OpenSQLConnection()
        {
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=ADO_NET;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        public int InsertPublisher(string publisherName)
        {
            string tableName = "Publisher";
            string commandQuery = $"insert into {tableName} (Name) values (@name); select scope_identity();";

            SqlParameter nameParam = new SqlParameter("@name", publisherName);

            var sqlConnection = OpenSQLConnection();
            SqlCommand command = new SqlCommand(commandQuery, sqlConnection);
            command.Parameters.Add(nameParam);

            int id = -1;
            try
            {
                id = (int)(decimal)command.ExecuteScalar();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            return id;
        }

        public int PrintNumberOfRowsPublisher()
        {
            int id = -1;
            var sqlConnection = OpenSQLConnection();
            try
            {
                var query = "select count (*) from Publisher";

                SqlCommand command = new SqlCommand(query, sqlConnection);

                id = (int)command.ExecuteScalar();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            return id;
        }

        public void ReadRowsFromPublisher()
        {
            var sqlConnection = OpenSQLConnection();

            try
            {
                var query = $"select top 10 publisherID, Name from Publisher";

                SqlCommand command = new SqlCommand(query, sqlConnection);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var id = currentRow["PublisherId"];
                    var name = currentRow["Name"];

                    Console.WriteLine($"{id} - {name}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void NoOfBooksForEachPublisher()
        {
            var sqlConnection = OpenSQLConnection();
            try
            {
                var query = "select p.name as PublisherName, count(b.bookid) as NoOfBooks from publisher p " +
                    "inner join book b on b.publisherid = p.publisherid group by p.name";

                SqlCommand command = new SqlCommand(query, sqlConnection);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var name = currentRow["PublisherName"];
                    var number = currentRow["NoOfBooks"];

                    Console.WriteLine($"Publisher {name} - No of books {number}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void PriceOfBooksPerEachPublisher()
        {
            var sqlConnection = OpenSQLConnection();
            try
            {
                var query = "select p.name as PublisherName, sum(b.Price) as TotalPrice from publisher p " +
                    "inner join book b on b.publisherid = p.publisherid group by p.name";

                SqlCommand command = new SqlCommand(query, sqlConnection);

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var currentRow = dataReader;

                    var name = currentRow["PublisherName"];
                    var price = currentRow["TotalPrice"];

                    Console.WriteLine($"Publisher {name} - TotalPrice {price}");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void InsertBookAndPrintID(string title, int publisherID, int year, decimal price)
        {

            var sqlConnection = OpenSQLConnection();

            try
            {
                var commandQuery = "insert into book values (@Title, @PublisherID, @Year, @Price); select scope_identity();";

                SqlParameter titleParam = new SqlParameter("@Title", title);
                SqlParameter publisherIDParam = new SqlParameter("@PublisherID", publisherID);
                SqlParameter yearParam = new SqlParameter("@Year", year);
                SqlParameter priceParam = new SqlParameter("@Price", price);

                SqlCommand insertCommand = new SqlCommand(commandQuery, sqlConnection);

                insertCommand.Parameters.Add(titleParam);
                insertCommand.Parameters.Add(publisherIDParam);
                insertCommand.Parameters.Add(yearParam);
                insertCommand.Parameters.Add(priceParam);

                var id = insertCommand.ExecuteScalar();
                Console.WriteLine($"The newly introduced book holds the BookID {id}");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void UpdateBook(string title)
        {
            var sqlConnection = OpenSQLConnection();
            try
            {
                var commandQuery = "update Book set Title = @Title where Title like 'Some Title'; select scope_identity();";

                SqlCommand updateCommand = new SqlCommand(commandQuery, sqlConnection);

                SqlParameter titleParam = new SqlParameter("@Title", title);
                updateCommand.Parameters.Add(titleParam);
                var id = updateCommand.ExecuteScalar();

                Console.WriteLine($"The book you just updated holds the BookID {id}");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void DeleteBook(string title)
        {
            var sqlConnection = OpenSQLConnection();
            try
            {
                var commandQuery = "delete Book set Title = @Title where Title like 'Some Title'; select scope_identity();";

                SqlCommand updateCommand = new SqlCommand(commandQuery, sqlConnection);

                SqlParameter titleParam = new SqlParameter("@Title", title);
                updateCommand.Parameters.Add(titleParam);
                var id = updateCommand.ExecuteScalar();

                Console.WriteLine($"The book you just updated holds the BookID {id}");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public void SelectBookAndRead()
        {
            var sqlConnection = OpenSQLConnection();
            try
            {
                var query = "select * from book where bookid = 6;select scope_identity();";

                SqlCommand command = new SqlCommand(query, sqlConnection);
                var id = command.ExecuteScalar();
                Console.WriteLine($"You selected bookID {id}");
                
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
