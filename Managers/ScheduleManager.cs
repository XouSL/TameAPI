using TameAPI.Context;
using TameAPI.Controllers.DTO;
using TameAPI.Models;
using TameAPI.Replicates;
using Microsoft.EntityFrameworkCore;

namespace TameAPI.Managers
{
    public class ScheduleManager
    {

        protected ApplicationContext ApplicationContext { get; set; }
        protected DBContext DBContext { get; set; }
        public ScheduleManager (ApplicationContext applicationContext) { ApplicationContext = applicationContext;  DBContext = applicationContext.CreateDbContext(); }

        private List<Schedule> _schedules { get; set; } = new List<Schedule> ();

        public Schedule[] Schedules { get => _schedules.ToArray (); }
        public bool Read()
        {
            try
            {
                DBContext.Schedules.Include(it => it.EFUsers.ToList());
                foreach (EFSchedule item in DBContext.Schedules)
                {
                    if(item.IsDeleted != true) _schedules.Add(new Schedule(item));
                }
                return true;
            }
            catch { throw; }
        }

        public Schedule Get(int id) => _schedules.FirstOrDefault(it => it.Id == id);

        public Schedule Create(ScheduleModel model)
        {
            try
            {
                EFSchedule schedule = new EFSchedule()
                {
                    NameEvent = model.nameEvent,
                    DescriptionEvent = model.descriptionEvent,
                    DateEvent = model.DateEvent,
                };
                DBContext.Add(schedule);
                DBContext.SaveChanges();

                Schedule replicate = new Schedule(schedule);
                _schedules.Add(replicate);

                return replicate;
            }
            catch { throw; }
        }

        public Schedule Update(ScheduleModel model)
        {
            try
            {

                EFSchedule _schedule = DBContext.Schedules.FirstOrDefault(it => it.Id == model.id);


                _schedule.NameEvent = model.nameEvent;
                _schedule.DescriptionEvent = model.descriptionEvent;
                _schedule.DateEvent = model.DateEvent;

                DBContext.Update(_schedule);
                DBContext.SaveChanges();

                _schedules.Remove(_schedules.FirstOrDefault(it => it.Id == model.id));
                Schedule repl = new Schedule(_schedule);
                _schedules.Add(repl);

                return repl;
            }
            catch { throw; }
        }

        public Users[] GetUsers(int scheduleId)
        {
            return Get(scheduleId).Users.ToArray();
        }

        public Users[] AttachUser(int scheduleId, int userId)
        {
            var user = ApplicationContext.UsersManager.Get(userId);

            var _schedule = DBContext.Schedules.FirstOrDefault(it => it.Id == scheduleId);
            _schedule.EFUsers.Add(user.Context);

            DBContext.Update(_schedule);
            DBContext.SaveChanges();

            var schedule = Get(scheduleId);
            schedule.Users.Add(user);

            return GetUsers(scheduleId);
        }

        public Users[] DettachUsers(int scheduleId, int userId)
        {
            var user = ApplicationContext.UsersManager.Get(userId);

            var _schedule = DBContext.Schedules.FirstOrDefault(it => it.Id == scheduleId);
            _schedule.EFUsers.Remove(user.Context);

            DBContext.Update(_schedule);
            DBContext.SaveChanges();


            var schedule = Get(scheduleId);
            schedule.Users.Remove(user);

            return GetUsers(scheduleId);
        }

        public bool Delete(int id)
        {
            try
            {

                EFSchedule _schedule = DBContext.Schedules.FirstOrDefault(it => it.Id == id);
                _schedule.IsDeleted = true;
                DBContext.Update(_schedule);
                DBContext.SaveChanges();
                _schedules.Remove(_schedules.FirstOrDefault(it => it.Id == id));
                return true;

            }
            catch { throw; }



        }

    }
}
