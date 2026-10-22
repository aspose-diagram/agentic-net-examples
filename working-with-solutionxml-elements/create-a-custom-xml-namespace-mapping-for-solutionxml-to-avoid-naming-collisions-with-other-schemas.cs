using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Define a custom XML namespace mapping to avoid collisions
            // The XML includes a unique namespace prefix "custom" bound to a URI
            string customXml = @"<root xmlns:custom=""http://example.com/custom"">" +
                               @"<custom:Data>Sample content</custom:Data>" +
                               @"</root>";

            // Create a new SolutionXML element with a distinct name
            SolutionXML solutionXml = new SolutionXML();
            solutionXml.Name = "CustomNamespaceMapping";
            solutionXml.XmlValue = customXml;

            // Add the SolutionXML element to the diagram's collection
            diagram.SolutionXMLs.Add(solutionXml);

            // Save the modified diagram to the output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram saved successfully with custom SolutionXML.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}