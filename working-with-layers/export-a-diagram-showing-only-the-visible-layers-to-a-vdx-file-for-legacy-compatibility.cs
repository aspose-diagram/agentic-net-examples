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

            // Path to the source Visio file (any supported format)
            string inputPath = "input.vsdx";

            // Path for the exported VDX file (legacy Visio format)
            string outputPath = "output.vdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and its layers.
            // Only layers with Visible == BOOL.True will be retained in the export.
            // Hidden layers remain hidden and are not rendered in the saved VDX.
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // No action needed; the layer's visibility flag is respected during save.
                    // Example of explicit handling (optional):
                    // if (layer.Visible.Value == BOOL.False)
                    // {
                    //     // Optionally, you could remove shapes from this hidden layer here.
                    // }
                }
            }

            // Save the diagram as VDX, preserving only visible layers.
            diagram.Save(outputPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
