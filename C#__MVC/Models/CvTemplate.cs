using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace C___MVC.Models
{
    [Table("CvTemplate")]
    public class CvTemplate
    {
        [Key]
        public int TemplateId { get; set; }  // Khóa chính
        public string TemplateName { get; set; }  // Tên của mẫu CV
        public string HtmlCreateContent { get; set; }  // Nội dung HTML cho việc tạo CV
        public string HtmlEditContent { get; set; }
        public string Image { get; set; }
        public ICollection<CV> CVs { get; set; } = new List<CV>();
    }
}
