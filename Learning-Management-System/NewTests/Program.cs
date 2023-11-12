using LearningManagementSystem.Domain.Entities.Users;
using LearningManagementSystem.Domain.Common;

class Program
{
    static async Task Main()
    {
        //LearningManagementSystemDbContext context = new();

        // Input data for the new user
        string firstName = "John";
        string lastName = "Doe";
        string email = "johndoe@example.com";
        string password = "password123";
        UserRole role = UserRole.Student; // Set the desired role
        string phoneNumber = "+40123456789"; // Optional phone number

        // Create the user using the Create method
        var result = User.Create(firstName, lastName, email, password, role);

        /*if (result.IsSuccess)
        {
            User newUser = result.Value;

            // Optionally, attach a phone number
            newUser.AttachPhoneNumber(phoneNumber);

            UserRepository userRepository = new(context);
            await userRepository.AddAsync(newUser);
            Console.WriteLine("User created and added to the database.");
            var result1 = await userRepository.FindByIdAsync(newUser.UserId);
            Console.WriteLine(result1.Value.FirstName);


        }
        else
        {
            Console.WriteLine(result.Error);
        }*/
    }
}
