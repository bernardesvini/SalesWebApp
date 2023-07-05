using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebApp.Models.ViewModels
{// classe criado, para o caso de você precisar mostrar dados de mais de uma entitie, no caso será usado para criação do Seller, que tb precisa de department
    // funciona como uma genérica
    public class SellerFormViewModel 
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; } // para mostrar departamentos disponiveis ao seller
    }
}
