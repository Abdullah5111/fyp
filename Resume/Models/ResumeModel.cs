namespace Resume.Resume
{
    public class ResumeModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Summary { get; set; }
        public List<Experience> Experience { get; set; }
        public List<Education> Education { get; set; }
        public string Skills { get; set; }
        public List<string> Certificates { get; set; } = new List<string>();


    }
}
