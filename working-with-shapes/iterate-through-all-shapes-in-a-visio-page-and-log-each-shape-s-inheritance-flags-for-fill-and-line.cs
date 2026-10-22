using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path argument was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path specified. Provide the Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found at path '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.NameU} (ID: {page.ID})");

                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve inherited fill and line data
                        Fill inheritFill = shape.InheritFill;
                        Line inheritLine = shape.InheritLine;

                        // Log shape details along with inherited fill foreground color and line color
                        Console.WriteLine(
                            $"  Shape ID: {shape.ID}, Name: {shape.NameU}, " +
                            $"InheritFill.Foregnd: {inheritFill.FillForegnd.Value}, " +
                            $"InheritLine.Color: {inheritLine.LineColor.Value}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}