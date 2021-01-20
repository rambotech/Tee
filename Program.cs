using System;
using System.IO;

namespace Tee
{
	class Program
	{
		/// <summary>
		/// A very simple form of Linux tee.  It will echo standard input to the console, and if an argument is supplied, writes the
		/// content of standard input to the file name specified AFTER the stdin stream hits EOF.  The output is always written to a
		/// temp file and the console.  It will rename the temporary file to the namein the first argument before exiting.
		/// </summary>
		/// <param name="args">outfile file (optional, but weird to use Tee when not provided)</param>
		static void Main(string[] args)
		{
			try
			{
				var tempOutput = Path.GetTempFileName();
				using (var writer = new StreamWriter(tempOutput))
				{
					using (var reader = new StreamReader(Console.OpenStandardInput()))
					{
						// Redirect standard output from the console to the output file.
						/// Console.SetOut(writer);
						// Redirect standard input from the console to the input file.
						Console.SetIn(reader);
						string line;
						while ((line = Console.ReadLine()) != null)
						{
							Console.WriteLine(line);
							writer.WriteLine(line);
						}
					}
				}
				if (args.Length >= 1)
				{
					if (File.Exists(args[0])) File.Delete(args[0]);
					File.Copy(tempOutput, args[0]);
					File.Delete(tempOutput);  // only done when the file name was specified, and it was successfully copied.
				}
				Environment.ExitCode = 0;
			}
			catch (Exception boom)
			{
				Console.WriteLine($"BOOM... there it is: {boom.Message}");
				Environment.ExitCode = 1;
			}
		}
	}
}
