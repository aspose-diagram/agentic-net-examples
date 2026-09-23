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
            // Path for the rendered image without comment overlays
            string outputPath = "output.png";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and their annotations (comments)
            foreach (Page page in diagram.Pages)
            {
                // Annotations collection holds the comments
                foreach (Annotation comment in page.PageSheet.Annotations)
                {
                    // There is no explicit visibility flag on Annotation in Aspose.Diagram.
                    // To ensure comments are not rendered, we rely on the save option below.
                    // (If a visibility property existed, it would be set here.)
                }
            }

            // Configure image export options to exclude comments
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Do not export comments (overlays)
            saveOptions.IsExportComments = false;

            // Render the diagram to PNG without comment overlays
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram rendered to '{outputPath}' without comments.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
