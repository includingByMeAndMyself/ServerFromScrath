using Server.ItSelf;
using System.Threading.Tasks;

namespace ServerFromScrath.Controllers
{
    public record User(string Name, string Surname, string Login);

    public class UsersController : IController
    {
        public User[] Index()
        {
            return new[]
            {
                new User("Name1", "Surname1", "Login1"),
                new User("Name2", "Surname2", "Login2"),
                new User("Name3", "Surname3", "Login3"),
                new User("Name4", "Surname4", "Login4"),
            };
        }

        public async Task<User[]> IndexAsync()
        {
            await Task.Delay(5);
            return new[]
            {
                new User("Name1", "Surname1", "Login1"),
                new User("Name2", "Surname2", "Login2"),
                new User("Name3", "Surname3", "Login3"),
                new User("Name4", "Surname4", "Login4"),
            };
        }
    }
}
