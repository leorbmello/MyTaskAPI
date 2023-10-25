namespace LeoMello.Shared.Exceptions.Models
{
    /// <summary>
    ///     The error message model, to display informations to the user.
    /// </summary>
    public class ExceptionErrorMessage
    {
        public string Code { get; private set; }

        public string Message { get; set; }

        public ExceptionErrorMessage()
        {
        }

        public ExceptionErrorMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    /// <summary>
    ///     References of all possible excetions to display.
    /// </summary>
    public static class ExceptionCode
    {
        public const string
            UnknownError = "00_00",                 // invalid error, if is thrown, means that we have a not handled exception
            AuthConfigMissing = "00_01",            // configuration file do not have the AuthConfig information
            AuthInvalidUser = "01_01",              // username is nulled on the authentication model.
            AuthUnknownUser = "01_02",              // username has been not found on database.
            AuthFailed = "01_03",                   // failed to authenticate the user.
                                  //AuthTokenUserInvalid = "01_04", // obsolete, maybe....
            AuthTokenCreationFailed = "01_05",      // failed to generate the authentication token, contact the support!
            UserAlreadyExist = "02_01",             // user registration failure, user already exists.
            UserRoleAssingFail = "02_01";           // user role assign assert failure.

    }
}
