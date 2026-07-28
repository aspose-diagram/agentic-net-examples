using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the Visio file and the XML data source
        string diagramPath = "input.vsdx";
        string xmlDataPath = "data.xml";
        string outputPath = "output.vsdx";

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
            Console.WriteLine("Diagram loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Load the XML data that contains mapping information
        XDocument xmlDoc;
        try
        {
            xmlDoc = XDocument.Load(xmlDataPath);
            Console.WriteLine("XML data loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load XML data: {ex.Message}");
            return;
        }

        // Example mapping format:
        // <ShapeMappings>
        //   <ShapeMapping>
        //     <ShapeName>MyShape</ShapeName>
        //     <Text>Hello World</Text>
        //   </ShapeMapping>
        //   ...
        // </ShapeMappings>

        foreach (var mapping in xmlDoc.Root.Elements("ShapeMapping"))
        {
            string shapeName = (string)mapping.Element("ShapeName");
            string newText = (string)mapping.Element("Text");

            if (string.IsNullOrEmpty(shapeName))
                continue;

            // Locate the shape by its universal name (NameU) across all pages
            Shape targetShape = FindShapeByNameU(diagram, shapeName);
            if (targetShape != null)
            {
                // Replace the shape's text content
                targetShape.Text.Value.Clear();
                targetShape.Text.Value.Add(new Txt(newText));
                Console.WriteLine($"Updated shape '{shapeName}' with text '{newText}'.");
            }
            else
            {
                Console.WriteLine($"Shape '{shapeName}' not found in the diagram.");
            }
        }

        // Save the modified diagram
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }

    // Helper method to find a shape by its NameU on any page
    private static Shape FindShapeByNameU(Diagram diagram, string nameU)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (!string.IsNullOrEmpty(shape.NameU) &&
                    shape.NameU.Equals(nameU, StringComparison.OrdinalIgnoreCase))
                {
                    return shape;
                }
            }
        }
        return null;
    }
}
