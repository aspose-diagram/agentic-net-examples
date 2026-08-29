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

            // Path to the input Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set fill foreground and background transparency to 20%
                    // (Transparency = 100 - Opacity). 80% opacity => 20% transparency.
                    shape.Fill.FillForegndTrans.Value = 20;
                    shape.Fill.FillBkgndTrans.Value = 20;
                }
            }

            // Prepare PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the diagram to a PNG file
            diagram.Save("output.png", pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
