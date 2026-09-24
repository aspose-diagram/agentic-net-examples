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

            // Path for the exported PNG image
            string outputPath = "output.png";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to adjust fill opacity
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set foreground and background fill transparency to 20%
                    // (100% - 80% opacity = 20% transparency)
                    shape.Fill.FillForegndTrans.Value = 20;
                    shape.Fill.FillBkgndTrans.Value = 20;
                }
            }

            // Configure PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.ExportHiddenPage = false; // export only visible pages

            // Save the diagram as a PNG image
            diagram.Save(outputPath, pngOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
