using System;
using System.IO;
using Aspose.Diagram;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputXmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create the root XML element.
                XDocument xmlDoc = new XDocument(new XElement("Diagram"));

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Create an element for the current shape with its ID.
                        XElement shapeElement = new XElement("Shape",
                            new XAttribute("id", shape.ID));

                        // Ensure the shape has a Props collection before iterating.
                        if (shape.Props != null)
                        {
                            // Iterate through all custom properties (Props) of the shape.
                            foreach (Prop prop in shape.Props)
                            {
                                // Extract property name and value.
                                string propName = prop.Name ?? string.Empty;
                                string propValue = prop.Value?.Val ?? string.Empty;

                                // Add a Property element under the current Shape element.
                                XElement propElement = new XElement("Property",
                                    new XAttribute("name", propName),
                                    new XAttribute("value", propValue));

                                shapeElement.Add(propElement);
                            }
                        }

                        // Append the shape element to the root.
                        xmlDoc.Root.Add(shapeElement);
                    }
                }

                // Save the constructed XML document.
                xmlDoc.Save(outputPath);
                Console.WriteLine($"Extraction completed. XML saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}