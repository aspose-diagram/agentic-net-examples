using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Create a custom XML namespace mapping for SolutionXML.
            // The XML content includes a unique namespace (http://example.com/custom)
            // to avoid collisions with other schemas.
            // -----------------------------------------------------------------
            string customXml = @"<root xmlns:custom=""http://example.com/custom"">" +
                               @"<custom:Data>Sample value</custom:Data>" +
                               @"</root>";

            SolutionXML solXml = new SolutionXML();
            solXml.Name = "CustomSolutionData";   // Identifier for the SolutionXML element.
            solXml.XmlValue = customXml;          // Store the XML with the custom namespace.

            // Add the SolutionXML element to the diagram.
            diagram.SolutionXMLs.Add(solXml);

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");

            // -----------------------------------------------------------------
            // Reload the saved diagram to demonstrate reading the custom SolutionXML.
            // -----------------------------------------------------------------
            Diagram loadedDiagram = new Diagram(outputPath);

            Console.WriteLine("Reading SolutionXML elements from the saved diagram:");
            foreach (SolutionXML sXml in loadedDiagram.SolutionXMLs)
            {
                Console.WriteLine($"Name: {sXml.Name}");
                Console.WriteLine($"XML Value: {sXml.XmlValue}");
                Console.WriteLine(new string('-', 40));
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}