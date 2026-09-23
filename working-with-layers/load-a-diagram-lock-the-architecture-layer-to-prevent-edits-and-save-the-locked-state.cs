using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output_locked.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and lock the layer named "Architecture"
            foreach (Page page in diagram.Pages)
            {
                // Access the collection of layers on the current page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Compare the layer name (case‑sensitive) with the target name
                    if (layer.Name.Value == "Architecture")
                    {
                        // Prevent edits by making the layer invisible (Visio treats invisible layers as locked)
                        layer.Visible.Value = BOOL.False;
                    }
                }
            }

            // Save the modified diagram preserving the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
