using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input folder: first argument or current directory
        string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
        // Output folder for processed diagrams
        string outputFolder = Path.Combine(inputFolder, "output");
        Directory.CreateDirectory(outputFolder);

        // Process each Visio file in the folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
        {
            try
            {
                // Load diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Attempt to add a comment; if the diagram format does not support comments,
                    // an exception will be thrown and caught below.
                    try
                    {
                        // Use the first page (index 0)
                        Page page = diagram.Pages[0];
                        // Add a simple comment at coordinates (1,1)
                        page.AddComment(1.0, 1.0, "Batch processed comment");
                    }
                    catch (Exception commentEx)
                    {
                        // Gracefully skip diagrams lacking comment support
                        Console.WriteLine($"Skipping comment for '{Path.GetFileName(filePath)}': {commentEx.Message}");
                    }

                    // Save the (possibly modified) diagram to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any loading or saving errors and continue with the next file
                Console.WriteLine($"Failed to process '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }
    }
}
