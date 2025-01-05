using C___MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace C___MVC.ViewComponents
{
	public class SkillsByJobViewComponent : ViewComponent
	{
		private readonly JobContext _jobContext;

		public SkillsByJobViewComponent(JobContext jobContext)
		{
			_jobContext = jobContext;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var skillsByJob = await _jobContext.Skills
				.Include(s => s.Job) // Bao gồm thông tin công việc
				.GroupBy(s => new { s.Job.Title, s.Name,s.SkillId  }) // Nhóm theo tiêu đề công việc và tên kỹ năng
				.Select(g => new
				{
					JobTitle = g.Key.Title,
					SkillName = g.Key.Name,
					SkillId = g.Key.SkillId,
					Skills = g.Select(s => s).FirstOrDefault()
				})
				.ToListAsync();

			return View("RenderSkillsByJob", skillsByJob); // Truyền danh sách kỹ năng theo công việc tới view
		}
	}
}
