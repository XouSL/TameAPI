using TameAPI.Context;
using TameAPI.Controllers.DTO;
using TameAPI.Models;
using TameAPI.Replicates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TameAPI.Managers
{
    public class UsersManager
    {

        protected ApplicationContext ApplicationContext { get; set; }
        protected DBContext DBContext { get; set; }
        public UsersManager(ApplicationContext applicationContext) { ApplicationContext = applicationContext; DBContext = applicationContext.CreateDbContext(); }

        private List<Users> _users { get; set; } = new List<Users>();

        public Users[] Users { get => _users.ToArray(); }
        public bool Read()
        {
            try
            {

                DBContext.Users.Include(it => it.Schedule).ToList();
                foreach (EFUsers item in DBContext.Users)
                {
                    if (item.IsDeleted != true) _users.Add(new Users(item));
                }
                return true;
            }
            catch { throw; }
        }

        public Users Get(int id) => _users.FirstOrDefault(it => it.Id == id);

        public Users Create(UsersModel model)
        {
            try
            {
                EFUsers users = new EFUsers()
                {
                    Name = model.name,
                   
                    Invited = model.invited,
                };
                DBContext.Add(users);
                DBContext.SaveChanges();

                Users replicate = new Users(users);
                _users.Add(replicate);

                return replicate;
            }
            catch { throw; }
        }

        public Users Update(UsersModel model)
        {
            try
            {

                EFUsers _User = DBContext.Users.FirstOrDefault(it => it.Id == model.id);


                _User.Name = model.name;
                _User.Invited = model.invited;
                

                DBContext.Update(_User);

                _users.Remove(_users.FirstOrDefault(it => it.Id == model.id));
                Users repl = new Users(_User);
                _users.Add(repl);

                return repl;
            }
            catch { throw; }
        }

        public bool Delete(int id)
        {
            try
            {

                EFUsers _user = DBContext.Users.FirstOrDefault(it => it.Id == id);
                _user.IsDeleted = true;
                DBContext.Update(_users);

                _users.Remove(_users.FirstOrDefault(it => it.Id == id));
                return true;

            }
            catch { throw; }
        }
    }
}
