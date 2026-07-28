using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define new canvas size (in inches)
                double newWidth = 11.0;   // example width
                double newHeight = 8.5;   // example height

                // Update the size of each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PageProps.PageWidth.Value = newWidth;
                    page.PageSheet.PageProps.PageHeight.Value = newHeight;
                }

                // Update (or create) a SolutionXML entry that stores the canvas size
                const string solutionXmlName = "PageDimensions";
                bool found = false;
                foreach (SolutionXML solXml in diagram.SolutionXMLs)
                {
                    if (solXml.Name == solutionXmlName)
                    {
                        solXml.XmlValue = $"<Page width=\"{newWidth}\" height=\"{newHeight}\" />";
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    SolutionXML newSolXml = new SolutionXML();
                    newSolXml.Name = solutionXmlName;
                    newSolXml.XmlValue = $"<Page width=\"{newWidth}\" height=\"{newHeight}\" />";
                    diagram.SolutionXMLs.Add(newSolXml);
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