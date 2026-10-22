using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    /// <summary>
    /// Injects or updates a SolutionXML element with a predefined style sheet XML.
    /// </summary>
    /// <param name="inputPath">Path to the source Visio file.</param>
    /// <param name="outputPath">Path where the modified Visio file will be saved.</param>
    /// <param name="solutionXmlName">Name identifier for the SolutionXML element.</param>
    /// <param name="styleSheetXml">The XML string representing the style sheet to inject.</param>
    static void InjectStyleSheet(string inputPath, string outputPath, string solutionXmlName, string styleSheetXml)
    {
        // Guard: ensure source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Search for an existing SolutionXML with the specified name
            SolutionXML target = null;
            foreach (SolutionXML s in diagram.SolutionXMLs)
            {
                if (s.Name == solutionXmlName)
                {
                    target = s;
                    break;
                }
            }

            if (target == null)
            {
                // Create a new SolutionXML element if it does not exist
                target = new SolutionXML();
                target.Name = solutionXmlName;
                diagram.SolutionXMLs.Add(target);
            }

            // Inject or replace the XML content
            target.XmlValue = styleSheetXml;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Style sheet injected successfully. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - input Visio file path
        // args[1] - output Visio file path
        // args[2] - SolutionXML name (identifier)
        // args[3] - style sheet file path (XML)

        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <solutionXmlName> <styleSheetFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string solutionXmlName = args[2];
        string styleSheetPath = args[3];

        // Guard: ensure style sheet file exists
        if (!File.Exists(styleSheetPath))
        {
            Console.Error.WriteLine($"Style sheet file not found: {styleSheetPath}");
            return;
        }

        // Read the style sheet XML content
        string styleSheetXml;
        try
        {
            styleSheetXml = File.ReadAllText(styleSheetPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read style sheet file: {ex.Message}");
            return;
        }

        // Call the reusable injection function
        InjectStyleSheet(inputPath, outputPath, solutionXmlName, styleSheetXml);
    }
}