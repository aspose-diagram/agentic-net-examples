using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a folder path argument was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the folder path containing Visio diagram files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all Visio files with .vsdx extension in the folder.
        string[] diagramFiles = Directory.GetFiles(folderPath, "*.vsdx");

        // Process each diagram file.
        foreach (string filePath in diagramFiles)
        {
            // Guard: ensure the file still exists before processing.
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required by the API.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Set a uniform header margin of 0.25 inches.
                    diagram.HeaderFooter.HeaderMargin.Value = 0.25;

                    // Save the diagram back to the same file using VSDX format.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }

                // Log successful processing.
                Console.WriteLine($"Successfully updated header margin: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                // Log any failures to the error stream.
                Console.Error.WriteLine($"Failed to process '{filePath}': {ex.Message}");
            }
        }
    }
}