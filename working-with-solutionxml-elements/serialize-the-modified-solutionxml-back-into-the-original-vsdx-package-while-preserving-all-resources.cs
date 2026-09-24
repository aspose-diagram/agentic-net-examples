using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the original VSDX file
            string inputPath = "input.vsdx";
            // Path where the modified VSDX will be saved
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Example modification: update or add a SolutionXML element
            string targetName = "MySolutionData";
            string newXml = "<root><value>123</value></root>";

            bool found = false;
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (solXml.Name == targetName)
                {
                    solXml.XmlValue = newXml;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                SolutionXML solXml = new SolutionXML();
                solXml.Name = targetName;
                solXml.XmlValue = newXml;
                diagram.SolutionXMLs.Add(solXml);
            }

            // Save the diagram back to VSDX, preserving all resources
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
