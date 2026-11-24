using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Reusable function that injects a predefined style sheet into a Visio diagram
    // and stores the style sheet XML inside a SolutionXML element.
    static void InjectPredefinedStyleSheet(string inputDiagramPath, string outputDiagramPath, string styleSheetName, string styleSheetXml)
    {
        // Guard: ensure the input file exists
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputDiagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputDiagramPath);

            // Create a new StyleSheet with the provided name
            StyleSheet styleSheet = new StyleSheet();
            styleSheet.Name = styleSheetName; // StyleSheet.Name is a plain string

            // Add the StyleSheet to the diagram's collection
            diagram.StyleSheets.Add(styleSheet);

            // Create a SolutionXML element that holds the style sheet XML
            SolutionXML solutionXml = new SolutionXML();
            solutionXml.Name = styleSheetName;      // Identifier for the SolutionXML element
            solutionXml.XmlValue = styleSheetXml;   // The actual XML content

            // Add the SolutionXML element to the diagram
            diagram.SolutionXMLs.Add(solutionXml);

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Style sheet '{styleSheetName}' injected and diagram saved to: {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    static void Main(string[] args)
    {
        // Example usage:
        // args[0] - input Visio file path
        // args[1] - output Visio file path
        // args[2] - style sheet name
        // args[3] - style sheet XML content (or path to XML file)

        if (args.Length < 4)
        {
            Console.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <styleSheetName> <styleSheetXmlOrPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string styleSheetName = args[2];
        string styleSheetXmlInput = args[3];
        string styleSheetXml;

        // Determine if the fourth argument is a file path or raw XML
        if (File.Exists(styleSheetXmlInput))
        {
            // Load XML content from file
            styleSheetXml = File.ReadAllText(styleSheetXmlInput);
        }
        else
        {
            // Treat argument as raw XML string
            styleSheetXml = styleSheetXmlInput;
        }

        // Call the reusable function
        InjectPredefinedStyleSheet(inputPath, outputPath, styleSheetName, styleSheetXml);
    }
}