namespace LearningManagementSystem.Application.Models.Identity
{
    public class UserInfoModel
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public UserInfoModel(string userName, string email, string phoneNumber, string firstName, string lastName)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
