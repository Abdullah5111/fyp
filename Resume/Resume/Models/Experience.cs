namespace Resume.Resume
{
    public class Experience
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public int StartDate { get; set; }
        public int? EndDate { get; set; } // Nullable if the experience is ongoing
        public string Description { get; set; }
    }
}
