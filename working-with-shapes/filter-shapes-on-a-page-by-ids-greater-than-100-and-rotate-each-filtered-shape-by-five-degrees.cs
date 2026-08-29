using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths can be passed as command‑line arguments.
            // If not provided, default paths are used.
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (index 0).
                Page page = diagram.Pages[0];

                // Iterate all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Filter shapes whose ID is greater than 100.
                    if (shape.ID > 100L)
                    {
                        // Retrieve the current rotation angle (in radians).
                        double currentAngle = shape.XForm.Angle.Value;

                        // Add five degrees (converted to radians) to the current angle.
                        double newAngle = currentAngle + (5.0 * Math.PI / 180.0);

                        // Set the new rotation angle.
                        shape.XForm.Angle.Value = newAngle;
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Processing completed. Diagram saved to: " + outputPath);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
