using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the name of the SolutionXML element we want to ensure exists.
                const string targetName = "MySolutionXML";

                // Try to find an existing SolutionXML with the target name.
                SolutionXML existing = null;
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (solXml.Name == targetName)
                    {
                        existing = solXml;
                        break;
                    }
                }

                if (existing != null)
                {
                    // Update the XML content of the existing element.
                    existing.XmlValue = "<root><updated>true</updated></root>";
                    Console.WriteLine($"Updated existing SolutionXML \"{targetName}\".");
                }
                else
                {
                    // Create a new SolutionXML element and add it to the diagram.
                    SolutionXML newSolXml = new SolutionXML();
                    newSolXml.Name = targetName;
                    newSolXml.XmlValue = "<root><value>Sample</value></root>";
                    diagram.SolutionXMLs.Add(newSolXml);
                    Console.WriteLine($"Added new SolutionXML \"{targetName}\".");
                }

                // Save the modified diagram back to a VSDX package, preserving all resources.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}