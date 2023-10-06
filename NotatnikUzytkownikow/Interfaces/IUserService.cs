using NotatnikUzytkownikow.Dtos;
using NotatnikUzytkownikow.Requests;

namespace NotatnikUzytkownikow.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(CreateUserRequest request);
        Task<List<foundUserDto>> GetAllUsers();
        Task UpdateUser(UpdateUserRequest request);
        Task DeleteUser(Guid id);
        Task GenerateRaport(Guid id);
    }
}
