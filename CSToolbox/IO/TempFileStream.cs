using System.IO;

namespace CSToolbox.IO
{
	/// <summary>
	/// A simple implementation which creates a temporary file when created and destroys it when disposed of.
	/// </summary>
	public class TempFileStream : FileStream
	{
		public TempFileStream() : base(Path.GetTempFileName(), FileMode.Open, FileAccess.ReadWrite) {}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			File.Delete(Name);
		}
	}
}
