using System.IO;
using System;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;

class SolutionXmlSecurityPolicy
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all SolutionXML entries in the diagram
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // Skip if the XML content is null or empty
                if (string.IsNullOrEmpty(solXml.XmlValue))
                    continue;

                XDocument xDoc;
                try
                {
                    // Parse the XML string into an XDocument for manipulation
                    xDoc = XDocument.Parse(solXml.XmlValue);
                }
                catch
                {
                    // If parsing fails, leave the original XML unchanged
                    continue;
                }

                // ------------------------------------------------------------
                // Remove potential external references
                // ------------------------------------------------------------

                // 1. Remove attributes that point to external resources (e.g., href, src)
                var elementsWithExternalAttrs = xDoc.Descendants()
                    .Where(e => e.Attributes().Any(a =>
                        (a.Name.LocalName.Equals("href", StringComparison.OrdinalIgnoreCase) ||
                         a.Name.LocalName.Equals("src", StringComparison.OrdinalIgnoreCase)) &&
                        !string.IsNullOrWhiteSpace(a.Value)))
                    .ToList();

                foreach (var element in elementsWithExternalAttrs)
                {
                    // Remove only the external attributes, keep other attributes intact
                    var attrsToRemove = element.Attributes()
                        .Where(a => a.Name.LocalName.Equals("href", StringComparison.OrdinalIgnoreCase) ||
                                    a.Name.LocalName.Equals("src", StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    foreach (var attr in attrsToRemove)
                        attr.Remove();
                }

                // 2. Remove entire elements that are explicitly marked as external references
                var externalReferenceElements = xDoc.Descendants()
                    .Where(e => e.Name.LocalName.Equals("ExternalReference", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var extElem in externalReferenceElements)
                    extElem.Remove();

                // ------------------------------------------------------------
                // Write the sanitized XML back to the SolutionXML object
                // ------------------------------------------------------------
                // Preserve XML declaration if present
                string cleanedXml = xDoc.Declaration != null
                    ? xDoc.Declaration.ToString() + Environment.NewLine + xDoc.ToString()
                    : xDoc.ToString();

                solXml.XmlValue = cleanedXml;
            }

            // Save the modified diagram (uses the provided save rule)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
