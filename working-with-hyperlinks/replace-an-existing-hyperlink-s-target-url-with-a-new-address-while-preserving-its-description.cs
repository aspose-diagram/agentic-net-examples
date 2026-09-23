using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // New URL to assign to existing hyperlinks
            string newUrl = "https://newexample.com";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Hyperlinks collection
                    if (shape.Hyperlinks != null)
                    {
                        // Update each hyperlink's address while keeping its description unchanged
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            link.Address.Value = newUrl;
                            // link.Description.Value remains as is
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
