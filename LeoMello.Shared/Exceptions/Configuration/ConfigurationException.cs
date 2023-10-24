using LeoMello.Shared.Exceptions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LeoMello.Shared.Exceptions.Configuration
{
    public class ConfigurationException : BaseException
    {
        public ConfigurationException(ExceptionErrorMessage error)
            : base(error)
        {
        }

        public ConfigurationException(ExceptionErrorMessage error, Exception innerException)
            : base(error, innerException)
        {
        }

        public ConfigurationException(IEnumerable<ExceptionErrorMessage> errors, Exception innerException)
            : base(errors, innerException)
        {
        }

        public ConfigurationException(SerializationInfo info, StreamingContext context)
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
