using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Get directory path from command line argument or prompt the user
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
                    Console.WriteLine("The specified directory does not exist.");
                    return;
                }
            }

            // Define Visio file extensions to process
            string[] visioExtensions = new[] { ".vsd", ".vsdx", ".vsdm", ".vss", ".vssx", ".vssm", ".vst", ".vstx", ".vstm" };

            // Get all Visio files in the directory (non‑recursive)
            var visioFiles = Directory.GetFiles(directoryPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (var filePath in visioFiles)
            {
                if (Array.IndexOf(visioExtensions, Path.GetExtension(filePath).ToLower()) < 0)
                    continue; // Skip non‑Visio files

                try
                {
                    // Load Visio file using a read stream
                    using (FileStream inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        // Load the diagram from the stream
                        Diagram diagram = new Diagram(inputStream);

                        // Prepare output HTML file path
                        string htmlFileName = Path.ChangeExtension(Path.GetFileName(filePath), ".html");
                        string htmlFilePath = Path.Combine(directoryPath, htmlFileName);

                        // Save diagram to HTML using a write stream
                        using (FileStream outputStream = new FileStream(htmlFilePath, FileMode.Create, FileAccess.Write))
                        {
                            diagram.Save(outputStream, SaveFileFormat.Html);
                        }

                        Console.WriteLine($"Converted: {Path.GetFileName(filePath)} -> {htmlFileName}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Conversion completed.");
        }
    }