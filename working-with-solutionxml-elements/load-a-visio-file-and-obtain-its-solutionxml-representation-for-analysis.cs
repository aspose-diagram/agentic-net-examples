using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string visioFilePath = "sample.vsdx";

            // Load the Visio diagram using the Diagram(string) constructor (load rule)
            Diagram diagram = new Diagram(visioFilePath);

            // Retrieve the collection of SolutionXML objects from the loaded diagram
            SolutionXMLCollection solutionXmls = diagram.SolutionXMLs;

            // Iterate through each SolutionXML entry and output its name and XML content
            foreach (SolutionXML solXml in solutionXmls)
            {
                // The identifier of the SolutionXML entry
                string name = solXml.Name;

                // The actual XML string stored in the entry
                string xmlContent = solXml.XmlValue;

                Console.WriteLine($"SolutionXML Name: {name}");
                Console.WriteLine(xmlContent);
                Console.WriteLine(new string('-', 40));
            }

            // Release resources held by the Diagram object
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
