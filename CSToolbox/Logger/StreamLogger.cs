using System;
using System.IO;
using System.Text;

namespace CSToolbox.Logger
{
    public class StreamLogger : LoggerBase
    {
        public Encoding Encoding { get; }
        protected Stream OutStream { get; }

        public StreamLogger(Stream stream) : this(stream, Encoding.UTF8) { }

        public StreamLogger(Stream stream, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(stream);
            ArgumentNullException.ThrowIfNull(encoding);

            Encoding = encoding;
            OutStream = stream;
        }

        public override void Dispose()
        {
            OutStream.Flush();
            OutStream.Dispose();
        }

        public override void Write(string? message)
        {
            message ??= "null";
            OutStream.Write(Encoding.GetBytes(message));
        }
    }
}
