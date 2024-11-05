using BusinessObjects.Entities.Enums;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAuthService
    {
        Task<AppResponse<bool>> SignInAsync(string email, string password);
        Task<AppResponse<bool>> RegisterAsync(string email, string password, Gender gender);
        Task<AppResponse<bool>> LogoutAsync();
    }
}
