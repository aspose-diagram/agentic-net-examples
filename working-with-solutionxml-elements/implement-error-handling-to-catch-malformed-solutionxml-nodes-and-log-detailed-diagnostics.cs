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

            // Path to the Visio file (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all SolutionXML elements and validate their XML content
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                Console.WriteLine($"Processing SolutionXML: Name = '{solutionXml.Name}'");
                try
                {
                    // Attempt to parse the XML value
                    XDocument.Parse(solutionXml.XmlValue);
                    Console.WriteLine("  XML is well-formed.");
                }
                catch (Exception ex)
                {
                    // Log detailed diagnostics for malformed XML
                    Console.WriteLine("  ERROR: Malformed XML detected.");
                    Console.WriteLine($"  Exception Type: {ex.GetType().FullName}");
                    Console.WriteLine($"  Message: {ex.Message}");
                    // If the exception provides line info, display it
                    if (ex is System.Xml.XmlException xmlEx)
                    {
                        Console.WriteLine($"  Line Number: {xmlEx.LineNumber}, Position: {xmlEx.LinePosition}");
                    }
                }
            }

            // Save the diagram (no modifications made, just demonstrating save lifecycle)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
