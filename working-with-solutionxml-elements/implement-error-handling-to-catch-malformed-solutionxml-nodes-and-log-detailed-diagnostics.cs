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

            // Path to the Visio file
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all SolutionXML elements and validate their XML content
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                Console.WriteLine($"Processing SolutionXML: Name = '{solXml.Name}'");
                try
                {
                    // Attempt to parse the XML value
                    XDocument xmlDoc = XDocument.Parse(solXml.XmlValue);
                    // If needed, further validation logic can be added here
                    Console.WriteLine("  XML is well-formed.");
                }
                catch (Exception ex)
                {
                    // Log detailed diagnostics for malformed XML
                    Console.WriteLine($"  ERROR: Malformed XML detected in SolutionXML '{solXml.Name}'.");
                    Console.WriteLine($"  Exception Message: {ex.Message}");
                    Console.WriteLine($"  StackTrace: {ex.StackTrace}");
                    // Optionally, you could decide to remove or fix the malformed entry here
                }
            }

            // Save the diagram (even if no changes were made) to demonstrate proper save usage
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
