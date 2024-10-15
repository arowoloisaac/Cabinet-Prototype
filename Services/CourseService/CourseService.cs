using Cabinet_Prototype.Data;

namespace Cabinet_Prototype.Services.CourseService
{
    public class CourseService: ICourseService
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseService (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
