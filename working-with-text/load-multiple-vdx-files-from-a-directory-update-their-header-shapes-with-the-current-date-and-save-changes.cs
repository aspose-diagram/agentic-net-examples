using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Define the directory containing VDX files.
            // You can change this path as needed.
            string directoryPath = @"C:\VisioFiles";

            // Verify the directory exists.
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Directory not found: {directoryPath}");
                return;
            }

            // Get all .vdx files in the directory.
            string[] vdxFiles = Directory.GetFiles(directoryPath, "*.vdx", SearchOption.TopDirectoryOnly);

            if (vdxFiles.Length == 0)
            {
                Console.WriteLine("No VDX files found in the specified directory.");
                return;
            }

            // Process each VDX file.
            foreach (string filePath in vdxFiles)
            {
                try
                {
                    // Load the diagram from the file.
                    Diagram diagram = new Diagram(filePath);

                    // Update the header with the current date.
                    // Using HeaderCenter as an example; you can modify other header/footer fields similarly.
                    diagram.HeaderFooter.HeaderCenter = DateTime.Now.ToString("yyyy-MM-dd");

                    // Save the changes back to the same file (overwrite).
                    diagram.Save(filePath, SaveFileFormat.Vdx);

                    Console.WriteLine($"Updated header for: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Header update operation completed.");
        }
    }