using System.IO;
using System;
using System.Xml;
using Aspose.Diagram;

public class SolutionXmlProcessor
{
    // Process all SolutionXML nodes in the given diagram,
    // catching malformed XML and logging detailed diagnostics.
    public static void ProcessSolutionXml(Diagram diagram)
    {
        if (diagram == null)
        {
            Console.WriteLine("Diagram instance is null. Aborting processing.");
            return;
        }

        // Iterate through the collection of SolutionXML objects stored in the diagram.
        foreach (SolutionXML solXml in diagram.SolutionXMLs)
        {
            // Basic validation of the object itself.
            if (solXml == null)
            {
                Console.WriteLine("Encountered a null SolutionXML entry in the collection.");
                continue;
            }

            string name = solXml.Name ?? "<Unnamed>";
            string xmlContent = solXml.XmlValue ?? string.Empty;

            // Log the start of processing for this node.
            Console.WriteLine($"Processing SolutionXML '{name}'.");

            // Attempt to load the XML string into an XmlDocument to validate well‑formedness.
            try
            {
                var xmlDoc = new XmlDocument();

                // Preserve whitespace for accurate diagnostics.
                xmlDoc.PreserveWhitespace = true;

                // LoadXml throws an exception if the XML is not well‑formed.
                xmlDoc.LoadXml(xmlContent);

                // If we reach this point, the XML is well‑formed.
                Console.WriteLine($"SolutionXML '{name}' is well‑formed.");
            }
            catch (XmlException xe)
            {
                // Detailed diagnostics for malformed XML.
                Console.WriteLine($"Error parsing SolutionXML '{name}': {xe.Message}");
                Console.WriteLine($"LineNumber: {xe.LineNumber}, LinePosition: {xe.LinePosition}");
                Console.WriteLine("Offending XML snippet:");
                // Show a short snippet around the error position for easier debugging.
                ShowXmlSnippet(xmlContent, xe.LineNumber, xe.LinePosition);
            }
            catch (Exception ex)
            {
                // Catch any other unexpected exceptions.
                Console.WriteLine($"Unexpected error processing SolutionXML '{name}': {ex.GetType().Name} - {ex.Message}");
            }
        }
    }

    // Helper method to display a small portion of the XML around the error location.
    private static void ShowXmlSnippet(string xml, int errorLine, int errorPosition, int contextLines = 2)
    {
        if (string.IsNullOrEmpty(xml) || errorLine <= 0)
        {
            Console.WriteLine("(No XML content available to display.)");
            return;
        }

        string[] lines = xml.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        int startLine = Math.Max(errorLine - contextLines - 1, 0);
        int endLine = Math.Min(errorLine + contextLines - 1, lines.Length - 1);

        for (int i = startLine; i <= endLine; i++)
        {
            string prefix = (i == errorLine - 1) ? ">> " : "   ";
            Console.WriteLine($"{prefix}{i + 1,4}: {lines[i]}");
        }
    }
}

// Example usage (assuming diagram loading/saving is handled elsewhere):
// Diagram diagram = Diagram.Load("input.vsdx"); // Load rule must be used elsewhere
// SolutionXmlProcessor.ProcessSolutionXml(diagram);
// diagram.Save("output.vsdx"); // Save rule must be used elsewhere

class Program
{
    static void Main(string[] args)
    {
        SolutionXmlProcessor.ProcessSolutionXml(null);
    }
}
