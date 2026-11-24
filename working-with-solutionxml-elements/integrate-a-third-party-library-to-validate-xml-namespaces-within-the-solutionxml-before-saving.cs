using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.Xml.Linq;
using System.Xml;

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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Validate each SolutionXML element's XML namespaces.
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                {
                    Console.Error.WriteLine($"SolutionXML \"{solXml.Name}\" contains empty XML.");
                    return;
                }

                try
                {
                    // Parse the XML content.
                    XDocument xDoc = XDocument.Parse(solXml.XmlValue);

                    // Check that the root element has a namespace defined.
                    XNamespace ns = xDoc.Root.Name.Namespace;
                    if (ns == null || ns == XNamespace.None)
                    {
                        Console.Error.WriteLine($"SolutionXML \"{solXml.Name}\" root element is missing a namespace.");
                        return;
                    }
                }
                catch (XmlException xmlEx)
                {
                    // XML is not well‑formed.
                    Console.Error.WriteLine($"SolutionXML \"{solXml.Name}\" contains invalid XML: {xmlEx.Message}");
                    return;
                }
                catch (Exception ex)
                {
                    // General parsing error.
                    Console.Error.WriteLine($"Error processing SolutionXML \"{solXml.Name}\": {ex.Message}");
                    return;
                }
            }

            // All SolutionXML entries passed validation; proceed to save the diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}