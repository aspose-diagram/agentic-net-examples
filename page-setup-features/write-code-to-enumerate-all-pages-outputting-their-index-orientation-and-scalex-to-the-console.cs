using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];

        // Guard against missing file
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(filePath);

            // Iterate through all pages
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Retrieve the page by index
                Page page = diagram.Pages[i];

                // Get orientation from PrintProps
                var orientationValue = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                // Get ScaleX from PrintProps
                var scaleXValue = page.PageSheet.PrintProps.ScaleX.Value;

                // Output page information
                Console.WriteLine($"Page {i}: Orientation = {orientationValue}, ScaleX = {scaleXValue}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released
            if (diagram != null)
            {
                diagram.Dispose();
            }
        }
    }
}