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
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the comment (Alt text) if present
                    string comment = shape.Misc?.Comment?.Value ?? string.Empty;

                    // Check if the comment contains a warning keyword (case‑insensitive)
                    if (comment.IndexOf("warning", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Ensure the shape has a solid fill pattern
                        shape.Fill.FillPattern.Value = 1; // Solid

                        // Set background fill transparency to 100 % (fully transparent)
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
