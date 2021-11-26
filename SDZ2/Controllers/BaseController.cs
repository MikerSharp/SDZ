using SDZ2.Domain;
namespace SDZ2.Controllers
{
    public abstract class BaseController
    {
        protected readonly MyDbContext _dbContext;
        public BaseController(MyDbContext dbContext) => _dbContext = dbContext;
    }
}
