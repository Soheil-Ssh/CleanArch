
namespace CleanArch.Application.DTOs.People
{
    public class PersonDTO : BaseEntity
    {
        #region MyRegion
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string ImageName { get; set; }

        #endregion
    }
}
