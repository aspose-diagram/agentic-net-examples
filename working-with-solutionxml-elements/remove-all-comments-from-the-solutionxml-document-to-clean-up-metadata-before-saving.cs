using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;

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

            // Iterate over all SolutionXML elements in the document.
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // Skip if the XML content is null or empty.
                if (string.IsNullOrWhiteSpace(solXml.XmlValue))
                    continue;

                try
                {
                    // Parse the XML string into an XDocument.
                    XDocument xDoc = XDocument.Parse(solXml.XmlValue);

                    // Remove all XML comment nodes (<!-- comment -->).
                    foreach (var comment in xDoc.DescendantNodes().OfType<XComment>().ToList())
                    {
                        comment.Remove();
                    }

                    // Write the cleaned XML back to the SolutionXML element.
                    solXml.XmlValue = xDoc.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
                }
                catch (Exception ex)
                {
                    // If parsing fails, write an error but continue processing other elements.
                    Console.Error.WriteLine($"Failed to process SolutionXML '{solXml.Name}': {ex.Message}");
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}