using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSToolbox.Extensions
{
	public static class EncodingExtensions
	{
		private const int BufferSize = 4096;
		private static readonly Dictionary<Encoding, int> _BufferSizesInChars = new Dictionary<Encoding, int>();

		public static int GetBytes(this Encoding encoding, ReadOnlySpan<char> chars, Stream output)
		{
			byte[] buffer = new byte[BufferSize];
			int bufferSizeInChars;

			if (!_BufferSizesInChars.TryGetValue(encoding, out bufferSizeInChars))
				_BufferSizesInChars[encoding] = bufferSizeInChars = GetBufferSizeInChars(encoding, BufferSize);

			int consumedChars = 0;
			int bytesWritten;
			int totalBytesWritten = 0;
			int charCount;
			while (consumedChars < chars.Length)
			{
				charCount = Math.Min(chars.Length - consumedChars, bufferSizeInChars);
				bytesWritten = encoding.GetBytes(chars.Slice(consumedChars, charCount), buffer);
				output.Write(buffer, 0, bytesWritten);
				consumedChars += charCount;
				totalBytesWritten += bytesWritten;
			}

			return totalBytesWritten;
		}

		private static int GetBufferSizeInChars(Encoding encoding, int bufferSize)
		{
			//Appoximate result
			int result = bufferSize/encoding.GetMaxByteCount(1);

			while ( encoding.GetMaxByteCount(result) > bufferSize);
				result--;

			return result;
		}
	}
}
