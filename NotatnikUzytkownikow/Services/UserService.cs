using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotatnikUzytkownikow.Dtos;
using NotatnikUzytkownikow.Entities;
using NotatnikUzytkownikow.Interfaces;
using NotatnikUzytkownikow.Requests;
using System.Data;

namespace NotatnikUzytkownikow.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dbContext;

        public UserService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task CreateUser(CreateUserRequest request)
        {
            var t = TimeOnly.Parse("00:00:00");
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate.ToDateTime(t)
            };

            await _dbContext.Users.AddAsync(newUser);

            if (!(request.AdditionalAttributes.IsNullOrEmpty()))
            {
                foreach (var attribute in request.AdditionalAttributes)
                {
                    var newAttribute = new AdditionalAttribute
                    {
                        AttributeName = attribute.AttributeName,
                        Value = attribute.Value,
                        UserId = newUser.Id
                    };
                    await _dbContext.AdditionalAttributes.AddAsync(newAttribute);
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid id)
        {
            var foundUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (foundUser is null)
            {
                throw new Exception("User doesn't exists");
            }
            _dbContext.Users.Remove(foundUser);
            await _dbContext.SaveChangesAsync();
        }      
        
        public async Task<List<foundUserDto>> GetAllUsers()
        {
            var foundUsers = await _dbContext.Users.ToListAsync();

            var foundUserDtoList = foundUsers.Select(f => new foundUserDto()
            {
                FirstName = f.FirstName,
                LastName = f.LastName,
                BirthDate = DateOnly.FromDateTime(f.BirthDate),
                Gender = f.Gender,

            })
            .ToList();

            return foundUserDtoList;
        }

        public async Task UpdateUser(UpdateUserRequest request)
        {
            var foundUser = await _dbContext.Users.Include(x => x.AdditionalAttributes).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (foundUser is null)
            {
                throw new Exception("User doesn't exists");
            }
            var t = TimeOnly.Parse("00:00:00");

            foundUser.FirstName = request.FirstName;
            foundUser.LastName = request.LastName;
            foundUser.BirthDate = request.BirthDate.ToDateTime(t);
            foundUser.Gender = request.LastName;

            _dbContext.Users.Update(foundUser);

            var recordsToDelete = _dbContext.AdditionalAttributes.Where(x => x.UserId == request.Id).ToList();
            _dbContext.AdditionalAttributes.RemoveRange(recordsToDelete);

            if (!request.AdditionalAttributes.IsNullOrEmpty())
            {
                foreach (var attribute in request.AdditionalAttributes)
                {
                    var newAttribute = new AdditionalAttribute
                    {
                        AttributeName = attribute.AttributeName,
                        Value = attribute.Value,
                        UserId = foundUser.Id
                    };
                    await _dbContext.AdditionalAttributes.AddAsync(newAttribute);
                }
            }
            _dbContext.Users.Update(foundUser);
            await _dbContext.SaveChangesAsync();
        }

        public Task GenerateRaport(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
