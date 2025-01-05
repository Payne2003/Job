using C___MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace C___MVC.ViewComponents
{
	public class JobListViewComponent : ViewComponent 
	{
		private readonly JobContext _jobContext;

		public JobListViewComponent(JobContext jobContext)
		{
			_jobContext = jobContext;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{// Lấy tất cả các công việc từ cơ sở dữ liệu cùng với các bảng liên quan
			var jobs = await _jobContext.Jobs
				.GroupBy(j => j.Title) // Nhóm các công việc theo tên (Title)
				.Select(g => g.FirstOrDefault()) // Chỉ lấy một công việc đại diện cho mỗi nhóm tên
				.ToListAsync();

			return View("RenderListJob", jobs); // Truyền danh sách job tới view 
		}
	}
}