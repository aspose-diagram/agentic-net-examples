using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        Diagram diagram = null;

        try
        {
            // Load the Visio diagram
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            // Log any loading errors
            Console.Error.WriteLine($"Failed to load diagram '{diagramPath}': {ex.Message}");
            return;
        }

        // Iterate through all SolutionXML elements and validate their XML content
        foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
        {
            try
            {
                // Attempt to parse the stored XML value
                XDocument.Parse(solutionXml.XmlValue);
                // If parsing succeeds, optionally log success
                Console.WriteLine($"SolutionXML '{solutionXml.Name}' is well-formed.");
            }
            catch (Exception ex)
            {
                // Log detailed diagnostics for malformed XML
                Console.Error.WriteLine($"Malformed SolutionXML detected:");
                Console.Error.WriteLine($"  Name      : {solutionXml.Name}");
                Console.Error.WriteLine($"  Error     : {ex.Message}");
                // If the exception provides line info, include it
                if (ex is System.Xml.XmlException xmlEx)
                {
                    Console.Error.WriteLine($"  Line      : {xmlEx.LineNumber}, Position: {xmlEx.LinePosition}");
                }
            }
        }

        // Example: add a new well-formed SolutionXML element (optional)
        try
        {
            var newSolutionXml = new SolutionXML
            {
                Name = "ExampleSolution",
                XmlValue = "<Root><Data>Sample</Data></Root>"
            };
            diagram.SolutionXMLs.Add(newSolutionXml);
            // Save the updated diagram to a new file
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "Updated_" + Path.GetFileName(diagramPath));
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with new SolutionXML to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while adding or saving new SolutionXML: {ex.Message}");
        }
    }
}