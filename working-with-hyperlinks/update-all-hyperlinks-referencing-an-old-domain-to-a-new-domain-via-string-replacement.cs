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

            // Domains to replace
            string oldDomain = "oldexample.com";
            string newDomain = "newexample.com";

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
                        // Iterate through each hyperlink in the shape
                        foreach (Hyperlink link in shape.Hyperlinks)
                        {
                            // Replace the old domain with the new one in the hyperlink address
                            if (!string.IsNullOrEmpty(link.Address?.Value) && link.Address.Value.Contains(oldDomain))
                            {
                                link.Address.Value = link.Address.Value.Replace(oldDomain, newDomain);
                            }
                        }
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
