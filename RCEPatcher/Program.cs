
using System;
using System.IO;

namespace RCEPatcher
{
    internal class Program
    {
        private static readonly uint PatchOffset = 0x2A7;
        private static readonly byte PatchValue = 0xC0;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("[ERROR] Input your WoW.exe file path to patch. If you are unsure how to use this, drag and drop your WoW.exe onto this program in explorer.\nPress any key to exit.");
                Console.ReadKey();
                return;
            }

            string filePath = args[0];
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("[ERROR] Unable to find file: " + filePath + "\nPress any key to exit.");
                    Console.ReadKey();
                    return;
                }

                if (IsPatchedExe(filePath))
                {
                    Console.WriteLine("[COMPLETE] File is already patched: " + filePath + "\nPress any key to exit.");
                    Console.ReadKey();
                    return;
                }

                string newPath = GetNewPath(filePath);
                if (File.Exists(newPath))
                {
                    Console.WriteLine("[ERROR] File already exists: " + newPath + "\nPress any key to exit.");
                    Console.ReadKey();
                    return;
                }

                PatchExe(filePath, newPath);

                Console.WriteLine("[COMPLETE] Created patched executable: " + newPath + "\nPress any key to exit.");
                Console.ReadKey();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"[ERROR] Unexpected: {exception.Message}, {exception}\n{exception.StackTrace}\nPress any key to exit.");
                Console.ReadKey();
            }
        }

        static bool IsPatchedExe(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            using var reader = new BinaryReader(stream);

            stream.Position = PatchOffset;

            return reader.ReadByte() == PatchValue;
        }

        static void PatchExe(string filePath, string newPath)
        {
            File.Copy(filePath, newPath);

            using var stream = File.OpenWrite(newPath);
            using var writer = new BinaryWriter(stream);

            stream.Position = PatchOffset;

            writer.Write(PatchValue);
        }

        static string GetNewPath(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string directory = Path.GetDirectoryName(filePath) ?? string.Empty;
            string seperator = directory.Length > 0 ? Path.DirectorySeparatorChar.ToString() : string.Empty;
            return $"{directory}{seperator}{fileName}_RCE_fix.exe";
        }
    }
}