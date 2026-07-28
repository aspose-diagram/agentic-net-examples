using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define custom XML containing version information
            string versionXml = "<VersionInfo><Version>1.2.3</Version></VersionInfo>";

            // Create a SolutionXML object with a unique name and the XML value
            SolutionXML versionSolutionXml = new SolutionXML("VersionTracking", versionXml);

            // Add the SolutionXML to the diagram's collection
            diagram.SolutionXMLs.Add(versionSolutionXml);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
