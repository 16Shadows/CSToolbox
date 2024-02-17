using System;

namespace CSToolbox.Logger
{
    public abstract class LoggerBase : IDisposable
    {
        public abstract void Write(string? message);
        public virtual void Dispose() { }

        public void WriteLine() =>
            Write(Environment.NewLine);
        public void WriteLine(string? message) =>
            Write($"{message ?? "null"}{Environment.NewLine}");

        public void Write(object obj) =>
            Write(obj?.ToString());
        public void WriteLine(object obj) =>
            WriteLine(obj?.ToString());

        public void WriteFormatted(string pattern, params object[] objs) =>
            Write(string.Format(pattern, objs));
        public void WriteLineFormatted(string pattern, params object[] objs) =>
            WriteLine(string.Format(pattern, objs));
    }
}
