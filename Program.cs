using System;
using System.IO;

namespace UsingTheIDisposable
{
	public class Program
	{
		public static void Main()
		{
			//Part I.
			//ShowCaseTemporaryDirectory.Run();
			//ShowCaseTemporaryDirectory.RunWithoutFluff();
		}
	}

#region Part I.

	public class TemporaryDirectory : IDisposable
	{
		public string Path { get; }

		public TemporaryDirectory(string directoryPath)
		{
			Path = directoryPath;

			Directory.CreateDirectory(Path);
		}

		public void Dispose()
		{
			Directory.Delete(Path, true);
		}
	}

	public class ApplicationStorage
	{
		public TemporaryDirectory CreateTemporaryDirectory()
		{
			var tempPath = Path.GetTempPath();
			var tempDirectoryName = Path.GetRandomFileName();
			var tempDirectoryFullPath = Path.Combine(
				tempPath,
				"MyAwesomeInc",
				tempDirectoryName);

			return new TemporaryDirectory(tempDirectoryFullPath);
		}
	}

	public static class ShowCaseTemporaryDirectory
	{
		public static void Run()
		{
			var applicationStorage = new ApplicationStorage();

			using (var temporaryDirectory = applicationStorage.CreateTemporaryDirectory())
			{
				var filePath = Path.Combine(temporaryDirectory.Path, "content.txt");
				File.WriteAllText(filePath, "Adding new content");

				/*
				 * Doing serious business here. Really.
				 * Temporary computations, file manipulations and what not.
				 */

				/*
				 * Persisting the result to a permanent storage.
				 */

			} //Put a breakpoint here, and check your %temp%\MyAwesomeInc\ folder

			//Check again your %temp%\MyAwesomeInc\
		}

		public static void RunWithoutFluff()
		{
			var tempPath = Path.GetTempPath();
			var tempDirectoryName = Path.GetRandomFileName();
			var tempDirectoryFullPath = Path.Combine(
				tempPath,
				"AwesomeInc",
				tempDirectoryName);

			try
			{
				Directory.CreateDirectory(tempDirectoryFullPath);

				var filePath = Path.Combine(tempDirectoryFullPath, "content.txt");
				File.WriteAllText(filePath, "Adding new content");

				/*
				 * Doing serious business here. Really.
				 * Temporary computations, file manipulations and what not.
				 */

				/*
				 * Persisting the result to a permanent storage.
				 */
			}
			finally
			{
				Directory.Delete(tempDirectoryFullPath, true);
			}
		}
	}

#endregion
}
