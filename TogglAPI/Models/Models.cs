namespace TogglAPI.Models
{
    public class Workspace
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Rounding { get; set; }
        public int RoundingMinutes { get; set; }
    }

    public class Project
    {
        public long Id { get; set; }
        public long Wid { get; set; }
        public long Cid { get; set; }
        public string Name { get; set; }
        public bool Billable { get; set; }
        public bool Active { get; set; }
    }

}
