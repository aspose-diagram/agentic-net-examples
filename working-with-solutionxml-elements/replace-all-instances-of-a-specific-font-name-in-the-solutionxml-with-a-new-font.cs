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

                // Font names to replace
                string oldFontName = "Arial";
                string newFontName = "Calibri";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all SolutionXML elements and replace the font name in the XML content
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    if (!string.IsNullOrEmpty(solutionXml.XmlValue) && solutionXml.XmlValue.Contains(oldFontName))
                    {
                        solutionXml.XmlValue = solutionXml.XmlValue.Replace(oldFontName, newFontName);
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