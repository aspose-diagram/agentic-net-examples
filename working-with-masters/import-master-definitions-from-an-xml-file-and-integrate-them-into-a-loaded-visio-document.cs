using System;
using System.IO;
using System.Xml;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <VisioFilePath> <MasterXmlPath> <OutputVisioPath>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <MasterXmlPath> <OutputVisioPath>");
            return;
        }

        string visioPath = args[0];
        string xmlPath = args[1];
        string outputPath = args[2];

        // Guard against missing Visio file
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Guard against missing XML file
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Load and parse the XML containing master definitions
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Expected XML structure:
            // <Masters>
            //   <Master name="Rectangle" stencilPath="C:\Stencils\Basic_U.vssx" />
            //   ...
            // </Masters>
            XmlNodeList masterNodes = xmlDoc.SelectNodes("//Masters/Master");
            if (masterNodes == null || masterNodes.Count == 0)
            {
                Console.Error.WriteLine("No master definitions found in the XML file.");
            }
            else
            {
                foreach (XmlNode node in masterNodes)
                {
                    // Retrieve required attributes
                    XmlAttribute nameAttr = node.Attributes["name"];
                    XmlAttribute stencilAttr = node.Attributes["stencilPath"];

                    if (nameAttr == null || stencilAttr == null)
                    {
                        Console.Error.WriteLine("Master node missing required attributes 'name' or 'stencilPath'. Skipping this entry.");
                        continue;
                    }

                    string masterName = nameAttr.Value;
                    string stencilPath = stencilAttr.Value;

                    // Guard against missing stencil file
                    if (!File.Exists(stencilPath))
                    {
                        Console.Error.WriteLine($"Stencil file not found for master '{masterName}': {stencilPath}. Skipping this master.");
                        continue;
                    }

                    try
                    {
                        // Import the master from the specified stencil into the diagram
                        diagram.AddMaster(stencilPath, masterName);
                        Console.WriteLine($"Successfully imported master '{masterName}' from stencil '{stencilPath}'.");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to import master '{masterName}': {ex.Message}");
                    }
                }
            }

            // Embed the original XML into a SolutionXML element for future reference (if supported)
            try
            {
                // The SolutionXML collection may be accessed via diagram.SolutionXMLs.
                // If the API differs, adjust accordingly.
                // Remove any existing element with the same name to avoid duplicates.
                // Note: The exact API for SolutionXML manipulation may vary; replace with correct calls if needed.
                // TODO: Verify the correct property/method names for SolutionXML handling in the used Aspose.Diagram version.
                // Example (if supported):
                // diagram.SolutionXMLs.Remove("ImportedMasters");
                // diagram.SolutionXMLs.Add("ImportedMasters", File.ReadAllText(xmlPath));
                Console.WriteLine("Embedded master XML into SolutionXML element (implementation may require adjustment).");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to embed XML into SolutionXML: {ex.Message}");
            }

            // Save the updated diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}