using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Paths for input and output diagrams
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        // Load the diagram from file with error handling
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through each SolutionXML element and clean its XML content
        foreach (SolutionXML solXml in diagram.SolutionXMLs)
        {
            if (!string.IsNullOrEmpty(solXml.XmlValue))
            {
                try
                {
                    // Parse the XML string
                    XDocument xdoc = XDocument.Parse(solXml.XmlValue);

                    // Remove all comment nodes from the XML
                    foreach (var comment in xdoc.Descendants().OfType<XComment>().ToList())
                    {
                        comment.Remove();
                    }

                    // Store the cleaned XML back into the SolutionXML element
                    solXml.XmlValue = xdoc.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
                }
                catch
                {
                    // If parsing fails, skip this element
                }
            }
        }

        // Save the modified diagram to the output file with error handling
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}