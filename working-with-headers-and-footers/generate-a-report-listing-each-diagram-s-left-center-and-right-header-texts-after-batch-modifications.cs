using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a folder path argument is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the folder path containing diagram files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Retrieve all Visio diagram files (VSDX) in the specified folder
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx");

        // If no files are found, inform the user and exit
        if (diagramFiles.Length == 0)
        {
            Console.WriteLine("No Visio diagram files (*.vsdx) were found in the specified folder.");
            return;
        }

        // Process each diagram file individually
        foreach (string filePath in diagramFiles)
        {
            // Guard: ensure the file still exists before processing
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram from the file
                Diagram diagram = new Diagram(filePath);

                // ----- Batch modification of header texts -----
                // Example modifications: set left, center, and right header strings
                diagram.HeaderFooter.HeaderLeft = $"Left Header - {Path.GetFileNameWithoutExtension(filePath)}";
                diagram.HeaderFooter.HeaderCenter = $"Center Header - {DateTime.Now:yyyy-MM-dd}";
                diagram.HeaderFooter.HeaderRight = $"Right Header - Page &p of &P";

                // Save the modified diagram back to the same file (overwrite)
                diagram.Save(filePath, SaveFileFormat.Vsdx);

                // ----- Report generation -----
                // Output the header texts after modification
                Console.WriteLine($"Diagram: {Path.GetFileName(filePath)}");
                Console.WriteLine($"  Header Left:   {diagram.HeaderFooter.HeaderLeft}");
                Console.WriteLine($"  Header Center: {diagram.HeaderFooter.HeaderCenter}");
                Console.WriteLine($"  Header Right:  {diagram.HeaderFooter.HeaderRight}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}