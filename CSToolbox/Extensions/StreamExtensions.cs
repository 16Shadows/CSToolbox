using System.IO;

namespace CSToolbox.Extensions
{
	public static class StreamExtensions
	{
		public static void Write(this Stream thisStream, Stream stream)
		{
			const int chunkSize = 8*1024;

			//Write in chunks of 8KB
			byte[] buffer = new byte[chunkSize];

			int readBytes;
			while((readBytes = stream.Read(buffer, 0, chunkSize)) > 0)
				thisStream.Write(buffer, 0, readBytes);
		}
	}
}
