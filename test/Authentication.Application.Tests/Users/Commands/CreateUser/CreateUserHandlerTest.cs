using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Authentication.Application.Tests.Infrastructure;
using Authentication.Application.Users.Commands.CreateUser;
using Xunit;

namespace Authentication.Application.Tests.Users.Commands.CreateUser
{
    public class CreateUserHandlerTest
    {
        [Fact]
        public async Task HandlerReturnsCorrectUserDTOAsync()
        {
            //Given
            var context = new TestDbContext();
            var command = new CreateUserHandler(context);
            var user = new CreateUserCommand(){
                UserName = "bruno2",
                Password = "123mudar",
                FirstName = "bruno",
                LastName = "costa",
                Email = "bruno.bruno@email.com"
            };
            //When
            await command.Handle(user, CancellationToken.None);
            //Then
            Assert.Equal(1, context.Users.Count());
        }
    }
}