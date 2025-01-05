using System.ComponentModel.DataAnnotations.Schema;

namespace C___MVC.Models
{
    [Table("Category")]
    public class Category
    {

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
