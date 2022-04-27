using System.ComponentModel.DataAnnotations;

namespace University_MGS_API.Models
{
    public class Student
    {
        [Key]
        public int StuID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }
    }
}
