using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SalesWebApp.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is mandatory")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} Size must be between {2} and {1}")]// {0} traz o nome do atributo, estas anotações são possivel por conta do entity framework
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is mandatory")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is mandatory")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} is mandatory")]
        [Display(Name = "Base Salary")]
        [DisplayFormat (DataFormatString = "{0:F2}")]

        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales (SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales (SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales (DateTime initial, DateTime final)
        {
            // *Mycomments SEM LAMBDA

            //double totalSales = 0.0;

            //foreach (SalesRecord sale in Sales)
            //{
            //    if (sale.Date >= initial && sale.Date <= final)
            //        totalSales += sale.Amount;
            //}
            //return totalSales;

            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
