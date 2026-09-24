using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output_rotated.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram has at least five pages
                if (diagram.Pages.Count < 5)
                {
                    Console.WriteLine("The diagram does not contain a fifth page.");
                    return;
                }

                // Retrieve the fifth page (zero‑based index 4)
                Page page = diagram.Pages[4];

                // Rotation angle: -30 degrees converted to radians
                double angleDeg = -30;
                double angleRad = (Math.PI / 180) * angleDeg;

                // Rotate the text of each non‑deleted shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue; // Skip shapes marked for deletion

                    shape.TextXForm.TxtAngle.Value = angleRad;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Text rotation applied and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
