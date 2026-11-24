using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Regular expressions to identify external URLs in XML attributes or text.
            // Removes attributes like href="http://example.com" or src='https://example.com'.
            Regex urlAttributeRegex = new Regex(@"(href|src)\s*=\s*[""']https?://[^""']+[""']", RegexOptions.IgnoreCase);
            // Removes bare URLs appearing in the text content.
            Regex urlTextRegex = new Regex(@"https?://\S+", RegexOptions.IgnoreCase);

            // Iterate over all SolutionXML elements.
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (solXml == null || string.IsNullOrEmpty(solXml.XmlValue))
                    continue;

                string cleanedXml = solXml.XmlValue;

                // Remove URL attributes.
                cleanedXml = urlAttributeRegex.Replace(cleanedXml, string.Empty);
                // Remove any remaining plain URLs.
                cleanedXml = urlTextRegex.Replace(cleanedXml, string.Empty);

                // Update the SolutionXML with the sanitized content.
                solXml.XmlValue = cleanedXml;
            }

            // Save the sanitized diagram. Using Vsdx as a widely compatible format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture and report any errors from Aspose.Diagram operations.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}