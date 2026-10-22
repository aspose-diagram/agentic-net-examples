using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output_page3_rotated.vsdx";

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least three pages
                if (diagram.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The diagram does not contain a third page.");
                    return;
                }

                // Page indices are zero‑based; page three is index 2
                Page pageThree = diagram.Pages[2];

                // Angle in radians for 90 degrees
                double angleRad = (Math.PI / 180) * 90.0;

                // Iterate over all shapes on page three
                foreach (Shape shape in pageThree.Shapes)
                {
                    // Ensure the shape has a TextXForm object before setting the angle
                    if (shape.TextXForm != null && shape.TextXForm.TxtAngle != null)
                    {
                        shape.TextXForm.TxtAngle.Value = angleRad;
                    }
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Text rotation applied and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}