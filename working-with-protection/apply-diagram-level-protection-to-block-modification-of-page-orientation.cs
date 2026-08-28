using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output_protected.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Set page orientation to Landscape for every page
            foreach (Page page in diagram.Pages)
            {
                // Access the print properties and assign Landscape orientation
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
            }

            // Apply global protection settings to the diagram
            // These BOOL enum values are assigned directly (no .Value property)
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;   // Prevent background changes
            diagram.DocumentSettings.ProtectMasters = BOOL.True; // Prevent master modifications
            diagram.DocumentSettings.ProtectShapes = BOOL.True;  // Prevent shape edits
            diagram.DocumentSettings.ProtectStyles = BOOL.True;  // Prevent style changes

            // Save the protected diagram using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}