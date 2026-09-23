using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked for deletion
                    if (shape.Del == BOOL.True)
                        continue;

                    // Configure the shape‑resize event (EventXFMod) to keep a fixed aspect ratio.
                    // The formula below forces the width to be 1.5 times the height.
                    // Adjust the multiplier (1.5) to the desired aspect ratio for your shapes.
                    shape.Event.EventXFMod.Ufe.F = "GUARD(Height*1.5)";
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
