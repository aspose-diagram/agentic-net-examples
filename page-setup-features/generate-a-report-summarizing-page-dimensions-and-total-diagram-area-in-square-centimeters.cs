using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a diagram file path was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as a command-line argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram.
            diagram = new Diagram(diagramPath);

            double totalAreaCm2 = 0.0;
            int pageNumber = 1;

            // Iterate over each page to retrieve dimensions.
            foreach (Page page in diagram.Pages)
            {
                // Page dimensions are stored in inches.
                double widthInches = page.PageSheet.PageProps.PageWidth.Value;
                double heightInches = page.PageSheet.PageProps.PageHeight.Value;

                // Convert inches to centimeters (1 inch = 2.54 cm).
                double widthCm = widthInches * 2.54;
                double heightCm = heightInches * 2.54;

                // Calculate area in square centimeters.
                double areaCm2 = widthCm * heightCm;
                totalAreaCm2 += areaCm2;

                // Output the dimensions and area for the current page.
                Console.WriteLine($"Page {pageNumber}: Width = {widthCm:F2} cm, Height = {heightCm:F2} cm, Area = {areaCm2:F2} cm²");
                pageNumber++;
            }

            // Output the total diagram area.
            Console.WriteLine($"Total diagram area: {totalAreaCm2:F2} cm²");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Ensure resources are released.
            if (diagram != null)
                diagram.Dispose();
        }
    }
}