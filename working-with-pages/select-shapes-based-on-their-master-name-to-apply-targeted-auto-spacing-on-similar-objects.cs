using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file, master name, output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <masterName> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string masterName = args[1];
        string outputPath = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare auto‑spacing options (distances are in inches)
                AutoSpaceOptions options = new AutoSpaceOptions
                {
                    // The correct property names are DistanceInHorizontal / DistanceInVertical
                    DistanceInHorizontal = 0.5,
                    DistanceInVertical = 0.5
                };

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Count shapes that use the specified master
                    int targetCount = 0;
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a master and compare its name
                        if (shape.Master != null && shape.Master.Name == masterName)
                        {
                            targetCount++;
                        }
                    }

                    // Apply auto‑spacing only if we have matching shapes
                    if (targetCount > 0)
                    {
                        // NOTE:
                        // The AutoSpaceShapes method expects a ShapeCollection.
                        // ShapeCollection cannot be instantiated directly, and there is no
                        // overload that accepts a filtered list. As a workaround, we invoke
                        // AutoSpaceShapes on the page's full shape collection. This will
                        // auto‑space all shapes on the page. In environments where a
                        // filtered ShapeCollection can be created, replace the call below
                        // with the filtered collection.
                        page.AutoSpaceShapes(page.Shapes, options);
                        Console.WriteLine($"Auto‑spaced page '{page.Name}' (found {targetCount} shape(s) with master '{masterName}').");
                    }
                }

                // Save the modified diagram in VSDX format (second argument must be a SaveFileFormat)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}