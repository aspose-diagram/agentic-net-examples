using System;
using System.IO;
using Aspose.Diagram;
using System.Xml.Linq;
using System.Linq;

public static class DiagramSecurity
{
    // Removes external references from all SolutionXML entries in the diagram.
    public static void RemoveExternalReferences(Diagram diagram)
    {
        // Iterate over each SolutionXML in the diagram.
        foreach (SolutionXML solXml in diagram.SolutionXMLs)
        {
            if (string.IsNullOrEmpty(solXml.XmlValue))
                continue;

            // Parse the XML content.
            XDocument doc;
            try
            {
                doc = XDocument.Parse(solXml.XmlValue);
            }
            catch
            {
                // Skip entries with invalid XML.
                continue;
            }

            // Remove attributes that point to external resources (http/https).
            foreach (var element in doc.Descendants())
            {
                var href = element.Attribute("href");
                if (href != null && IsExternalReference(href.Value))
                    href.Remove();

                var src = element.Attribute("src");
                if (src != null && IsExternalReference(src.Value))
                    src.Remove();
            }

            // Remove any elements explicitly named "ExternalReference".
            foreach (var extRef in doc.Descendants("ExternalReference").ToList())
                extRef.Remove();

            // Write the cleaned XML back.
            solXml.XmlValue = doc.ToString(SaveOptions.DisableFormatting);
        }
    }

    // Determines whether a URI is external (starts with http/https).
    private static bool IsExternalReference(string uri)
    {
        return uri.StartsWith("http://", System.StringComparison.OrdinalIgnoreCase) ||
               uri.StartsWith("https://", System.StringComparison.OrdinalIgnoreCase);
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            DiagramSecurity.RemoveExternalReferences(null);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
