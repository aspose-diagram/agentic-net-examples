using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the directory containing VDX files.
        string folderPath;
        if (args.Length > 0 && Directory.Exists(args[0]))
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Enter the full path to the folder containing VDX files: ");
            folderPath = Console.ReadLine();
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("The specified folder does not exist.");
                return;
            }
        }

        // Get all .vdx files in the directory.
        string[] vdxFiles = Directory.GetFiles(folderPath, "*.vdx", SearchOption.TopDirectoryOnly);
        if (vdxFiles.Length == 0)
        {
            Console.WriteLine("No VDX files found in the specified folder.");
            return;
        }

        // Process each file.
        foreach (string filePath in vdxFiles)
        {
            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(filePath);

                // Update the header with the current date (centered header).
                // Using a formatted date string; adjust format as needed.
                diagram.HeaderFooter.HeaderCenter = DateTime.Now.ToString("yyyy-MM-dd");

                // Save changes back to the same file (overwrite).
                diagram.Save(filePath, SaveFileFormat.Vdx);

                Console.WriteLine($"Updated header for: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}
