using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Validate namespaces in all SolutionXML elements
            ValidateSolutionXmlNamespaces(diagram);

            // Save the diagram after successful validation
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            // Rethrow if needed
            // throw;
        }
    }

    // Validates that each SolutionXML's XML content has a non‑empty namespace
    static void ValidateSolutionXmlNamespaces(Diagram diagram)
    {
        foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
        {
            if (string.IsNullOrWhiteSpace(solutionXml.XmlValue))
            {
                throw new Exception($"SolutionXML '{solutionXml.Name}' contains empty XML.");
            }

            XDocument doc;
            try
            {
                doc = XDocument.Parse(solutionXml.XmlValue);
            }
            catch (Exception parseEx)
            {
                throw new Exception($"SolutionXML '{solutionXml.Name}' is not well‑formed XML: {parseEx.Message}");
            }

            if (!HasValidNamespaces(doc))
            {
                throw new Exception($"SolutionXML '{solutionXml.Name}' does not define a valid XML namespace.");
            }
        }
    }

    // Checks that the root element has a namespace defined (non‑empty)
    static bool HasValidNamespaces(XDocument doc)
    {
        if (doc.Root == null)
            return false;

        // Namespace is considered valid if it is not empty
        return !string.IsNullOrEmpty(doc.Root.Name.NamespaceName);
    }
}
