using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect shapes that are not currently part of any group
                List<Shape> shapesToGroup = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    // Check group membership; IsInGroup returns true if the shape belongs to a group
                    if (!shape.IsInGroup())
                    {
                        shapesToGroup.Add(shape);
                    }
                }

                // For each ungrouped shape, create a new group containing only that shape
                foreach (Shape shape in shapesToGroup)
                {
                    // Group method expects an array of Shape objects and returns the new group shape
                    page.Shapes.Group(new Shape[] { shape });
                }
            }

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}