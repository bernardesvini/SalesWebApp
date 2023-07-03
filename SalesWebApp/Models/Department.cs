using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {
        }
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            // SEM LAMBDA

            //double totalSales = 0.0;

            //foreach (Seller seller in Sellers)
            //    totalSales += seller.TotalSales(initial, final);
            
            //return totalSales;

            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
