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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

            // Create a hyperlink and set its target URL
            Hyperlink link = new Hyperlink();
            link.Name = "WebLink";
            link.Address.Value = "https://example.com";

            // Add the hyperlink to the shape's Hyperlinks collection
            shape.Hyperlinks.Add(link);

            // Record the hyperlink information in a SolutionXML element
            SolutionXML solXml = new SolutionXML();
            solXml.Name = "HyperlinkInfo";
            solXml.XmlValue = $"<Hyperlink ShapeID=\"{shape.ID}\" URL=\"{link.Address.Value}\" />";
            diagram.SolutionXMLs.Add(solXml);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with hyperlink and SolutionXML.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
