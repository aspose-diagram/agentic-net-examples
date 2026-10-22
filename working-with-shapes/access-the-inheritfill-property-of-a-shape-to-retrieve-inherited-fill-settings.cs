using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard against missing file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];
                Console.WriteLine($"Page {pageIndex + 1}/{diagram.Pages.Count}");

                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

                    // Access the inherited fill information.
                    Fill inheritFill = shape.InheritFill;
                    if (inheritFill == null)
                    {
                        Console.WriteLine("  No inherited fill data available.");
                        continue;
                    }

                    // Retrieve and display inherited fill properties.
                    Console.WriteLine($"  Inherited FillForegnd: {inheritFill.FillForegnd?.Value}");
                    Console.WriteLine($"  Inherited FillBkgnd: {inheritFill.FillBkgnd?.Value}");
                    Console.WriteLine($"  Inherited FillPattern: {inheritFill.FillPattern?.Value}");
                    Console.WriteLine($"  Inherited ShdwForegnd: {inheritFill.ShdwForegnd?.Value}");
                    Console.WriteLine($"  Inherited ShdwBkgnd: {inheritFill.ShdwBkgnd?.Value}");
                    Console.WriteLine($"  Inherited ShdwPattern: {inheritFill.ShdwPattern?.Value}");
                    Console.WriteLine($"  Inherited ShapeShdwType: {inheritFill.ShapeShdwType?.Value}");
                    Console.WriteLine($"  Inherited ShapeShdwObliqueAngle: {inheritFill.ShapeShdwObliqueAngle?.Value}");
                    Console.WriteLine($"  Inherited ShapeShdwOffsetX: {inheritFill.ShapeShdwOffsetX?.Value}");
                    Console.WriteLine($"  Inherited ShapeShdwOffsetY: {inheritFill.ShapeShdwOffsetY?.Value}");
                    Console.WriteLine($"  Inherited ShapeShdwScaleFactor: {inheritFill.ShapeShdwScaleFactor?.Value}");
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}