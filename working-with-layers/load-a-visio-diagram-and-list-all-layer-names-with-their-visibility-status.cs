using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string inputPath = "input.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");

                // Iterate through each layer on the current page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Determine visibility status
                    string visibility = layer.Visible.Value == BOOL.True ? "Visible" : "Hidden";

                    // Output layer name and its visibility
                    Console.WriteLine($"  Layer: {layer.Name.Value}, Visibility: {visibility}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
