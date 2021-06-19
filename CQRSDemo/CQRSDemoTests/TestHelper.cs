using CQRSDemo.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSDemoTests
{
    public class TestHelper
    {
        protected readonly ApplicationContext appDbContext;

        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: "AppContextDbInMemory");

            var dbContextOptions = builder.Options;
            appDbContext = new ApplicationContext(dbContextOptions);
            appDbContext.Database.EnsureDeleted();
            appDbContext.Database.EnsureCreated();
        }
    }
}
