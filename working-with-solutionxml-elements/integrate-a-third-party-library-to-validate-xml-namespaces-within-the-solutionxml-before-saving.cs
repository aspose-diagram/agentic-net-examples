using System.IO;
using System;
using System.Xml.Linq;
using Aspose.Diagram;

public class DiagramProcessor
{
    // Validates that every element in the XML has an explicit namespace.
    private static void ValidateNamespaces(string xmlContent)
    {
        // Parse the XML string into an XDocument.
        XDocument document = XDocument.Parse(xmlContent);

        // Iterate through all elements and check their namespace.
        foreach (XElement element in document.Descendants())
        {
            if (element.Name.Namespace == XNamespace.None)
            {
                // Throw an exception if an element lacks a namespace.
                throw new InvalidOperationException(
                    $"Element '{element.Name}' does not have an explicit XML namespace.");
            }
        }
    }

    // Loads a diagram, validates its SolutionXML namespaces, and saves the diagram.
    public static void ProcessDiagram(string inputFilePath, string outputFilePath)
    {
        // Load the diagram from the specified file.
        Diagram diagram = new Diagram(inputFilePath);

        // Validate each SolutionXML entry before saving.
        foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
        {
            ValidateNamespaces(solutionXml.XmlValue);
        }

        // Save the diagram to the desired output location.
        diagram.Save(outputFilePath, SaveFileFormat.Vdx);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramProcessor.ProcessDiagram("", "");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
