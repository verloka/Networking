using ADAuthentication.Enums;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Text.RegularExpressions;

namespace ADAuthentication.Services
{
    public class ActiveDirectoryService
    {
        static Regex ldapErrorCodeCapture = new Regex(@", data (\w+),", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        /// <summary>
        /// Validates a username and password against the Active Directory domain.
        /// </summary>
        /// <param name="domain">The domain to authenticate against.</param>
        /// <param name="username">The username to check.</param>
        /// <param name="password">The password to check.</param>
        /// <returns>The status returned by the domain controller.</returns>
        public AccountAuthenticationResponse Authenticate(string domain, string username, string password)
        {
            try
            {
                using (LdapConnection connection = new LdapConnection(domain) { Credential = new NetworkCredential(username, password) })
                {
                    connection.Bind();
                }
                return AccountAuthenticationResponse.Success;
            }
            catch (LdapException ex)
            {
                if(!string.IsNullOrWhiteSpace(ex.ServerErrorMessage))
                {
                    string errorCode = ldapErrorCodeCapture.Match(ex.ServerErrorMessage).Groups[1].Value;
                    switch (errorCode.ToUpperInvariant())
                    {
                        case "525":
                            {
                                return AccountAuthenticationResponse.NotFound;
                            }

                        case "52E":
                            {
                                return AccountAuthenticationResponse.InvalidCredentials;
                            }

                        case "530":
                            {
                                return AccountAuthenticationResponse.LoginNotPermittedTime;
                            }

                        case "531":
                            {
                                return AccountAuthenticationResponse.LoginNotPermittedWorkstation;
                            }

                        case "532":
                            {
                                return AccountAuthenticationResponse.PasswordExpired;
                            }

                        case "533":
                            {
                                return AccountAuthenticationResponse.AccountDisabled;
                            }

                        case "701":
                            {
                                return AccountAuthenticationResponse.AccountExpired;
                            }

                        case "773":
                            {
                                return AccountAuthenticationResponse.ResetPasswordRequired;
                            }

                        case "775":
                            {
                                return AccountAuthenticationResponse.AccountLocked;
                            }
                    }
                }
                
            }

            // If we reached this point, we encountered an unknown condition.
            return AccountAuthenticationResponse.Unknown;
        }
    }
}
