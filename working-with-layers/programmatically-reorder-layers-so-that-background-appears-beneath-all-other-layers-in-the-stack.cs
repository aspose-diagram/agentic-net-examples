using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Iterate through the layers collection and move the layer named "Background"
            // to the bottom of the stack by setting its index (IX) to 0.
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value == "Background")
                {
                    // Bring the background layer to the bottom
                    layer.IX = 0;
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
