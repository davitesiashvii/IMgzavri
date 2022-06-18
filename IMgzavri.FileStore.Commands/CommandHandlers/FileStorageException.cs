using IMgzavri.Shared.Domain.Models;
using System.Runtime.Serialization;

namespace IMgzavri.FileStore.Commands.CommandHandlers
{
    [Serializable]
    internal class FileStorageException : Exception
    {
        private string v;
        private ExceptionLevel fatal;

        public FileStorageException()
        {
        }

        public FileStorageException(string message) : base(message)
        {
        }

        public FileStorageException(string v, ExceptionLevel fatal)
        {
            this.v = v;
            this.fatal = fatal;
        }

        public FileStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}