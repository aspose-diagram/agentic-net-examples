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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Define a 10‑pixel margin (assuming 96 DPI).
            double marginInches = 10.0 / 96.0;

            bool rectangleFound = false;

            // Iterate through all pages to locate a rectangle shape.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify rectangle shapes by master name.
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        // Compute new PinX/Y so that the shape's left/top edges align with the margin.
                        double halfWidth = shape.XForm.Width.Value / 2.0;
                        double halfHeight = shape.XForm.Height.Value / 2.0;

                        // Left edge = margin => PinX = margin + halfWidth
                        double newPinX = marginInches + halfWidth;

                        // Top edge = pageHeight - margin => PinY = pageHeight - margin - halfHeight
                        double newPinY = pageHeight - marginInches - halfHeight;

                        // Apply the new position.
                        shape.XForm.PinX.Value = newPinX;
                        shape.XForm.PinY.Value = newPinY;

                        rectangleFound = true;
                        // If only the first rectangle should be aligned, break out of loops.
                        break;
                    }
                }

                if (rectangleFound)
                    break;
            }

            if (!rectangleFound)
            {
                Console.Error.WriteLine("No rectangle shape with master name 'Rectangle' was found in the diagram.");
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}