using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Update header fields – these changes will be reflected in any print or image output
            diagram.HeaderFooter.HeaderLeft = "Updated Header - Left";
            diagram.HeaderFooter.HeaderCenter = "Updated Header - Center";
            diagram.HeaderFooter.HeaderRight = "Updated Header - Right";

            // Save the modified diagram (optional, verifies changes are persisted)
            diagram.Save("modified.vsdx", SaveFileFormat.Vsdx);

            // Export a PNG image to act as a print preview – the header text appears in the image
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save("preview.png", pngOptions);

            Console.WriteLine("Header updated and preview image generated successfully.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading, modification, or saving
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}