using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebApp.Models;
using SalesWebApp.Data;
using Microsoft.EntityFrameworkCore;
using SalesWebApp.Services.Exceptions;

namespace SalesWebApp.Services
{
    public class SellerService
    {
        private readonly SalesWebAppContext _context; // cria dependencia com o repositório

        public SellerService(SalesWebAppContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
           await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindbyIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(seller => seller.Id == id); // include é um comando para fazer o inner, este comando esta na Microsoft.entityFrameworkCore
        }

        public async Task RemoveAsync(int id)
        {
            var sellerToDelete = await _context.Seller.FindAsync(id); // MyComments nao pode ser _context.Seller.FirstOrDefault(seller => seller.Id == id - porque isso é uma prevenção no find para nao achar null, no delete, caso nao ache o ID disponivel, deletaria o default
            _context.Remove(sellerToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {

            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
                throw new NotFoundException("Id not found");

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) // MyComments captura uma excessao do banco de dados e trata como uma excessao criada nos services, assim nosso controller so ira lidar com exceçoes do service, respeitando o MVC
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
