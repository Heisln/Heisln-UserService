using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heisln.Car.Infrastructure
{
    public abstract class EntityFrameWorkRepository
    {
        protected IMongoUserDbContext dbContext;

        protected EntityFrameWorkRepository(IMongoUserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
