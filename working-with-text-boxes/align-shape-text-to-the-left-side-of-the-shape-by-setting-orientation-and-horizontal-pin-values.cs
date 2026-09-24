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
            // Path for the modified output file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set text orientation to 0 degrees (no rotation)
                    shape.TextXForm.TxtAngle.Value = 0;

                    // Align text to the left side of the shape
                    // Horizontal pin values: set both local and absolute pins to 0
                    shape.TextXForm.TxtLocPinX.Value = 0;
                    shape.TextXForm.TxtPinX.Value = 0;

                    // Ensure each paragraph within the shape is left-aligned
                    foreach (Para para in shape.Paras)
                    {
                        para.HorzAlign.Value = HorzAlignValue.LeftAlign;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
