using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    try
                    {
                        // Attempt to access the InheritFill property
                        var inheritFill = shape.InheritFill;

                        if (inheritFill == null)
                        {
                            // Property is missing or not applicable for this shape
                            Console.WriteLine($"Shape ID {shape.ID} on page '{page.NameU}' does not have InheritFill data.");
                            continue;
                        }

                        // Example: read some fill values safely
                        var foregnd = inheritFill.FillForegnd?.Value;
                        var bkgnd = inheritFill.FillBkgnd?.Value;
                        var pattern = inheritFill.FillPattern?.Value;

                        Console.WriteLine($"Shape ID {shape.ID} InheritFill - Foregnd: {foregnd}, Bkgnd: {bkgnd}, Pattern: {pattern}");
                    }
                    catch (Exception shapeEx)
                    {
                        // Handle any unexpected errors while processing a shape
                        Console.Error.WriteLine($"Error processing shape ID {shape.ID}: {shapeEx.Message}");
                    }
                }
            }

            // Save the (potentially unchanged) diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Handle errors related to loading or saving the diagram
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}