using LeoMello.Shared.Exceptions.Models;
using System.Runtime.Serialization;

namespace LeoMello.Shared.Exceptions.Authorization
{
    public class AuthorizationException : BaseException
    {
        public AuthorizationException(ExceptionErrorMessage error)
            : base(error)
        {
        }

        public AuthorizationException(ExceptionErrorMessage error, Exception innerException)
            : base(error, innerException)
        {
        }

        public AuthorizationException(IEnumerable<ExceptionErrorMessage> errors, Exception innerException)
            : base(errors, innerException)
        {
        }

        public AuthorizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ExceptionErrorMessage GetFirstError()
        {
            return Errors?.Count() > 0 ? 
                Errors.First() :
                DefaultUknownError;
        }

        public IEnumerable<ExceptionErrorMessage> GetErrors()
        {
            return Errors?.Count() > 0 ?
                Errors.ToList() :
                new List<ExceptionErrorMessage>() 
                {
                    DefaultUknownError
                };
        }
    }
}
