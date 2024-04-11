namespace TameAPI.Models
{
    public class EFSchedule:EFBaseModel
    {

      

        public string NameEvent { get; set; }
        public string DescriptionEvent { get; set; }
        public int DateEvent { get; set; }
        public List<EFUsers> EFUsers { get; set; } = new List<EFUsers>();
    }
}
