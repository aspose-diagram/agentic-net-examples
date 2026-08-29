using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Create custom XML for version tracking
            string customXml = "<VersionInfo><Version>1.0.0</Version></VersionInfo>";

            // Add the custom SolutionXML entry to the document
            Aspose.Diagram.SolutionXML versionXml = new Aspose.Diagram.SolutionXML("CustomVersionInfo", customXml);
            diagram.SolutionXMLs.Add(versionXml);

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
