using CQRSDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CQRSDemo.Context
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }

        Task<int> SaveChanges();
    }
}