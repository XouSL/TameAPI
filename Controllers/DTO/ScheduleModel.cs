using TameAPI.Context;
using TameAPI.Replicates;

namespace TameAPI.Controllers.DTO
{
    public class ScheduleModel
    {
        public ScheduleModel() { }
        public ScheduleModel(Schedule context)
        {
            id = context.Id;
            nameEvent = context.NameEvent;
            descriptionEvent = context.DescriptionEvent;
            DateEvent = context.DateEvent;
            Users = context.Users.Select(it => new UsersModel(it)).ToArray();
        }

        public int id { get; set; }
        public string nameEvent { get; set; }
        public string descriptionEvent { get; set; }
        public int DateEvent { get; set; }
        public UsersModel[] Users { get; set; }
    }
}
