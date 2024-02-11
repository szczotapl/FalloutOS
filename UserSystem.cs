using System;
using System.Collections.Generic;

namespace FalloutOS
{
    public class UserSystem
    {
        private Dictionary<string, string> users;

        public UserSystem()
        {
            users = new Dictionary<string, string>();
        }

        public void AddUser(string username, string password)
        {
            if (!users.ContainsKey(username))
            {
                users.Add(username, password);
                Console.WriteLine($"User '{username}' added successfully.");
            }
            else
            {
                Console.WriteLine($"Error: User '{username}' already exists.");
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            if (users.TryGetValue(username, out string storedPassword))
            {
                return password == storedPassword;
            }
            return false;
        }

        public bool CanExecuteCommand(string username, string command)
        {
            return true;
        }
    }
}
