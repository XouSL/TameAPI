namespace TameAPI.Models
{
    public class EFUsers:EFBaseModel
    {

        public string Name { get; set; }
       
        public string? Invited { get; set; }

        public List<EFSchedule> Schedule { get; set; } = new List<EFSchedule>();

    }
}
