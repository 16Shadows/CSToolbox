using System.IO;
using System.Text;

namespace CSToolbox.Logger
{
    public class FileLogger : StreamLogger
    {
        private static readonly Encoding _DefaultEncoding = Encoding.UTF8;

        public FileLogger(string path) : this(path, _DefaultEncoding) { }
        public FileLogger(string path, bool wipeFile) : this(path, _DefaultEncoding, wipeFile) { }
        public FileLogger(string path, Encoding encoding) : this(path, encoding, true) { }
        public FileLogger(string path, Encoding encoding, bool wipeFile) : base(new FileStream(path, wipeFile ? FileMode.Create : FileMode.OpenOrCreate, FileAccess.Write), encoding) { }
    }
}
