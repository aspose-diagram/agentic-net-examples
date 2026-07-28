using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Collect SolutionXML items that contain external references
            List<SolutionXML> externalRefs = new List<SolutionXML>();

            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                string xml = solXml.XmlValue;
                if (!string.IsNullOrEmpty(xml) &&
                    (xml.Contains("http://") || xml.Contains("https://") || xml.Contains("file://")))
                {
                    externalRefs.Add(solXml);
                }
            }

            // Remove the identified external references from the collection
            foreach (SolutionXML solXml in externalRefs)
            {
                diagram.SolutionXMLs.Remove(solXml);
            }

            // Save the sanitized diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
