using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using System.IO;
using PrismAPI;

namespace FalloutOS
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
            VFSManager.RegisterVFS(fs);
            UserSystem userSystem = new UserSystem();
            userSystem.AddUser("root", "root");
            Directory.SetCurrentDirectory(@"0:\");
            Console.WriteLine("=====================================================");
            var available_space = fs.GetAvailableFreeSpace(@"0:\");
            var fs_type = fs.GetFileSystemType(@"0:\");
            Console.WriteLine("Available Free Space: " + available_space);
            Console.WriteLine("File System Type: " + fs_type);
            Console.WriteLine("=====================================================");
            Console.WriteLine(@"
   ____     ____          __  ____  ____
  / __/__ _/ / /__  __ __/ /_/ __ \/ __/
 / _// _ `/ / / _ \/ // / __/ /_/ /\ \  
/_/  \_,_/_/_/\___/\_,_/\__/\____/___/  
                                        ");

            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                Console.WriteLine("Current Directory: " + currentDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting current directory: {ex.Message}");
            }

            Console.WriteLine("FalloutOS booted successfully.");
        }



        protected override void Run()
        {
            var currentuser = "root";
            var host = "fallout";
            Console.Write($"\n[ {currentuser}@{host} ]\n{Directory.GetCurrentDirectory()} $ ");
            string input = Console.ReadLine();
            string[] args = input.Split(' ');

            if (args.Length > 0)
            {
                string command = args[0].ToLower();

                switch (command)
                {
                    case "cd":
                        Commands.Cd(args);
                        break;
                    case "ls":
                        Commands.Ls();
                        break;
                    case "help":
                        Commands.Help();
                        break;
                    case "exit":
                        Sys.Power.Shutdown();
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "mkdir":
                        Commands.Mkdir(args);
                        break;
                    case "rm":
                        Commands.Rm(args);
                        break;
                    case "rmdir":
                        Commands.Rmdir(args);
                        break;
                    case "cat":
                        Commands.Cat(args);
                        break;
                    case "touch":
                        Commands.Touch(args);
                        break;
                    case "date":
                        Console.WriteLine(DateTime.Now);
                        break;
                    default:
                        Console.WriteLine($"Command '{command}' not recognized. Type 'help' for available commands.");
                        break;
                }
            }
        }

    }
}
