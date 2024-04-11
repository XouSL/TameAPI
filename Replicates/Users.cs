using TameAPI.Models;

namespace TameAPI.Replicates
{
    public class Users
    {

        public EFUsers Context { get; set; }
        public Users(EFUsers context) { Context = context; }

        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public string? Invited { get => Context.Invited; set => Context.Invited = value; }

        public List<Schedule> Schedule { get => Context.Schedule.Select(it => new Schedule(it)).ToList(); }
    }
}
