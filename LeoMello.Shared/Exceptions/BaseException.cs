using LeoMello.Shared.Exceptions.Models;
using System.Runtime.Serialization;

namespace LeoMello.Shared.Exceptions
{
    [Serializable]
    public abstract class BaseException : Exception
    {
        protected static ExceptionErrorMessage DefaultUknownError = new ExceptionErrorMessage(ExceptionCode.UnknownError, "Unknown Error!");

        public IEnumerable<ExceptionErrorMessage> Errors { get; private set; }

        protected BaseException(ExceptionErrorMessage error)
            : this(new ExceptionErrorMessage[] { error })
        {
        }

        protected BaseException(IEnumerable<ExceptionErrorMessage> errors)
            : base(string.Join("\n", errors?.Select(error => error.Message)))
        {
            Errors = errors;
        }

        protected BaseException(string code, string message)
            : this(new ExceptionErrorMessage(code, message))
        {
        }

        protected BaseException(ExceptionErrorMessage error, Exception innerException)
            : this(new ExceptionErrorMessage[] { error }, innerException)
        {
        }

        protected BaseException(string code, string message, Exception innerException)
            : this(new ExceptionErrorMessage(code, message), innerException)
        {
        }

        protected BaseException(IEnumerable<ExceptionErrorMessage> errors, Exception innerException)
            : base(string.Join("\n", errors?.Select(error => error.Message)), innerException)
        {
            Errors = errors;
        }

        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
