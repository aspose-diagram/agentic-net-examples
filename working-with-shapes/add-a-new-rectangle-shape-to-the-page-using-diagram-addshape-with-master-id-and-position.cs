using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page to work with.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page as the target page.
                Page page = diagram.Pages[0];

                // Find a master named "Rectangle". If it does not exist, report and exit.
                Master rectangleMaster = null;
                foreach (Master m in diagram.Masters)
                {
                    if (string.Equals(m.NameU, "Rectangle", StringComparison.OrdinalIgnoreCase))
                    {
                        rectangleMaster = m;
                        break;
                    }
                }

                if (rectangleMaster == null)
                {
                    Console.Error.WriteLine("Master named 'Rectangle' was not found in the diagram.");
                    return;
                }

                // Add a new rectangle shape using the master name and desired position.
                // Position (PinX, PinY) is set to (2.0, 2.0) – adjust as needed.
                // Page index is 0 for the first page.
                long shapeId = diagram.AddShape(2.0, 2.0, rectangleMaster.Name, 0);

                // Retrieve the newly added shape from the page's shape collection.
                Shape newShape = page.Shapes.GetShape(shapeId);

                // Optional: set some text on the rectangle to identify it.
                newShape.Text.Value.Clear();
                newShape.Text.Value.Add(new Txt("Rectangle"));

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Rectangle shape added and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}