using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
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
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        if (shape.OneD)
                        {
                            // For connectors, force a route recalculation by setting a routing style.
                            shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.RightAngle;
                        }
                        else
                        {
                            // Resize non‑connector shapes by increasing width and height by 10%.
                            double newWidth = shape.XForm.Width.Value * 1.1;
                            double newHeight = shape.XForm.Height.Value * 1.1;

                            shape.SetWidth(newWidth);
                            shape.SetHeight(newHeight);
                        }
                    }
                }

                // Save the updated diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}