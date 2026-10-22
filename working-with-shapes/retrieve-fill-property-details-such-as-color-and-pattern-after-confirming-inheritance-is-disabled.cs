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

        string visioPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];
                Console.WriteLine($"Page {pageIndex + 1} (ID: {page.ID}) contains {page.Shapes.Count} shapes.");

                // Iterate through all shapes on the current page
                for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                {
                    Shape shape = page.Shapes[shapeIdx];

                    // Ensure fill inheritance is disabled by reassigning current values
                    shape.Fill.FillForegnd.Value = shape.Fill.FillForegnd.Value;
                    shape.Fill.FillBkgnd.Value = shape.Fill.FillBkgnd.Value;
                    shape.Fill.FillPattern.Value = shape.Fill.FillPattern.Value;

                    // Retrieve fill details
                    string foreColor = shape.Fill.FillForegnd.Value;
                    string backColor = shape.Fill.FillBkgnd.Value;
                    int pattern = shape.Fill.FillPattern.Value;

                    // Output the retrieved fill information
                    Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");
                    Console.WriteLine($"  Fill Foreground Color: {foreColor}");
                    Console.WriteLine($"  Fill Background Color: {backColor}");
                    Console.WriteLine($"  Fill Pattern Index: {pattern}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}