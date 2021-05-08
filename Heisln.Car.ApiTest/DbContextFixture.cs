using Heisln.Car.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heisln.ApiTest
{
    public class DbContextFixture : IDisposable
    {
        readonly string connectionString = "DataSource=:memory:";
        readonly SqliteConnection connection;
        public DatabaseContext DatabaseContext { get; }
        readonly Car.Domain.User[] Users;

        public DbContextFixture()
        {
            connection = new SqliteConnection(connectionString);
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite(connection)
                .Options;
            DatabaseContext = new DatabaseContext(options);

            DatabaseContext.Database.OpenConnection();
            DatabaseContext.Database.Migrate();
            DatabaseContext.Database.EnsureCreated();


            Users = new Car.Domain.User[]
            {
                new Car.Domain.User(Guid.NewGuid(), "hans.peter@yahoo.at", "asdfg", "Hans", "Peter", DateTime.Now),
                new Car.Domain.User(Guid.NewGuid(), "gustav.nimmersatt@yahoo.at", "asdfg123", "Gustav", "Nimmersatt", DateTime.Now)
            };


            SeedDatabase();
        }

        void SeedDatabase()
        {
            foreach (var user in Users) DatabaseContext.Users.Add(user);
            DatabaseContext.SaveChanges();
        }

        public void Dispose()
        {
            DatabaseContext.Dispose();
            connection.Close();
        }
    }
}
