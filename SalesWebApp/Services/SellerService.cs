using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebApp.Models;
using SalesWebApp.Data;

namespace SalesWebApp.Services
{
    public class SellerService 
    {
        private readonly SalesWebAppContext _context; // cria dependencia com o repositório

        public SellerService(SalesWebAppContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            seller.Department = _context.Department.First();
            _context.Add(seller);
            _context.SaveChanges();
        }
    }
}
