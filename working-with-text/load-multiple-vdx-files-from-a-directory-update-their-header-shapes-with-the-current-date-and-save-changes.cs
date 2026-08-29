using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Specify the directory containing VDX files
            string directoryPath = @"C:\VisioFiles";

            // Validate that the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Directory does not exist: {directoryPath}");
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

                    // Update header/footer with the current date (centered header)
                    // Using a formatted date string; you could also use Visio wildcard "&d"
                    diagram.HeaderFooter.HeaderCenter = DateTime.Now.ToString("yyyy-MM-dd");

                    // Save the changes back to the same file
                    diagram.Save(filePath, SaveFileFormat.Vdx);

                    Console.WriteLine($"Successfully updated header for: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }
        }
    }