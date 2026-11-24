using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Find two regular (non‑connector) shapes on the page.
                Shape firstShape = null;
                Shape secondShape = null;

                foreach (Shape shape in page.Shapes)
                {
                    // Skip 1‑D connector shapes.
                    if (shape.OneD)
                        continue;

                    if (firstShape == null)
                        firstShape = shape;
                    else if (secondShape == null)
                    {
                        secondShape = shape;
                        break;
                    }
                }

                if (firstShape == null || secondShape == null)
                {
                    Console.Error.WriteLine("The page does not contain at least two regular shapes.");
                    return;
                }

                // ------------------------------------------------------------
                // Insert a custom text field into the first shape.
                // ------------------------------------------------------------
                Field customField = new Field();
                // Set the displayed value of the field.
                customField.Value.Val = "Custom Field Text";
                // Add the field to the shape's field collection.
                firstShape.Fields.Add(customField);

                // ------------------------------------------------------------
                // Add a dynamic connector shape to the page.
                // ------------------------------------------------------------
                // The connector will be routed automatically; position is arbitrary.
                long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector", false);
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // ------------------------------------------------------------
                // Connect the two shapes using the dynamic connector.
                // ------------------------------------------------------------
                // Use connection points: Right side of the first shape, Bottom side of the second shape.
                page.ConnectShapesViaConnector(
                    firstShape.ID,
                    ConnectionPointPlace.Right,
                    secondShape.ID,
                    ConnectionPointPlace.Bottom,
                    connectorId);

                // ------------------------------------------------------------
                // Save the modified diagram.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}