using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        // Desired page dimensions in inches
        double newWidth = 11.0;   // e.g., Letter width
        double newHeight = 8.5;   // e.g., Letter height

        try
        {
            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Update dimensions for each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PageProps.PageWidth.Value = newWidth;
                page.PageSheet.PageProps.PageHeight.Value = newHeight;
            }

            // Update or add a SolutionXML element that stores canvas size
            string solutionXmlName = "CanvasSize";
            string newXmlValue = $"<Canvas><Width>{newWidth}</Width><Height>{newHeight}</Height></Canvas>";

            bool found = false;
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (solXml.Name == solutionXmlName)
                {
                    solXml.XmlValue = newXmlValue;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                // Create a new SolutionXML entry if it does not exist
                SolutionXML canvasXml = new SolutionXML();
                canvasXml.Name = solutionXmlName;
                canvasXml.XmlValue = newXmlValue;
                diagram.SolutionXMLs.Add(canvasXml);
            }

            // Save the modified diagram back to a Visio file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Propagate any errors
            throw new Exception("Failed to update diagram canvas size: " + ex.Message, ex);
        }
    }
}