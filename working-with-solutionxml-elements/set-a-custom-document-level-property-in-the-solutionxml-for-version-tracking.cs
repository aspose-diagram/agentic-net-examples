using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create a SolutionXML element to store version information
            SolutionXML versionInfo = new SolutionXML();
            versionInfo.Name = "VersionInfo";
            versionInfo.XmlValue = diagram.Version; // Use the diagram's built‑in version string

            // Add the custom SolutionXML to the document
            diagram.SolutionXMLs.Add(versionInfo);

            // Save the diagram with the new custom property
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
