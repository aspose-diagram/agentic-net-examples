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

            // Load the Visio diagram from a file.
            // Replace "input.vsdx" with the actual path to your diagram.
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and their annotations (comments).
            // Although Aspose.Diagram does not expose a direct visibility flag for annotations,
            // we clear the comment text to effectively hide it if needed.
            foreach (Page page in diagram.Pages)
            {
                foreach (Annotation comment in page.PageSheet.Annotations)
                {
                    // Clear the comment text to hide the overlay content.
                    comment.Comment.Value = string.Empty;
                }
            }

            // Configure image export options.
            // Setting IsExportComments to false ensures that comment overlays are not rendered.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.IsExportComments = false;

            // Save the diagram as a PNG image without comment overlays.
            diagram.Save("output.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
