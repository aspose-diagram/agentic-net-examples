using System.IO;
using System;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the sanitized output file
            string outputPath = "output_sanitized.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Regular expression to match typical external references (http, https, file URLs)
            Regex externalRefPattern = new Regex(@"(https?://\S+)|(file://\S+)", RegexOptions.IgnoreCase);

            // Iterate through all SolutionXML elements and remove external references
            foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
            {
                if (!string.IsNullOrEmpty(solutionXml.XmlValue))
                {
                    // Replace any external reference with an empty string
                    string sanitizedXml = externalRefPattern.Replace(solutionXml.XmlValue, string.Empty);
                    solutionXml.XmlValue = sanitizedXml;
                }
            }

            // Save the sanitized diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
