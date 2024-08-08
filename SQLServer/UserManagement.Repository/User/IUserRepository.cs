using UserManagement.Common.GenericRespository;
using UserManagement.Data;
using UserManagement.Data.Dto;
using System.Threading.Tasks;
using UserManagement.Data.Resources;

namespace UserManagement.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<UserList> GetUsers(UserResource userResource);
        Task<UserAuthDto> BuildUserAuthObject(User appUser);
    }
}
