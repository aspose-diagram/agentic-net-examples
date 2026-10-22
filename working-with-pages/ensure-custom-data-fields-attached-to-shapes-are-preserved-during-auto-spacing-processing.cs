using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
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
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Ensure the page contains at least one shape.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The page contains no shapes to modify.");
                    return;
                }

                // Retrieve the first shape by its ID.
                long firstShapeId = page.Shapes[0].ID;
                Shape shape = page.Shapes.GetShape(firstShapeId);

                // -------------------------------------------------
                // Add a custom property (Prop) to the shape.
                // -------------------------------------------------
                Prop customProp = new Prop();
                customProp.Name = "MyCustomProp";
                customProp.Label.Value = "My Label";
                customProp.Value.Val = "InitialValue";
                // Set the data type of the custom property to string.
                customProp.Type.Value = TypePropValue.String;
                shape.Props.Add(customProp);
                Console.WriteLine("Custom property added to shape before auto‑spacing.");

                // -------------------------------------------------
                // Configure auto‑spacing options.
                // -------------------------------------------------
                AutoSpaceOptions options = new AutoSpaceOptions();
                // Set horizontal and vertical distances (in inches).
                options.DistanceInHorizontal = 0.5;
                options.DistanceInVertical = 0.5;

                // Perform auto‑spacing on all shapes of the page.
                page.AutoSpaceShapes(page.Shapes, options);
                Console.WriteLine("Auto‑spacing completed.");

                // -------------------------------------------------
                // Verify that the custom property still exists.
                // -------------------------------------------------
                bool propFound = false;
                foreach (Prop p in shape.Props)
                {
                    if (p.Name == "MyCustomProp")
                    {
                        propFound = true;
                        Console.WriteLine($"Custom property preserved. Value = '{p.Value.Val}'");
                        break;
                    }
                }

                if (!propFound)
                {
                    Console.Error.WriteLine("Custom property was lost after auto‑spacing.");
                }

                // -------------------------------------------------
                // Save the modified diagram to the output path.
                // -------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}