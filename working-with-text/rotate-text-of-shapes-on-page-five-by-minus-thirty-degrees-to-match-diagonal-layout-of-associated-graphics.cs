using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with actual file path)
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Ensure the diagram has at least five pages (page index is zero‑based)
                if (diagram.Pages.Count < 5)
                {
                    throw new Exception("The diagram does not contain a fifth page.");
                }

                // Retrieve page five (index 4)
                Page page = diagram.Pages[4];

                // Rotation angle: -30 degrees converted to radians
                double angleRad = -Math.PI / 6.0; // -30° = -π/6 rad

                // Rotate the text of each non‑deleted shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        shape.TextXForm.TxtAngle.Value = angleRad;
                    }
                }

                // Save the modified diagram (replace with desired output path)
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
