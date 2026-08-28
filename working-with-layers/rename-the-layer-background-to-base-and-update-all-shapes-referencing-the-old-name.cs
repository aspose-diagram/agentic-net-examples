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

            // Load the Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the document
            foreach (Page page in diagram.Pages)
            {
                // Iterate through the layers collection of the page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Identify the layer named "Background"
                    if (layer.Name.Value == "Background")
                    {
                        // Rename the layer to "Base"
                        layer.Name.Value = "Base";
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
