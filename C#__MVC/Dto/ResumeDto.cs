namespace C___MVC.Dto
{
    public class ResumeDto
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Introduce { get; set; }
        public IFormFile Cv { get; set; }
    }
}
