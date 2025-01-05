using C___MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace C___MVC.Data
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Category> Categories { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Link> Links { get; set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<SavedJob> SavedJobs { get; set; }
        public DbSet<CV> CVs { get; set; }  
        public DbSet<CvTemplate> CvTemplates { get; set; }
        public DbSet<Contact> Contacts{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Jobs)
                .WithOne(j => j.Category)
                .HasForeignKey(j => j.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade

            // Cấu hình mối quan hệ giữa Job và Link
            modelBuilder.Entity<Job>()
                .HasMany(j => j.Links)
                .WithOne(l => l.Job)
                .HasForeignKey(r => r.JobId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade cho Links

            // Cấu hình mối quan hệ giữa Job và Skill
            modelBuilder.Entity<Job>()
                .HasMany(j => j.Skills)
                .WithOne(s => s.Job)
                .HasForeignKey(r => r.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình mối quan hệ Job và Resume
            modelBuilder.Entity<Job>()
                .HasMany(j => j.Resumes)
                .WithOne(r => r.Job)
                .HasForeignKey(r => r.JobId)
                .OnDelete(DeleteBehavior.NoAction); // Thay đổi xóa cascade

            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Links)
                .WithOne(l => l.Candidate)
                .HasForeignKey(r => r.CandidateId)
                .OnDelete(DeleteBehavior.NoAction); // Thay đổi xóa cascade

            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Skills)
                .WithOne(s => s.Candidate)
                .HasForeignKey(r => r.CandidateId)
                .OnDelete(DeleteBehavior.NoAction); // Thay đổi xóa cascade

            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Resumes)
                .WithOne(r => r.Candidate)
                .HasForeignKey(r => r.CandidateId)
                .OnDelete(DeleteBehavior.NoAction); // Thay đổi xóa cascade

            modelBuilder.Entity<Recruiter>()
                .HasMany(r => r.Links)
                .WithOne(l => l.Recruiter)
                .HasForeignKey(r => r.RecruiterId)
                .OnDelete(DeleteBehavior.NoAction); // Thay đổi xóa cascade

            modelBuilder.Entity<Recruiter>()
                .HasMany(r => r.Skills)
                .WithOne(s => s.Recruiter)
                .HasForeignKey(r => r.RecruiterId)
                .OnDelete(DeleteBehavior.NoAction); // Thay đổi xóa cascade

            modelBuilder.Entity<Recruiter>()
                .HasMany(r => r.PostedJobs)
                .WithOne(j => j.Recruiter)
                .HasForeignKey(r => r.RecruiterId)
                .OnDelete(DeleteBehavior.NoAction); // Thay đổi xóa cascade

            base.OnModelCreating(modelBuilder);
        }

    }
}
