using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through pages to find a circle (ellipse) shape
            bool shapeFound = false;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape is based on the Ellipse master (commonly used for circles)
                    if (shape.Master != null && shape.Master.Name == "Ellipse")
                    {
                        // Aspose.Diagram does not expose a direct LineJoin property.
                        // To achieve sharper corners similar to a bevel join, we set the line rounding to zero.
                        // This removes any rounding applied to the shape's outline.
                        shape.Line.Rounding.Value = 0.0;

                        shapeFound = true;
                        break;
                    }
                }
                if (shapeFound) break;
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine("No circle (ellipse) shape found in the diagram.");
                return;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}