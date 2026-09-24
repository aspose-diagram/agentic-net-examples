using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file to be loaded
            string inputPath = "input.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all SolutionXML elements and output their data
            if (diagram.SolutionXMLs.Count == 0)
            {
                Console.WriteLine("No SolutionXML elements found in the diagram.");
            }
            else
            {
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    Console.WriteLine($"Name: {solXml.Name}");
                    Console.WriteLine("XML Content:");
                    Console.WriteLine(solXml.XmlValue);
                    Console.WriteLine(new string('-', 40));
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
