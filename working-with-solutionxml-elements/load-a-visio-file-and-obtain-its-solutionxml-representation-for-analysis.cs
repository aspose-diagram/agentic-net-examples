using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be loaded
                string visioPath = "input.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Verify that the diagram contains any SolutionXML elements
                if (diagram.SolutionXMLs.Count == 0)
                {
                    Console.WriteLine("No SolutionXML elements found in the diagram.");
                    return;
                }

                // Iterate through each SolutionXML element and display its data
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    Console.WriteLine($"Name: {solXml.Name}");
                    Console.WriteLine("XML Content:");
                    Console.WriteLine(solXml.XmlValue);
                    Console.WriteLine(new string('-', 40));
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }