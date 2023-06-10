

namespace CleanArch.Application.DTOs.People
{
    public class CreatePersonDTO
    {
        #region Properties

        [Display(Name = "First Name")]
        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Age { get; set; }

        [MaxLength(200)]
        public string Jub { get; set; }

        #endregion
    }
}
