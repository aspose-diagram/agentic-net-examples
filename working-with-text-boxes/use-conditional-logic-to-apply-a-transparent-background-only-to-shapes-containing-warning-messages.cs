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

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the plain text of the shape
                    string text = shape.Text.Value.Text ?? string.Empty;

                    // Apply transparent background only to shapes containing a warning message
                    if (text.IndexOf("warning", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Use a solid fill pattern
                        shape.Fill.FillPattern.Value = 1; // Solid fill

                        // Set both foreground and background transparency to 100% (fully transparent)
                        shape.Fill.FillForegndTrans.Value = 100;
                        shape.Fill.FillBkgndTrans.Value = 100;
                    }
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
