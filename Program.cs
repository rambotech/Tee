using System;
using System.IO;
using System.Text;

namespace Tee
{
    class Program
    {
        /// <summary>
        /// 
        /// A very simple form of Linux tee.  It will echo standard input to the console, and if an argument is supplied, writes the
        /// content of standard input to the file name specified AFTER the stdin stream hits EOF.  The output is always written to a
        /// temp file and the console.  It will rename the temporary file to the namein the first argument before exiting.
        /// </summary>
        /// <param name="args">[--append] Outfile</param>
        /// <remarks>
        /// outfile is the file name to write the output to.  If --append is present, it is appended to the existing
        /// Outfile if it exists.  The order of the arguments is irrelevant.
        /// </remarks>
        /// <example>c:\temp\myoutput.txt</example>
        /// <example>c:\temp\myoutput.txt --append</example>
		/// <example>--append c:\temp\myoutput.txt</example>
        static void Main(string[] args)
        {
            string outputFile = string.Empty;
            var appendToFile = false;
            var haveFileName = false;
            var haveAppendArg = false;
            var validArguments = true;
            var index = 0;

            while (index < args.Length)
            {
                if (args[index] == "--append")
                {
                    if (haveAppendArg)
                    {
                        validArguments = false;
                        Console.WriteLine("argument --append appears more than once");
                    }
                    else
                    {
                        appendToFile = true;
                        haveAppendArg = true;
                    }
                }
                else
                {
                    if (haveFileName)
                    {
                        validArguments = false;
                        Console.WriteLine("argument for outfile appears more than once.");
                    }
                    else
                    {
                        haveFileName = true;
                        outputFile = args[index];
                    }
                }
                index++;
            }

            if (!haveFileName)
            {
                validArguments = false;
                Console.WriteLine("no argument found for outfile: the  appears more than once.");
            }

            if (validArguments)
            {
                try
                {
                    using (var writer = new StreamWriter(outputFile, appendToFile))
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
                    Environment.ExitCode = 0;
                }
                catch (Exception boom)
                {
                    Console.WriteLine($"BOOM... there it is: {boom.Message}");
                    Environment.ExitCode = 1;
                }
            }
            else
            {
                Console.WriteLine();
                Environment.ExitCode = 2;
            }
        }
    }
}
