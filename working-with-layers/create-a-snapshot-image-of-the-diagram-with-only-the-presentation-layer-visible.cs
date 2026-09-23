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

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output snapshot image path
            string outputPath = "snapshot.png";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Access the collection of layers on the page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Make only the layer named "Presentation" visible; hide others
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

            // Configure image export options (PNG format)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Export only the first page (snapshot)
            saveOptions.PageIndex = 0;
            saveOptions.PageCount = 1;
            // Do not export hidden pages
            saveOptions.ExportHiddenPage = false;

            // Save the snapshot image
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
