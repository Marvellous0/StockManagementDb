using System;
using System.Collections.Generic;
using Entities;
using Repositories;
using MySql.Data.MySqlClient;

namespace FilePractical
{
    public class Menu
    {
        static string connectionString = "server=localhost;user=root;database=stockmanagementdb;port=3306;password=MARVELLOUS";
        static MySqlConnection conn = new MySqlConnection(connectionString);
        static MySqlConnection connect = new MySqlConnection(connectionString);
        CategoryRepository categoryRepo = new CategoryRepository(conn);
        StockRepository stockRepository = new StockRepository(connect);

        private static void ShowMainMenu()
        {
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Stock");
            Console.WriteLine("2. Category");
            Console.Write("Option: ");
        }
        private void CategoryMenu()
        {
            Console.Clear();
            Console.WriteLine("0. Back");
            Console.WriteLine("1. Add Category");
            Console.WriteLine("2. List all Categories");
            Console.WriteLine("3. Update Category");
            Console.WriteLine("4. Delete Category");
            Console.WriteLine("5. Find Category by Id");
        }
        private void StockMenu()
        {
            Console.WriteLine("0. Back");
            Console.WriteLine("1. Add Stock");
            Console.WriteLine("2. List all Stock");
            Console.WriteLine("3. Update Stock");
            Console.WriteLine("4. Delete Stock");
            Console.WriteLine("5. Find Stock by Id");
        }
        public void MainMenu()
        {
            bool appRunning = true;

            do
            {
                ShowMainMenu();
                string option = Console.ReadLine().Trim();

                switch (option)
                {
                    case "0":
                        appRunning = false;
                        break;
                    case "1":
                        ShowStockMenu();
                        break;
                    case "2":
                        ShowCategoryMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid Option!");
                        break;

                }
            } while (appRunning);
        }
        public void AddCategoryDetails()
        {
            Console.Write("Enter Category Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Category Name: ");
            string name = Console.ReadLine();

            DateTime created_at = DateTime.Now;

            categoryRepo.AddCategory(id, name, created_at);
        }
        private void ShowCategoryMenu()
        {
            CategoryMenu();
            Console.Write("Option: ");
            string option = Console.ReadLine().Trim();

            switch (option)
            {
                case "0":
                    break;
                case "1":
                    AddCategoryDetails();
                    ShowCategoryMenu();
                    break;
                case "2":
                    categoryRepo.GetCategories();
                    ShowCategoryMenu();
                    break;
                case "3":
                    Console.Write("Enter Id of Category: ");
                    int id = int.Parse(Console.ReadLine());

                    Console.Write("Update Category Name: ");
                    string name = Console.ReadLine();
                    categoryRepo.UpdateCategory(id, name);
                    ShowCategoryMenu();
                    break;
                case "4":
                    Console.Write("Enter Id of Category you want to Delete: ");
                    int Id = int.Parse(Console.ReadLine());
                    categoryRepo.DeleteCategory(Id);
                    ShowCategoryMenu();
                    break;
                case "5":
                    Console.Write("Enter Id of Category you want to Find: ");
                    int iD = int.Parse(Console.ReadLine());
                    categoryRepo.FindCategory(iD);
                    ShowCategoryMenu();
                    break;
                default:
                    Console.WriteLine("Invalid Option!");
                    break;
            }
        }

        public void AddStockDetails()
        {
            Console.Write("Enter Stock Id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Stock Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Stock cost price: ");
            double costPrice = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Stock selling price: ");
            double sellingPrice = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Stock SKU: ");
            string sKU = Console.ReadLine();

            Console.Write("Enter Stock quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Stock variation: ");
            string variation = Console.ReadLine();

            DateTime created_at = DateTime.Now;

            ReshuffleMenu();
            Console.WriteLine("Choose Category. ");
            int category_Id = Convert.ToInt32(Console.ReadLine());

            List<Stock> Stocks = stockRepository.ListAllStocks();
            foreach (var stock in Stocks)
            {
                stock.Category_Id = category_Id;
            }
            stockRepository.AddStock(id, name, costPrice, sellingPrice, sKU, quantity, variation, created_at, category_Id);
        }

        public void UpdateStockDetails()
        {
            Console.Write("Enter Id of Stock: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Update Stock Name: ");
            string name = Console.ReadLine();

            Console.Write("Update Stock cost price: ");
            double costPrice = Convert.ToDouble(Console.ReadLine());

            Console.Write("Update Stock selling price: ");
            double sellingPrice = Convert.ToDouble(Console.ReadLine());

            Console.Write("Update Stock SKU: ");
            string sKU = Console.ReadLine();

            Console.Write("Update Stock quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Update Stock variation: ");
            string variation = Console.ReadLine();

            stockRepository.UpdateStock(id, name, costPrice, sellingPrice, sKU, quantity, variation);
        }

        public void DeleteStockDetails()
        {
            Console.Write("Enter Id of Stock you want to Delete: ");
            int Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Are you sure you want to delete");
            string answer = Console.ReadLine().ToLower().Trim();

            if (answer == "yes")
            {
                stockRepository.DeleteStock(Id);
            }

            else
            {
                StockMenu();
            }
        }
        private void ShowStockMenu()
        {
            StockMenu();
            Console.WriteLine("Enter an option: ");
            string option = Console.ReadLine().Trim();

            switch (option)
            {
                case "0":
                    break;

                case "1":
                    AddStockDetails();
                    ShowStockMenu();
                    break;

                case "2":
                    stockRepository.GetStocks();
                    ShowStockMenu();
                    break;

                case "3":
                    UpdateStockDetails();
                    ShowStockMenu();
                    break;

                case "4":
                    DeleteStockDetails();
                    ShowStockMenu();
                    break;

                case "5":
                    Console.Write("Enter Id of Category you want to Find: ");
                    int id = int.Parse(Console.ReadLine());
                    stockRepository.FindStock(id);
                    break;
                default:
                    Console.Write("Invalid Option! ");
                    break;
            }
        }

        private void ReshuffleMenu()
        {
            List<Category> printAllStocks = categoryRepo.ListAllCategories();
            foreach (var printAllStock in printAllStocks)
            {
                Console.WriteLine($"{printAllStock.Id}. {printAllStock.Name} ");
            }
        }
    }
}