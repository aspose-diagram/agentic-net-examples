using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Define a custom XML fragment that will hold the version information
            string versionXml = "<VersionTracking><Version>1.0.0</Version></VersionTracking>";

            // Create a SolutionXML object with a unique name and the XML content
            Aspose.Diagram.SolutionXML versionSolutionXml = new Aspose.Diagram.SolutionXML("VersionTracking", versionXml);

            // Add the SolutionXML entry to the diagram's collection
            diagram.SolutionXMLs.Add(versionSolutionXml);

            // Save the modified diagram (replace with your desired output path and format)
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
