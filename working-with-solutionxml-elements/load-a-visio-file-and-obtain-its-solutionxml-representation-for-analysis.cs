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

            // Load the Visio diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(visioFilePath);

            // Retrieve the collection of SolutionXML objects from the diagram
            SolutionXMLCollection solutionXmls = diagram.SolutionXMLs;

            // Iterate through each SolutionXML entry and output its details
            foreach (SolutionXML solXml in solutionXmls)
            {
                // Name of the SolutionXML entry (if available)
                Console.WriteLine($"Name: {solXml.Name}");

                // The actual XML content stored in the entry
                Console.WriteLine("XML Content:");
                Console.WriteLine(solXml.XmlValue);
                Console.WriteLine(new string('-', 40));
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
