using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page at position (2,2)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the concrete Shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a new hyperlink, set its name and target URL
                Hyperlink link = new Hyperlink();
                link.Name = "WebLink";
                link.Address.Value = "https://example.com";

                // Add the hyperlink to the shape's Hyperlinks collection
                shape.Hyperlinks.Add(link);

                // Create a SolutionXML entry that records the hyperlink information
                SolutionXML solXml = new SolutionXML();
                solXml.Name = "HyperlinkInfo";
                solXml.XmlValue = $"<Hyperlink ShapeID=\"{shapeId}\" URL=\"{link.Address.Value}\" />";

                // Add the SolutionXML element to the diagram
                diagram.SolutionXMLs.Add(solXml);

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }