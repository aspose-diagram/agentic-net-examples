using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and disable the layer named "Grid"
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Grid")
                    {
                        layer.Visible.Value = BOOL.False;
                    }
                }
            }

            // Save the modified diagram as VSDX
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
