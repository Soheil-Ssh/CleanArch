namespace CleanArch.Application.DTOs.People
{
    public class DetailsPersonDTO
    {
        #region Properties

        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Age { get; set; }

        public string Jub { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        #endregion
    }
}
