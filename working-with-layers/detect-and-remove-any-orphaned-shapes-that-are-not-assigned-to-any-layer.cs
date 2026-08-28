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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes already marked as deleted
                    if (shape.Del == BOOL.False)
                    {
                        // Layer membership string (semicolon‑separated indexes)
                        string layerMember = shape.LayerMem?.LayerMember?.Value;

                        // If the shape is not assigned to any layer, mark it as deleted
                        if (string.IsNullOrEmpty(layerMember))
                        {
                            shape.Del = BOOL.True;
                            Console.WriteLine($"Removed orphaned shape ID {shape.ID} on page \"{page.Name}\"");
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to \"{outputPath}\"");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
