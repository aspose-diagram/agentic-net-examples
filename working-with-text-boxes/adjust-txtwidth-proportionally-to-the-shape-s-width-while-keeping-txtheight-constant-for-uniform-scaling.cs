using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Adjust the text block width to match the shape's width.
                    // TxtHeight is left unchanged to keep uniform scaling.
                    shape.TextXForm.TxtWidth.Value = shape.XForm.Width.Value;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("TxtWidth adjusted proportionally to shape width and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
