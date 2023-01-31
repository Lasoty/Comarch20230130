using BMICalculator.Model.Context;
using BMICalculator.Model.DTO;
using System.Threading.Tasks;

namespace BMICalculator.Model.Repositories
{
    public interface IResultRepository
    {
        Task SaveResultAsync(BmiResult result);
    }

    public class ResultRepository : IResultRepository
    {
        private AppDbContext dbContext;

        public ResultRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task SaveResultAsync(BmiResult result)
        {
            dbContext.Results.Add(result);
            return dbContext.SaveChangesAsync();
        }
    }
}
