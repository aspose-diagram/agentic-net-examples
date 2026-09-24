using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.Xml;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Variables to hold the extracted dimensions (in inches)
            double widthInches = 0;
            double heightInches = 0;

            // Search for a SolutionXML element that stores page dimensions
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (solXml.Name == "PageDimensions")
                {
                    // Parse the XML content of the SolutionXML element
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(solXml.XmlValue);

                    XmlNode widthNode = xmlDoc.SelectSingleNode("//Width");
                    XmlNode heightNode = xmlDoc.SelectSingleNode("//Height");

                    if (widthNode != null && double.TryParse(widthNode.InnerText, out double w))
                        widthInches = w;

                    if (heightNode != null && double.TryParse(heightNode.InnerText, out double h))
                        heightInches = h;

                    break; // Dimensions found, exit loop
                }
            }

            // If dimensions were successfully retrieved, apply them to each page
            if (widthInches > 0 && heightInches > 0)
            {
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PageProps.PageWidth.Value = widthInches;
                    page.PageSheet.PageProps.PageHeight.Value = heightInches;
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved with updated page dimensions.");
            }
            else
            {
                Console.WriteLine("Page dimensions not found in SolutionXML. No changes applied.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
