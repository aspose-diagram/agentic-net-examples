using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.Xml.Linq;
using System.Xml;

/// <summary>
/// Demonstrates loading a Visio diagram, validating the XML namespaces
/// of all SolutionXML elements, and saving the diagram.
/// </summary>
class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";

        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the diagram (Aspose operation)
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            // Report loading errors
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Validate SolutionXML namespaces before saving
        ValidateSolutionXmlNamespaces(diagram);

        // Path for the output Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Save the diagram after successful validation (Aspose operation)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Report saving errors
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Parses each SolutionXML.XmlValue and ensures that the XML is well‑formed
    /// and that all namespace prefixes are properly declared.
    /// Throws an exception if any validation error is found.
    /// </summary>
    /// <param name="diagram">The diagram whose SolutionXML collection will be validated.</param>
    private static void ValidateSolutionXmlNamespaces(Diagram diagram)
    {
        foreach (SolutionXML solXml in diagram.SolutionXMLs)
        {
            if (string.IsNullOrWhiteSpace(solXml.XmlValue))
            {
                // Empty XML is considered valid; continue to next item
                continue;
            }

            try
            {
                // Parse the XML string. XDocument will throw if the XML is not well‑formed
                // or if a namespace prefix is used without a corresponding declaration.
                XDocument.Parse(solXml.XmlValue, System.Xml.Linq.LoadOptions.SetLineInfo);
            }
            catch (XmlException ex)
            {
                // Include the name of the SolutionXML element in the error message
                throw new Exception($"Invalid XML in SolutionXML '{solXml.Name}': {ex.Message}", ex);
            }
        }
    }
}