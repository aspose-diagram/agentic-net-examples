using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output Visio file path
            string outputPath = "output_offset.vsdx";

            // Offset values (in inches)
            double deltaX = 1.0; // move right by 1 inch
            double deltaY = 0.5; // move up by 0.5 inch

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);
            try
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Offset the shape position
                        shape.XForm.PinX.Value += deltaX;
                        shape.XForm.PinY.Value += deltaY;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            finally
            {
                // Release resources
                diagram.Dispose();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
