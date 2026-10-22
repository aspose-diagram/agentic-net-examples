using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all SolutionXML elements and validate their XML content.
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // NOTE: In a real implementation a third‑party XML namespace validation library
                // would be invoked here. Since external libraries are not available in this
                // environment, we perform a basic validation using the built‑in .NET XML parser.
                try
                {
                    // Parse the XML string with line‑info to aid debugging.
                    XDocument xDoc = XDocument.Parse(solXml.XmlValue, System.Xml.Linq.LoadOptions.SetLineInfo);

                    // Ensure each element has a non‑empty namespace.
                    foreach (XElement elem in xDoc.Descendants())
                    {
                        XNamespace ns = elem.Name.Namespace;
                        if (ns == null || string.IsNullOrWhiteSpace(ns.NamespaceName))
                        {
                            Console.Error.WriteLine($"Element '{elem.Name.LocalName}' in SolutionXML '{solXml.Name}' lacks a namespace.");
                            // Abort saving due to validation failure.
                            return;
                        }
                    }
                }
                catch (XmlException xmlEx)
                {
                    Console.Error.WriteLine($"Invalid XML in SolutionXML '{solXml.Name}': {xmlEx.Message}");
                    // Abort saving due to validation failure.
                    return;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Unexpected error while validating SolutionXML '{solXml.Name}': {ex.Message}");
                    return;
                }
            }

            // All SolutionXML entries passed validation. Save the diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}