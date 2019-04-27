using AuthorizeService.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace authorize.service.test.TestData
{
    public class ApplicationDbContextTest
    {
        public ApplicationDbContext GetTestDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "AuthorizeTest")
                        .Options;
            return new ApplicationDbContext(options);
        }
    }
}
