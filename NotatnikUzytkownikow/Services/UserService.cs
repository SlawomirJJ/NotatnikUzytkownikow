using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotatnikUzytkownikow.Dtos;
using NotatnikUzytkownikow.Entities;
using NotatnikUzytkownikow.Interfaces;
using NotatnikUzytkownikow.Requests;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System.Data;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace NotatnikUzytkownikow.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task CreateUser(CreateUserRequest request)
        {
            var t = TimeOnly.Parse("00:00:00");
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender.ToString(),
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
        
        public async Task<List<FoundUserDto>> GetAllUsers()
        {
            var foundUsers = await _dbContext.Users.Include(x => x.AdditionalAttributes).ToListAsync();

            var foundUserDtoList = foundUsers.Select(f => new FoundUserDto()
            {
                FirstName = f.FirstName,
                LastName = f.LastName,
                BirthDate = DateOnly.FromDateTime(f.BirthDate),
                Gender = f.Gender,
                AdditionalAttributes = _mapper.Map<List<AdditionalAttributeRequest>>(f.AdditionalAttributes)

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
            foundUser.Gender = request.Gender.ToString();

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

        public async Task<Guid> GetUserId(CreateUserRequest request)
        {
            var t = TimeOnly.Parse("00:00:00");

            Guid foundId = await _dbContext.Users
                .Where(x => x.BirthDate == request.BirthDate.ToDateTime(t)
                    && x.FirstName == request.FirstName
                    && x.Gender == request.Gender.ToString()
                    && x.LastName == request.LastName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return foundId;
        }

        public async Task<byte[]> GenerateReport()
        {
            var document = new PdfDocument();

            string htmlContent = "<html><body>";

            htmlContent += "<table border='1'>";
            htmlContent += "<thead><tr>";
            htmlContent += "<th>Imię</th>";
            htmlContent += "<th>Nazwisko</th>";
            htmlContent += "<th>Data urodzenia</th>";
            htmlContent += "<th>Płeć</th>";
            htmlContent += "<th>Tytuł</th>";
            htmlContent += "<th>Wiek</th>";
            htmlContent += "</tr></thead>";
            htmlContent += "<tbody>";

            var users = await GetAllUsers();

            foreach (var user in users)
            {
                htmlContent += "<tr>";
                htmlContent += $"<td>{user.FirstName}</td>";
                htmlContent += $"<td>{user.LastName}</td>";
                htmlContent += $"<td>{user.BirthDate.ToShortDateString()}</td>";
                htmlContent += $"<td>{user.Gender}</td>";
                htmlContent += $"<td>{(user.Gender == "Male" ? "Pan" : "Pani")}</td>";
                htmlContent += $"<td>{(DateTime.Now.Year - user.BirthDate.Year)}</td>";
                htmlContent += "</tr>";
            }

            htmlContent += "</tbody></table>";
            htmlContent += "</body></html>";

            PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
            byte[] responseBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                responseBytes = ms.ToArray();
            }

            return responseBytes;
        }

    }
}
