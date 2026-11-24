using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Reference height for scaling (in inches). Adjust as needed.
            const double referenceHeight = 10.0;

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the page height (in inches).
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Compute scaling factor based on page height.
                double scaleFactor = pageHeight / referenceHeight;

                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a valid Angle cell before modifying.
                    if (shape.XForm != null && shape.XForm.Angle != null)
                    {
                        // Original rotation angle (in degrees).
                        double originalAngle = shape.XForm.Angle.Value;

                        // Adjust the angle proportionally to the page height.
                        double newAngle = originalAngle * scaleFactor;

                        // Apply the new rotation angle.
                        shape.XForm.Angle.Value = newAngle;
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}