using System;
using System.Collections.Generic;
using Entities;
using MySql.Data.MySqlClient;

namespace Repositories
{
    public class StockRepository
    {
        MySqlConnection connect;
        public List<Stock> Stocks = new List<Stock>();

        public StockRepository(MySqlConnection connection)
        {
            connect = connection;
        }
        public void PrintStock(Stock stock)
        {
            Console.WriteLine($"Id: {stock.Id}, Created_At: {stock.CreatedAt}, Name: {stock.Name}, CostPrice: {stock.CostPrice}, Selling Price: {stock.SellingPrice}, SKU: {stock.SKU}, Variation: {stock.Variation}, Category Id: {stock.Category_Id}");
        }
        public List<Stock> ListAllStocks()
        {
            List<Stock> Stocks = new List<Stock>();
            try
            {
                connect.Open();
                string stockQuery = "Select id, name, cost_price, selling_price, sku, quantity, variation, created_at,category_id from stocks";
                MySqlCommand command = new MySqlCommand(stockQuery, connect);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    {
                        int id = reader.GetInt32(0);

                        string name = reader.GetString(1);

                        double cost_price = reader.GetDouble(2);

                        double selling_price = reader.GetDouble(3);

                        string sku = reader.GetString(4);

                        int quantity = reader.GetInt32(5);

                        string variation = reader.GetString(6);

                        DateTime created_at = reader.GetDateTime(7);

                        int category_id = reader.GetInt32(8);

                        Stock stock = new Stock(id, name, cost_price, selling_price, sku, quantity, variation ,created_at, category_id);
                        Stocks.Add(stock);

                    }
                    // Console.WriteLine(reader[0] + " " + reader[1] +  " " + reader[2] + " " + reader[3] + " " + reader[4] + " " + reader[5] + " " + reader[6] + " " + reader[7] + " " + reader[8]);
                }
                reader.Close();
                connect.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Stocks;
        }

        public void GetStocks()
        {
            List<Stock> Stocks = ListAllStocks();
            foreach (Stock stock in Stocks)
            {
                PrintStock(stock);
            }
        }
        public Stock FindStock(int id)
        {
            Stock stock = null;
            try
            {
                connect.Open();
                string stockQuery = "Select id, name, cost_price, selling_price, sku, quantity, variation ,created_at, category_id from stocks where id = '" + id + "'";

                MySqlCommand command = new MySqlCommand(stockQuery, connect);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    {
                        string name = reader.GetString(1);

                        double cost_price = reader.GetDouble(2);

                        double selling_price = reader.GetDouble(3);

                        string sku = reader.GetString(4);

                        int quantity = reader.GetInt32(5);

                        string variation = reader.GetString(6);

                        DateTime created_at = reader.GetDateTime(7);

                        int category_id = reader.GetInt32(8);
                        
                        stock = new Stock(id, name, cost_price, selling_price, sku, quantity, variation, created_at, category_id);
                    }
                    Console.WriteLine(reader[0] + " " + reader[1] + " " + reader[2] + " " + reader[3] + " " + reader[4] + " " + reader[5] + " " + reader[6] + " " + reader[7] + " " + reader[8]);
                }

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            connect.Close();
            return stock;
        }
        public bool AddStock(int id, string name, double costPrice, double sellingPrice, string sKU, int quantity, string variation, DateTime created_at, int category_Id)
        {
            try
            {
                connect.Open();
                string addstockQuery = "Insert into stocks (id, name, cost_price, selling_price, sku, quantity, variation, created_at, category_id)values ('" + id + "', '" + name + "', '" + costPrice + "', '" + sellingPrice + "', '" + sKU + "', '" + quantity + "', '" + variation + "', '" + created_at.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + category_Id + "')";

                MySqlCommand command = new MySqlCommand(addstockQuery, connect);
                int Count = command.ExecuteNonQuery();
                if (Count > 0)
                {
                    connect.Close();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            connect.Close();
            return false;
        }

        public bool UpdateStock(int id, string name, double costPrice, double sellingPrice, string sKU, int quantity, string variation)
        {
            var stock = FindStock(id);
            if (stock == null)
            {
                Console.WriteLine($"Stock with {id} does not exist");
            }
            try
            {
                connect.Open();

                string updatestockQuery = "update stocks set name ='" + name + "', cost_price = '" + costPrice + "', selling_price = '" + sellingPrice + "', sku = '" + sKU + "', quantity = '" + quantity + "', variation = '" + variation + "' where id = '" + id + "' ";

                MySqlCommand command = new MySqlCommand(updatestockQuery, connect);
                int Count = command.ExecuteNonQuery();

                if (Count > 0)
                {
                    Console.WriteLine("Informations updated successfully! ");
                    connect.Close();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            connect.Close();
            return false;
        }

        public bool DeleteStock(int id)
        {
            var stock = FindStock(id);
            if (stock== null)
            {
                Console.WriteLine($"Stock with {id} does not exist");
            }
            try
            {
                connect.Open();
                string deleteStockQuery = "delete from stocks where id = '" + id + "'";

                MySqlCommand command = new MySqlCommand(deleteStockQuery, connect);
                int Count = command.ExecuteNonQuery();
                if (Count > 0)
                {
                    connect.Close();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            connect.Close();
            return false;
        }
    }
}