using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Specify the directory containing VDX files
            string directoryPath = @"C:\VisioFiles";

            // Verify the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Directory not found: {directoryPath}");
                return;
            }

            // Get all .vdx files in the directory
            string[] vdxFiles = Directory.GetFiles(directoryPath, "*.vdx", SearchOption.TopDirectoryOnly);

            if (vdxFiles.Length == 0)
            {
                Console.WriteLine("No VDX files found in the specified directory.");
                return;
            }

            // Process each VDX file
            foreach (string filePath in vdxFiles)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Update the header center with the current date (e.g., 2026-09-23)
                    diagram.HeaderFooter.HeaderCenter = DateTime.Now.ToString("yyyy-MM-dd");

                    // Save the changes back to the same file
                    diagram.Save(filePath, SaveFileFormat.Vdx);

                    Console.WriteLine($"Updated header for: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Header update completed.");
        }
    }