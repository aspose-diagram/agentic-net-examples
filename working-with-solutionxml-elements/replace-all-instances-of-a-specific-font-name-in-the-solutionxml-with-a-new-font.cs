using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input parameters
                string diagramPath = "input.vsdx";          // Path to the Visio file
                string outputPath = "output.vsdx";          // Path where the modified file will be saved
                string oldFontName = "Arial";               // Font name to be replaced
                string newFontName = "Calibri";             // New font name

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate over all SolutionXML elements and replace the font name in the XML content
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    if (!string.IsNullOrEmpty(solutionXml.XmlValue) && solutionXml.XmlValue.Contains(oldFontName))
                    {
                        solutionXml.XmlValue = solutionXml.XmlValue.Replace(oldFontName, newFontName);
                        Console.WriteLine($"Replaced font in SolutionXML named '{solutionXml.Name}'.");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }