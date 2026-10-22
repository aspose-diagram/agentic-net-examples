using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <XmlDataPath> [OutputVisioPath]");
            return;
        }

        string visioPath = args[0];
        string xmlPath = args[1];
        string outputPath = args.Length >= 3
            ? args[2]
            : Path.Combine(Path.GetDirectoryName(visioPath) ?? "", Path.GetFileNameWithoutExtension(visioPath) + "_mapped.vsdx");

        // Guard against missing input files.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlPath}");
            return;
        }

        // Load XML data into memory.
        XDocument xmlDoc;
        try
        {
            xmlDoc = XDocument.Load(xmlPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load XML file: {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate over each <Shape> element in the XML.
                foreach (XElement shapeElement in xmlDoc.Root?.Elements("Shape") ?? new XElement[0])
                {
                    // Expected XML attributes/elements:
                    //   Name  - matches shape.NameU in the diagram
                    //   Fill  - optional fill color (e.g., "#FF0000")
                    //   Text  - optional text value
                    XAttribute nameAttr = shapeElement.Attribute("Name");
                    if (nameAttr == null)
                        continue; // Skip elements without a Name attribute.

                    string targetName = nameAttr.Value;

                    // Locate the shape with the matching NameU.
                    Shape targetShape = FindShapeByNameU(diagram, targetName);
                    if (targetShape == null)
                    {
                        Console.WriteLine($"Shape with NameU '{targetName}' not found in diagram.");
                        continue;
                    }

                    // Map Fill color if present.
                    XElement fillElement = shapeElement.Element("Fill");
                    if (fillElement != null)
                    {
                        // Set the fill foreground color. The Value property expects a string like "#RRGGBB".
                        targetShape.Fill.FillForegnd.Value = fillElement.Value.Trim();
                    }

                    // Map Text if present.
                    XElement textElement = shapeElement.Element("Text");
                    if (textElement != null)
                    {
                        // Clear existing text runs.
                        targetShape.Text.Value.Clear();
                        // Add new text run.
                        targetShape.Text.Value.Add(new Txt(textElement.Value.Trim()));
                    }
                }

                // Save the modified diagram to the output path.
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Searches all pages of the diagram for a shape whose NameU matches the specified value.
    /// Returns null if no matching shape is found.
    /// </summary>
    static Shape FindShapeByNameU(Diagram diagram, string nameU)
    {
        // Iterate through each page.
        foreach (Page page in diagram.Pages)
        {
            // Iterate through each shape on the page.
            foreach (Shape shape in page.Shapes)
            {
                // NameU is a string property that holds the universal name of the shape.
                if (shape.NameU == nameU)
                {
                    return shape;
                }
            }
        }
        return null;
    }
}