using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sys = Cosmos.System;
using System.Text;
using System.Threading.Tasks;

namespace FalloutOS
{
    internal class Commands
    {
        public static void Mkdir(string[] args)
        {
            if (args.Length > 1)
            {
                string newDirectory = string.Join(" ", args.Skip(1));

                try
                {
                    Directory.CreateDirectory(newDirectory);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating directory: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: mkdir <directory>");
            }
        }

        public static void Rm(string[] args)
        {
            if (args.Length > 1)
            {
                string fileToDelete = string.Join(" ", args.Skip(1));

                try
                {
                    File.Delete(fileToDelete);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: rm <file>");
            }
        }

        public static void Rmdir(string[] args)
        {
            if (args.Length > 1)
            {
                string directoryToDelete = string.Join(" ", args.Skip(1));

                try
                {
                    Directory.Delete(directoryToDelete);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting directory: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: rmdir <directory>");
            }
        }

        public static void Cat(string[] args)
        {
            if (args.Length > 1)
            {
                string fileToRead = string.Join(" ", args.Skip(1));

                try
                {
                    string content = File.ReadAllText(fileToRead);
                    Console.WriteLine(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: cat <file>");
            }
        }

        public static void Touch(string[] args)
        {
            if (args.Length > 1)
            {
                string newFile = string.Join(" ", args.Skip(1));

                try
                {
                    File.Create(newFile).Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: touch <file>");
            }
        }

        public static void Cd(string[] args)
        {
            if (args.Length > 1)
            {
                string newDirectory = string.Join(" ", args.Skip(1));

                if (newDirectory == "..")
                {
                    // Move up one directory level
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string parentDirectory = Path.GetDirectoryName(currentDirectory);

                    if (!string.IsNullOrEmpty(parentDirectory))
                    {
                        try
                        {
                            Directory.SetCurrentDirectory(parentDirectory);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Console.WriteLine($"Error: Access denied to change to parent directory.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error changing to parent directory: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: Already at the root directory.");
                    }
                }
                else
                {
                    try
                    {
                        Directory.SetCurrentDirectory(newDirectory);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"Error: Access denied to change to directory '{newDirectory}'.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error changing directory: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Usage: cd <directory>");
            }
        }



        public static void Echo(string[] args)
        {
            if (args.Length > 0)
            {
                string message = string.Join(" ", args).Replace("echo", "");
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Usage: Echo <message>");
            }
        }


        public static void Ls()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string[] directories = Directory.GetDirectories(currentDirectory);
                string[] files = Directory.GetFiles(currentDirectory);

                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var directory in directories)
                {
                    Console.WriteLine($"  {Path.GetFileName(directory)}");
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (var file in files)
                {
                    Console.WriteLine($"  {Path.GetFileName(file)}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking directory content: {ex.Message}");
            }
        }

        private static string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int counter = 0;
            decimal number = bytes;

            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }

            return $"{number:n1} {suffixes[counter]}";
        }

        public static void Grep(string[] args)
        {
            if (args.Length == 3)
            {
                string pattern = args[1];
                string fileToSearch = args[2];

                try
                {
                    string[] lines = File.ReadAllLines(fileToSearch);
                    foreach (var line in lines)
                    {
                        if (line.Contains(pattern))
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error searching file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Usage: grep <pattern> <file>");
            }
        }

        public static void Help()
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║     -==== Available Commands ====-     ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            Console.WriteLine("\n[ Navigation ]");
            Console.WriteLine("  cd <directory>   - Change current directory");
            Console.WriteLine("  ls               - List files and directories in the current directory");
            Console.WriteLine("  history          - Shows history of executed commands");

            Console.WriteLine("\n[ File Operations ]");
            Console.WriteLine("  clear            - Clear the console screen");
            Console.WriteLine("  mkdir <directory>- Create a new directory");
            Console.WriteLine("  rm <file>        - Remove (delete) a file");
            Console.WriteLine("  rmdir <directory>- Remove (delete) a directory");
            Console.WriteLine("  cat <file>       - Display the content of a file");
            Console.WriteLine("  touch <file>     - Create an empty file");
            Console.WriteLine("  miv              - Open MIV - MInimalistic Vi ");
            Console.WriteLine("  grep <pattern> <file>     - Search for a pattern in a file");
            Console.WriteLine("  find <directory> -name <filename> - Search for a file by name");
            Console.WriteLine("  cp <source> <destination> - Copy a file or directory");
            Console.WriteLine("  mv <source> <destination> - Move (rename) a file or directory");

            Console.WriteLine("\n[ System Operations ]");
            Console.WriteLine("  reboot           - Reboots the system");
            Console.WriteLine("  help             - Show available commands");
            Console.WriteLine("  beep <freq> <dur>- Play a beep sound with specified frequency and duration");
            Console.WriteLine("  exit             - Shutdown the system");
        }

    }
}
