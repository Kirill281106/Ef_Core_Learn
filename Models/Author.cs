using System.ComponentModel.DataAnnotations;

namespace EFCore.Models
{
    internal class Author
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int? Age { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
