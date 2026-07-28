using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths and font names – adjust as needed
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";
            string oldFontName = "Arial";
            string newFontName = "Calibri";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Replace font name in each SolutionXML element
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (!string.IsNullOrEmpty(solXml.XmlValue))
                {
                    solXml.XmlValue = solXml.XmlValue.Replace(oldFontName, newFontName);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
