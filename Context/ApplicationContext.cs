using TameAPI.Managers;
using Microsoft.EntityFrameworkCore;

namespace TameAPI.Context
{
    public class ApplicationContext
    {

        public ApplicationContext(IConfiguration config)
        {
            Version = "0.1";
            Title = "TIME";
            Configuration = config;
            Initialize();
        }

        public void Initialize()
        {

            ScheduleManager = new ScheduleManager(this);
            UsersManager = new UsersManager(this);

            ScheduleManager.Read();
            UsersManager.Read();

        }

        public ScheduleManager ScheduleManager { get; set; }
        public UsersManager UsersManager { get; set; }
        public string Version { get; set; }
        public string Title { get; set; }
        public IConfiguration Configuration { get; set; }

        public DBContext CreateDbContext() => new DBContext(Configuration.GetConnectionString("DefaultConnection"));

    }
}
