using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebApp.Models;
using SalesWebApp.Data;
using Microsoft.EntityFrameworkCore;

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
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller FindbyId(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(seller => seller.Id == id); // include é um comando para fazer o inner, este comando esta na Microsoft.entityFrameworkCore
        }

        public void Remove (int id)
        {
            var sellerToDelete = _context.Seller.Find(id); // nao pode ser _context.Seller.FirstOrDefault(seller => seller.Id == id - porque isso é uma prevenção no find para nao achar null, no delete, caso nao ache o ID disponivel, deletaria o default
            _context.Remove(sellerToDelete);
            _context.SaveChanges();
        }
    }
}
