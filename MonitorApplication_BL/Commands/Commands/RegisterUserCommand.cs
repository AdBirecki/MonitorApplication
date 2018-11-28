using System;
using System.Collections.Generic;
using System.Text;
using MonitorApplication_BL.Commands.Interfaces;

namespace MonitorApplication_BL.Commands.RegisterCommand
{
    /* Command that handles user registration. This command contains
     * logic necessary to validate command as well as Fields necessary for user creation.*/
    public class RegisterUserCommand : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public RegisterUserCommand() {

        }

        public RegisterUserCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public RegisterUserCommand(string userName, string password, string name, string surname): this(userName, password)
        {
            Name = name;
            Surname = surname;
        }

        /* Simplified validation. I check for username and password length   */
        public bool IsValid()
        {
            bool validationResult = false;

            if (!string.IsNullOrEmpty(UserName) 
                && !string.IsNullOrEmpty(Password) 
                && Password.Length > 4  ) {
                validationResult = true;
            }

            return validationResult;
        }
    }
}
