using LeoMello.Shared.Exceptions.Models;
using System.Runtime.Serialization;

namespace LeoMello.Shared.Exceptions.Creation
{
    public class CreationException : BaseException
    {
        public CreationException(ExceptionErrorMessage error)
            : base(error)
        {
        }

        public CreationException(ExceptionErrorMessage error, Exception innerException)
            : base(error, innerException)
        {
        }

        public CreationException(IEnumerable<ExceptionErrorMessage> errors, Exception innerException)
            : base(errors, innerException)
        {
        }

        public CreationException(SerializationInfo info, StreamingContext context)
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
