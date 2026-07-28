using System.IO;
using System;
using Aspose.Diagram;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Validate namespaces of all SolutionXML entries before saving
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (!IsNamespaceValid(solXml.XmlValue))
                {
                    Console.WriteLine($"Invalid namespace detected in SolutionXML '{solXml.Name}'.");
                    throw new InvalidOperationException($"Invalid namespace in SolutionXML '{solXml.Name}'.");
                }
            }

            // Save the diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Third‑party (System.Xml.Linq) based validation of XML namespaces
    static bool IsNamespaceValid(string xmlContent)
    {
        try
        {
            XDocument doc = XDocument.Parse(xmlContent);
            XNamespace ns = doc.Root.Name.Namespace;

            // Example rule: namespace must start with the expected Aspose Diagram schema URI
            return ns != null && ns.NamespaceName.StartsWith("http://schemas.aspose.com/diagram");
        }
        catch
        {
            // Parsing error indicates malformed XML or missing namespace
            return false;
        }
    }
}
