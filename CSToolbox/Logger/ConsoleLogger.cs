using System;

namespace CSToolbox.Logger
{
	public class ConsoleLogger : LoggerBase
	{
		public override void Write(string? message)
		{
			Console.Write(message);
		}
	}
}
