using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path for the new diagram
        string outputPath = "CreatedDiagram.vsdx";

        // Verify that the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram())
            {
                // Retrieve the first (default) page
                Page page = diagram.Pages[0];

                // Assign a custom name to the page (optional)
                page.Name = "CustomPage";

                // Define desired page dimensions (e.g., 8.5 inches width x 11 inches height)
                double widthInches = 8.5;
                double heightInches = 11.0;

                // Set page width and height via the PageSheet cells (assign double values directly)
                page.PageSheet.PageProps.PageWidth.Value = widthInches;
                page.PageSheet.PageProps.PageHeight.Value = heightInches;

                // Save the diagram as VSDX using the required overload (path + format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during diagram creation or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
