using TameAPI.Models;

namespace TameAPI.Replicates
{
    public class Schedule
    {
        private EFSchedule Context { get; set; }
        public Schedule(EFSchedule context) { Context = context; }
        public int Id { get => Context.Id; }
        public string NameEvent  { get => Context.NameEvent; set => Context.NameEvent = value; }

        public string DescriptionEvent { get => Context.DescriptionEvent; set => Context.DescriptionEvent = value; }
        public int DateEvent { get => Context.DateEvent; set => Context.DateEvent = value; }


        public List<Users> Users {  get => Context.EFUsers.Select(it => new Users(it)).ToList();
        }
    }
}
