using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NotatnikUzytkownikow.Dtos;
using NotatnikUzytkownikow.Requests;

namespace NotatnikUzytkownikow.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(CreateUserRequest request);
        Task<List<FoundUserDto>> GetAllUsers();
        Task UpdateUser(UpdateUserRequest request);
        Task DeleteUser(Guid id);
        Task<byte[]> GenerateReport();
        Task<Guid> GetUserId(CreateUserRequest request);
    }
}
