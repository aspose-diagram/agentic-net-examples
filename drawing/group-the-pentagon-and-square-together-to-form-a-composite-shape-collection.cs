using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Lists to hold the shapes to be grouped and their originating page.
            List<Shape> shapesToGroup = new List<Shape>();
            Page targetPage = null;

            // Iterate through all pages and shapes to find Pentagon and Square masters.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master before accessing its name.
                    if (shape.Master != null && shape.Master.Name != null)
                    {
                        string masterName = shape.Master.Name;
                        if (masterName.Equals("Pentagon", StringComparison.OrdinalIgnoreCase) ||
                            masterName.Equals("Square", StringComparison.OrdinalIgnoreCase))
                        {
                            shapesToGroup.Add(shape);
                            // Remember the page of the first matching shape.
                            if (targetPage == null)
                                targetPage = page;
                        }
                    }
                }
            }

            // Validate that we found both shapes on the same page.
            if (shapesToGroup.Count < 2 || targetPage == null)
            {
                Console.Error.WriteLine("Could not find both a Pentagon and a Square shape on the same page.");
                return;
            }

            // Group the collected shapes into a composite shape.
            Shape groupShape = targetPage.Shapes.Group(shapesToGroup.ToArray());

            // Optional: you can set a name for the group for easier identification.
            if (groupShape != null && groupShape.NameU != null)
                groupShape.NameU = "PentagonSquareGroup";

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Successfully grouped shapes and saved diagram to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}