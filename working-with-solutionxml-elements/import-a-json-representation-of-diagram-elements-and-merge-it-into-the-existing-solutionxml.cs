using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, JSON file path, output diagram path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <jsonFilePath> <outputDiagramPath>");
            return;
        }

        string inputDiagramPath = args[0];
        string jsonFilePath = args[1];
        string outputDiagramPath = args[2];

        // Guard: verify input diagram exists
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        // Guard: verify JSON file exists
        if (!File.Exists(jsonFilePath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonFilePath}");
            return;
        }

        // Read JSON content and validate it is well‑formed
        string jsonContent;
        try
        {
            jsonContent = File.ReadAllText(jsonFilePath);
            // Validate JSON structure (throws if invalid)
            using (JsonDocument.Parse(jsonContent)) { }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse JSON file: {ex.Message}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputDiagramPath))
            {
                // Define a name for the SolutionXML element that will hold the imported JSON
                const string solutionXmlName = "ImportedJson";

                // Search for an existing SolutionXML with the same name
                SolutionXML existing = null;
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (solXml.Name == solutionXmlName)
                    {
                        existing = solXml;
                        break;
                    }
                }

                if (existing != null)
                {
                    // Update the existing element's XML value with the new JSON content
                    existing.XmlValue = jsonContent;
                }
                else
                {
                    // Create a new SolutionXML element and add it to the diagram
                    SolutionXML newSolXml = new SolutionXML();
                    newSolXml.Name = solutionXmlName;
                    newSolXml.XmlValue = jsonContent;
                    diagram.SolutionXMLs.Add(newSolXml);
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram updated and saved successfully.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}