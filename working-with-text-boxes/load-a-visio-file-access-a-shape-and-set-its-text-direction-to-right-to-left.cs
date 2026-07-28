using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: <inputVisioPath> <outputVisioPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found on the first page.");
                return;
            }

            // Set the text direction.
            // Aspose.Diagram supports Horizontal and Vertical directions.
            // Right‑to‑left is not a distinct enum value, so we use Vertical as an example.
            targetShape.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;

            // Save the modified diagram to a new file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
    }