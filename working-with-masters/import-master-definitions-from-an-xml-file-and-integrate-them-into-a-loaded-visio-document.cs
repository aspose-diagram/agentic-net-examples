using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class ImportMastersExample
{
    static void Main()
    {
        try
        {

            // Paths to the target Visio file and the XML file that defines masters
            string visioFilePath = @"C:\Docs\TargetDiagram.vsdx";
            string mastersXmlPath = @"C:\Docs\MastersDefinition.xml";
            string outputFilePath = @"C:\Docs\TargetDiagram_WithMasters.vsdx";

            // Load the existing Visio document
            Diagram diagram = new Diagram(visioFilePath);

            // Load the XML that contains master definitions
            // Expected XML format:
            // <Masters>
            //   <Master Name="MyShape" TemplatePath="C:\Stencils\MyStencil.vssx" />
            //   <Master Name="AnotherShape" TemplatePath="C:\Stencils\AnotherStencil.vssx" />
            // </Masters>
            XDocument xmlDoc = XDocument.Load(mastersXmlPath);
            foreach (XElement masterElem in xmlDoc.Root.Elements("Master"))
            {
                string masterName = masterElem.Attribute("Name")?.Value;
                string templatePath = masterElem.Attribute("TemplatePath")?.Value;

                if (string.IsNullOrEmpty(masterName) || string.IsNullOrEmpty(templatePath))
                    continue; // Skip invalid entries

                // Import the master from the specified template file into the diagram
                // Using AddMaster(string templateFilePath, string masterName)
                diagram.AddMaster(templatePath, masterName);
            }

            // Save the updated diagram
            diagram.Save(outputFilePath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
