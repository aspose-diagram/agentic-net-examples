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

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the shape we want to modify is on the first page and named "MyShape"
            Page page = diagram.Pages[0];
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.NameU == "MyShape")
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception("Shape with NameU 'MyShape' was not found.");
            }

            // Create a new hyperlink and set its properties
            Hyperlink link = new Hyperlink();
            link.Name = "WebLink";
            link.Address.Value = "https://www.example.com";
            link.Description.Value = "Example website";

            // Add the hyperlink to the shape's Hyperlinks collection
            targetShape.Hyperlinks.Add(link);

            // Record the hyperlink information in a SolutionXML element
            SolutionXML solXml = new SolutionXML();
            solXml.Name = "ShapeHyperlinkInfo";
            solXml.XmlValue = $"<Hyperlink Name=\"{link.Name}\" URL=\"{link.Address.Value}\" Description=\"{link.Description.Value}\" />";
            diagram.SolutionXMLs.Add(solXml);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
