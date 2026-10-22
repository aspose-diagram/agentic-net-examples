using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Add a new shape to the active page (a simple rectangle).
                // -----------------------------------------------------------------
                Page page = diagram.ActivePage;
                // Parameters: pinX, pinY, masterName, isCalculate
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);
                Shape shape = page.Shapes.GetShape(shapeId);

                // -----------------------------------------------------------------
                // Create a hyperlink and attach it to the newly added shape.
                // -----------------------------------------------------------------
                Hyperlink link = new Hyperlink();
                link.Name = "WebLink";
                link.Address.Value = "https://example.com";
                // Add the hyperlink to the shape's collection.
                shape.Hyperlinks.Add(link);

                // -----------------------------------------------------------------
                // TODO: Add a SolutionXML element that contains the hyperlink definition.
                // The exact API for creating a SolutionXML object (its constructor,
                // properties such as Name or XmlData) is not documented in the
                // provided reference. Implement this part when the correct API
                // details are available.
                // -----------------------------------------------------------------
                // Example (pseudo‑code):
                // SolutionXML solXml = new SolutionXML();
                // solXml.Name = "HyperlinkData";
                // solXml.Xml = "<Hyperlink Address=\"https://example.com\" />";
                // diagram.SolutionXMLs.Add(solXml);

                // -----------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}