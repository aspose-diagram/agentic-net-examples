using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Name of the SolutionXML element we want to work with
            string targetSolutionXmlName = "MyShapeData";

            // Locate the SolutionXML by name; if it doesn't exist, create a new one
            SolutionXML targetSolutionXml = null;
            foreach (SolutionXML s in diagram.SolutionXMLs)
            {
                if (s.Name == targetSolutionXmlName)
                {
                    targetSolutionXml = s;
                    break;
                }
            }

            if (targetSolutionXml == null)
            {
                targetSolutionXml = new SolutionXML();
                targetSolutionXml.Name = targetSolutionXmlName;
                diagram.SolutionXMLs.Add(targetSolutionXml);
            }

            // Define the custom data row to add (example XML fragment)
            string newDataRow = "<CustomData ShapeId=\"123\">SomeValue</CustomData>";

            // Append the new row to the existing XmlValue
            if (string.IsNullOrEmpty(targetSolutionXml.XmlValue))
            {
                targetSolutionXml.XmlValue = newDataRow;
            }
            else
            {
                targetSolutionXml.XmlValue += newDataRow;
            }

            // Save the updated diagram back to a file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
