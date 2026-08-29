using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define output file path
        string outputPath = "output_cleaned.vsdx";

        try
        {
            // Load the Visio diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each SolutionXML element to clean its XML content
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                // Skip if the XML value is null or whitespace
                if (string.IsNullOrWhiteSpace(solutionXml.XmlValue))
                    continue;

                // Parse the XML content while preserving whitespace
                XDocument doc = XDocument.Parse(solutionXml.XmlValue, System.Xml.Linq.LoadOptions.PreserveWhitespace);

                // Remove all comment nodes from the XML document
                foreach (var comment in doc.DescendantNodes().OfType<XComment>())
                {
                    comment.Remove();
                }

                // Write the cleaned XML back to the SolutionXML element
                solutionXml.XmlValue = doc.ToString();
            }

            // Save the cleaned diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}