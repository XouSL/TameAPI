using TameAPI.Replicates;

namespace TameAPI.Controllers.DTO
{
    public class UsersModel
    {
        public UsersModel() { }
        public UsersModel(Users context)
        {
            id = context.Id;
            name = context.Name;
          
            invited = context.Invited;
        }

        public int id { get; set; }
        public string name { get; set; }
       
        public string? invited { get; set; }
    }
}
