using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Get directory path from command line or ask the user
            string directoryPath;
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                directoryPath = args[0];
            }
            else
            {
                Console.Write("Enter the directory path containing Visio files: ");
                directoryPath = Console.ReadLine();
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine("Directory does not exist.");
                    return;
                }
            }

            // Supported Visio extensions
            string[] visioExtensions = new[] { ".vsd", ".vsdx", ".vdx", ".vss", ".vssx", ".vst", ".vstx", ".vsx", ".vtx", ".vdw" };

            // Process each Visio file in the directory (non‑recursive)
            foreach (string filePath in Directory.GetFiles(directoryPath))
            {
                if (Array.IndexOf(visioExtensions, Path.GetExtension(filePath).ToLower()) < 0)
                    continue; // skip non‑Visio files

                try
                {
                    // Load diagram from a read‑only file stream
                    using (FileStream inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    using (Diagram diagram = new Diagram(inputStream))
                    {
                        // Prepare output HTML file path
                        string outputFileName = Path.ChangeExtension(filePath, ".html");
                        using (FileStream outputStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                        {
                            // Save diagram as HTML to the output stream
                            diagram.Save(outputStream, SaveFileFormat.Html);
                        }
                    }

                    Console.WriteLine($"Converted: {Path.GetFileName(filePath)} -> {Path.GetFileName(Path.ChangeExtension(filePath, ".html"))}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to convert {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            Console.WriteLine("Conversion process completed.");
        }
    }