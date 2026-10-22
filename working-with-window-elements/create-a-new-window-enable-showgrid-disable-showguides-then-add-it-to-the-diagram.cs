using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path for the diagram
        string outputPath = "output.vsdx";

        // Ensure the output directory exists (if a directory is specified)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Create a new empty diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram())
            {
                // Create a new Window instance
                Window window = new Window();

                // Enable the grid visibility in the window
                window.ShowGrid = BOOL.True;

                // Disable the guides visibility in the window
                window.ShowGuides = BOOL.False;

                // Add the configured window to the diagram's window collection
                diagram.Windows.Add(window);

                // Save the diagram to a VSDX file (requires a SaveFileFormat argument)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}