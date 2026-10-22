using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Modify SolutionXML collection
                // ------------------------------------------------------------
                // Example: add a new SolutionXML element or update an existing one
                string targetName = "MySolutionData";
                string newXmlContent = "<Root><Data>Sample</Data></Root>";

                // Try to find an existing SolutionXML with the target name
                SolutionXML existing = null;
                foreach (SolutionXML s in diagram.SolutionXMLs)
                {
                    if (s.Name == targetName)
                    {
                        existing = s;
                        break;
                    }
                }

                if (existing != null)
                {
                    // Update the XML value of the existing element
                    existing.XmlValue = newXmlContent;
                }
                else
                {
                    // Create and add a new SolutionXML element
                    SolutionXML solXml = new SolutionXML();
                    solXml.Name = targetName;
                    solXml.XmlValue = newXmlContent;
                    diagram.SolutionXMLs.Add(solXml);
                }

                // ------------------------------------------------------------
                // Save the modified diagram back to VSDX, preserving all resources
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}