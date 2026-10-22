using System;
using System.IO;
using Aspose.Diagram;

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

        // Guard: ensure the input file exists.
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
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Locate a master named "Rectangle".
                Master rectangleMaster = null;
                foreach (Master m in diagram.Masters)
                {
                    if (m.Name == "Rectangle")
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

                // Define the position (center) for the new rectangle shape (in inches).
                double pinX = 5.0;
                double pinY = 5.0;

                // Add the rectangle shape to the first page using the master name.
                // The fourth argument isCalculate is a bool; use false to skip automatic size calculation.
                long shapeId = diagram.Pages[0].AddShape(pinX, pinY, rectangleMaster.Name, false);

                // Retrieve the newly added shape from the first page.
                Shape newShape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Optionally set explicit width and height (in inches).
                newShape.XForm.Width.Value = 2.0;
                newShape.XForm.Height.Value = 1.0;

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Rectangle shape added and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
