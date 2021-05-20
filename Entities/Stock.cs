using System;

namespace Entities
{
    public class Stock : BaseEntity
    {
        public int Category_Id;
        public string Name { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public string Variation { get; set; }

        public Stock(int id, string name, double costPrice, double sellingPrice, string sKU, int quantity, string variation, DateTime created_at, int category_Id)
        {
            Id = id;
            CreatedAt = created_at;
            Name = name;
            CostPrice = costPrice;
            SellingPrice = sellingPrice;
            SKU = sKU;
            Quantity = quantity;
            Variation = variation;
            Category_Id = category_Id;
        }

        public override string ToString()
        {
            return $"{Id}\t{CreatedAt}\t{Name}\t{CostPrice}\t{SellingPrice}\t{SKU}\t{Quantity}\t{Variation}\t{Category_Id}";
        }

        // internal static Stock StringToStock(string stockString)
        // {
        //     var props = stockString.Split("\t");

        //     int id = int.Parse(props[0]);

        //     DateTime created_At = DateTime.Parse(props[1]);

        //     double costPrice = Convert.ToDouble(props[3]);

        //     double sellingPrice = Convert.ToDouble(props[4]);

        //     int quantity = int.Parse(props[6]);

        //     int category_Id = Convert.ToInt32(props[8]);

        //     return new Stock(id, props[2], costPrice, sellingPrice, props[5], quantity, props[7], category_Id);
        // }
    }
}