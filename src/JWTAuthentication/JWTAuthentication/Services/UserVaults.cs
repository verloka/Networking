using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JWTAuthentication.Services
{
    public static class UserVaults
    {
        private static Dictionary<string, string> _users { get; set; }

        static UserVaults()
        {
            try
            {
                using var sr = new StreamReader("user_vault.json");
                var json = sr.ReadToEnd();
                _users = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool ContainsCredentials(string userName, string password)
        {
            if (_users.ContainsKey(userName) && _users.TryGetValue(userName, out string storedPassword))
                    return storedPassword.Equals(password);

            return false;
        }
    }
}
