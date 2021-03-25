using System;
using System.Text;
using qsLibPack.Domain.Entities;
using qsLibPack.Domain.Exceptions;
using qsLibPack.Domain.ValueObjects;
using qsLibPack.Validations;

namespace qsLog.Domains.Users
{
    public class User : AggregateRoot<Guid>
    {
        protected User() {}

        public User(string name, string userName, EmailVO email, PasswordVO password, bool administrator)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserName = userName;
            Email = email.ToString();
            Password = password.ToString();
            Administrator = administrator;

            this.Validate();
            this.Password = this.CriptPassword(password.ToString());
        }

        public string Name { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool Administrator {get; private set; } = false;

        public void SetName(string value)
        {
            this.Name = value;
            this.Validate();
        }

        public void SetEmail(EmailVO value)
        {
            this.Email = value.ToString();
            this.Validate();
        }

        public void SetUserName(string value)
        {
            this.UserName = value;
            this.Validate();
        }

        public void SetAdministrator(bool value)
        {
            this.Administrator = value;
        }

        public void SetPassword(PasswordVO value)
        {
            this.Password = value.ToString();
            this.Validate();
            this.Password = this.CriptPassword(value.ToString());
        }

        public void ChangePassoword(string oldPassword, PasswordVO newPassword)
        {
            oldPassword = CriptPassword(oldPassword);
            if (!oldPassword.Equals(this.Password)) 
                throw new DomainException("Sua senha antiga não confere.");
            
            this.Password = CriptPassword(newPassword.ToString());
            this.Validate();
        }

        public void ResetPassword()
        {
            this.Password = this.CriptPassword("123456");
        }

        public bool PasswordEquals(string password)
        {
            var criptPassword = CriptPassword(password);
            return this.Password.Equals(criptPassword);
        }

        protected override void Validate()
        {
            this.Id.NotNullOrEmpty("Id nao gerado para o projeto.");
            this.Name.NotNullOrEmpty();
            this.Password.NotNullOrEmpty();
            this.Email.NotNullOrEmpty();
            this.UserName.NotNullOrEmpty();
        }

        private string CriptPassword(string password)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            
            return sb.ToString();
        }
    }
}