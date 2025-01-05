using C___MVC.Models;

namespace C___MVC.Dto
{
    public class DashboardViewModel
    {
        public int TotalCandidates { get; set; }
        public int TotalRecruiters { get; set; }
        public int CandidatesWithResume { get; set; }
        public int TotalJobs { get; set; } // Tổng số Job

        // Các danh sách chứa dữ liệu của từng loại đối tượng
        public List<Candidate> Candidates { get; set; } = new List<Candidate>();
        public List<Recruiter> Recruiters { get; set; } = new List<Recruiter>();
        public List<Job> Jobs { get; set; } = new List<Job>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Resume> Resumes { get; set; } = new List<Resume>();

        // Các thuộc tính bổ sung (nếu cần)
        public double AverageProfileCompletion { get; set; } // Tỷ lệ hoàn thành hồ sơ trung bình
    }
}
