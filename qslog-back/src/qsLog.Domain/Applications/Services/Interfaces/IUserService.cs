using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qsLibPack.Domain.ValueObjects;
using qsLog.Applications.Models;

namespace qsLog.Applications.Services.Interfaces
{
    public interface IUserService
    {
        Task<Guid> Create(UserModel model);
        Task Update(UserModel model, Guid id);
        Task<UserModel> GetByID(Guid id);
        IList<UserListModel> ListAll();
        IList<UserListModel> List(string search);
        Task ChangePassoword(Guid id, string oldPassword, PasswordVO newPassword);
        Task ResetPassword(Guid id);
        Task<Guid> CreateAdminUser();
        Task Remove(Guid id);
    }
}