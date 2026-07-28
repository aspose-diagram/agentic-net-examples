using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the protected output file
            string outputPath = "output_protected.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Enable global protection to prevent editing or deletion of background pages
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;

            // Save the protected diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine($"Diagram saved with background protection to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
