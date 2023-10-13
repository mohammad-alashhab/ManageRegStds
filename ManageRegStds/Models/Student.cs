using System.ComponentModel.DataAnnotations;

namespace ManageRegStds.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [RegularExpression("^[A-Za-z ]*$", ErrorMessage = "Name must contain only alphabetical characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Student Number is required")]
        public int StudentNumber { get; set; }

        [Required(ErrorMessage = "Grade is required")]
        public string GradeName { get; set; }
    }
}
