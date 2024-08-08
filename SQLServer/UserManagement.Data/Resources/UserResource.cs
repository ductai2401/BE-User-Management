namespace UserManagement.Data.Resources
{
    public class UserResource : ResourceParameter
    {
        public UserResource() : base("Email")
        {
        }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IsActive { get; set; }
    }
}
