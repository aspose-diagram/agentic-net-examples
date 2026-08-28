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

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and set layer visibility
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Only the layer named "Presentation" should be visible
                    if (layer.Name.Value == "Presentation")
                    {
                        layer.Visible.Value = BOOL.True;
                    }
                    else
                    {
                        layer.Visible.Value = BOOL.False;
                    }
                }
            }

            // Configure image save options (PNG format)
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the snapshot image with the applied layer visibility
            string outputPath = "snapshot.png";
            diagram.Save(outputPath, options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
