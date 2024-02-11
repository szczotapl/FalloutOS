using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutOS
{
    internal class Commands
    {
        public static void  Mkdir(string[] args)
        {
            if (args.Length > 1)
            {
                string newDirectory = args[1];
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), newDirectory);

                try
                {
                    Directory.CreateDirectory(fullPath);
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

        public static void  Rm(string[] args)
        {
            if (args.Length > 1)
            {
                string fileToDelete = args[1];
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileToDelete);

                try
                {
                    File.Delete(fullPath);
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

        public static void  Rmdir(string[] args)
        {
            if (args.Length > 1)
            {
                string directoryToDelete = args[1];
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), directoryToDelete);

                try
                {
                    Directory.Delete(fullPath);
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

        public static void  Cat(string[] args)
        {
            if (args.Length > 1)
            {
                string fileToRead = args[1];
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileToRead);

                try
                {
                    string content = File.ReadAllText(fullPath);
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

        public static void  Touch(string[] args)
        {
            if (args.Length > 1)
            {
                string newFile = args[1];
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), newFile);

                try
                {
                    File.Create(fullPath).Close();
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

        public static void  Cd(string[] args)
        {
            if (args.Length > 1)
            {
                string newDirectory = args[1];

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
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), newDirectory);

                    try
                    {
                        if (Directory.Exists(fullPath))
                        {
                            Directory.SetCurrentDirectory(fullPath);
                        }
                        else
                        {
                            Console.WriteLine($"Error: Directory '{newDirectory}' not found.");
                        }
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



        public static void  Ls()
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string[] files = Directory.GetFiles(currentDirectory);
                string[] directories = Directory.GetDirectories(currentDirectory);

                Console.WriteLine($"\n[  Files in {currentDirectory}:  ]");
                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }

                Console.WriteLine($"\n[  Directories in {currentDirectory}:  ]");
                foreach (var directory in directories)
                {
                    Console.WriteLine(directory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking directory content: {ex.Message}");
            }
        }


        public static void  Help()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("cd <directory> - Change current directory");
            Console.WriteLine("ls - List files and directories in the current directory");
            Console.WriteLine("clear - Clear the console screen");
            Console.WriteLine("mkdir <directory> - Create a new directory");
            Console.WriteLine("rm <file> - Remove (delete) a file");
            Console.WriteLine("rmdir <directory> - Remove (delete) a directory");
            Console.WriteLine("cat <file> - Display the content of a file");
            Console.WriteLine("touch <file> - Create an empty file");
            Console.WriteLine("help - Show available commands");
            Console.WriteLine("exit - Shutdown the system");
        }
    }
}
