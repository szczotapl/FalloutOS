using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using System.IO;
using PrismAPI;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using Cosmos.System.FileSystem;

namespace FalloutOS
{
    public class Kernel : Sys.Kernel
    {
        private static readonly ConsoleColor Blue = ConsoleColor.Blue;
        private static readonly ConsoleColor DarkBlue = ConsoleColor.DarkBlue;
        private static readonly ConsoleColor White = ConsoleColor.White;
        private static readonly ConsoleColor Red = ConsoleColor.Red;
        private static readonly ConsoleColor DarkRed = ConsoleColor.DarkRed;
        private static readonly ConsoleColor Green = ConsoleColor.Green;
        private static readonly ConsoleColor DarkGreen = ConsoleColor.DarkGreen;
        private static readonly ConsoleColor Yellow = ConsoleColor.Yellow;
        private static readonly ConsoleColor DarkYellow = ConsoleColor.DarkYellow;
        private static readonly ConsoleColor Cyan = ConsoleColor.Cyan;
        private static readonly ConsoleColor DarkCyan = ConsoleColor.DarkCyan;
        private static readonly ConsoleColor Magenta = ConsoleColor.Magenta;
        private static readonly ConsoleColor DarkMagenta = ConsoleColor.DarkMagenta;
        public static string file;
        public static Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        protected override void BeforeRun()
        {
            VFSManager.RegisterVFS(fs);
            UserSystem userSystem = new UserSystem();
            userSystem.AddUser("root", "root");
            var availableSpace = fs.GetAvailableFreeSpace(@"0:\");
            var fsType = fs.GetFileSystemType(@"0:\");
            Directory.SetCurrentDirectory(@"0:\");
            Console.ForegroundColor = Green;
            Console.WriteLine("Booted FalloutOS!");
            PlayNote(440, 150); // A4
            PlayNote(494, 150); // B4
            PlayNote(523, 150); // C5
            PlayNote(587, 150); // D5
            PlayNote(659, 150); // E5
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("=======================================================================");
            Console.WriteLine($"Available Free Space: {availableSpace}");
            Console.WriteLine($"File System Type: {fsType}");
            Console.WriteLine("=======================================================================");
            Console.ForegroundColor = DarkBlue;
            Console.WriteLine(@"
   ____     ____          __  ____  ____   |
  / __/__ _/ / /__  __ __/ /_/ __ \/ __/   |   By .riviox
 / _// _ `/ / / _ \/ // / __/ /_/ /\ \     |
/_/  \_,_/_/_/\___/\_,_/\__/\____/___/     |
                                           |
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

        static void PlayNote(int frequency, int duration)
        {
            Console.Beep(frequency, duration);
            Thread.Sleep(duration);
        }

        protected override void Run()
        {
            var currentuser = "root";
            var host = "fallout";
            Console.ForegroundColor = DarkGreen;
            Console.Write($"\n[ {currentuser}@{host} ]");
            Console.ForegroundColor = Cyan;
            Console.Write($"\n{Directory.GetCurrentDirectory()} $ ");
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
                    case "beep":
                        if (args.Length == 3 && int.TryParse(args[1], out int frequency) && int.TryParse(args[2], out int duration))
                        {
                            PlayNote(frequency, duration);
                        }
                        else
                        {
                            Console.WriteLine("Invalid arguments. Usage: beep <frequency> <duration>");
                        }
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
                    case "miv":
                        MIV.StartMIV();
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "sysinfo":
                        Sysinfo();
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
                    case "chuj":
                        Console.WriteLine("Trzepac smyrac napleta dziecku psu");
                        break;
                    case "reboot":
                        Sys.Power.Reboot();
                        break;
                    case "touch":
                        Commands.Touch(args);
                        break;
                    case "echo":
                        Commands.Echo(args);
                        break;
                    case "date":
                        Console.WriteLine(DateTime.Now);
                        break;
                    default:
                        Console.ForegroundColor = Red;
                        Console.WriteLine($"Command '{command}' not recognized. Type 'help' for available commands.");
                        Console.ForegroundColor = White;
                        break;
                }
            }
        }

        public static void Sysinfo()
        {
            var Diskspace = fs.GetTotalSize(@"0:\");
            var fsType = fs.GetFileSystemType(@"0:\");
            if (Cosmos.Core.CPU.GetCPUVendorName().Contains("Intel"))
            {
                Console.WriteLine("88                              88");
                Console.WriteLine("\"\"              ,d              88");
                Console.WriteLine("                88              88");
                Console.WriteLine("88 8b,dPPYba, MM88MMM ,adPPYba, 88");
                Console.WriteLine("88 88P'   `\"8a  88   a8P_____88 88");
                Console.WriteLine("88 88       88  88   8PP\"\"\"\"\"\"\" 88");
                Console.WriteLine("88 88       88  88,  \"8b,   ,aa 88");
                Console.WriteLine("88 88       88  \"Y888 `\"Ybbd8\"' 88");
            }
            else if (Cosmos.Core.CPU.GetCPUVendorName().Contains("AMD"))
            {
                Console.WriteLine("      *@@@@@@@@@@@@@@@    ");
                Console.WriteLine("         @@@@@@@@@@@@@    ");
                Console.WriteLine("        @%       @@@@@    ");
                Console.WriteLine("      @@@%       @@@@@    ");
                Console.WriteLine("     @@@@&       @@@@@    ");
                Console.WriteLine("    @@@@@@@@@     @@@     ");
                Console.WriteLine("     #######              ");
                Console.WriteLine();
                Console.WriteLine("    @@     @\\ /@  @@@@*   ");
                Console.WriteLine("   @..@    @ @ @  @.   @  ");
                Console.WriteLine("  @    @   @   @  @@@@*   ");
            }
            Console.Beep(250, 250);
            Console.Beep(500, 250);
            Console.WriteLine($"{Cosmos.Core.CPU.GetCPUBrandString()}"); // print cpu
            Console.WriteLine($"RAM: {Cosmos.Core.CPU.GetAmountOfRAM()} MB"); // print ram
            Console.Write("VM: ");
            if (Sys.VMTools.IsVMWare)
            {
                Console.Write("VMware\n");
            }
            else if (Sys.VMTools.IsQEMU)
            {
                Console.Write("QEMU (or maybe KVM)\n");
            }
            else if (Sys.VMTools.IsVirtualBox)
            {
                Console.Write("VirtualBox\n");
            }
            else
            {
                Console.Write("Environment isn't virtualized\n");
            }
            Console.Write("Available Free Space: ");
            Console.Write(Diskspace + "\n");
            Console.Write("File System Type: ");
            Console.Write(fsType + "\n");
        }


    }

}
