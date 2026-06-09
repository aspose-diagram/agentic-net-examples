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

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and set layer visibility
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Make only the layer named "Presentation" visible
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
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the snapshot image
            diagram.Save("snapshot.png", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
