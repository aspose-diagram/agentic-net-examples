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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Desired canvas size in inches (example: A4 size)
            double newWidth = 8.27;
            double newHeight = 11.69;

            // Update the width and height of every page in the document
            foreach (Page page in diagram.Pages)
            {
                page.PageSheet.PageProps.PageWidth.Value = newWidth;
                page.PageSheet.PageProps.PageHeight.Value = newHeight;
            }

            // Update (or add) a SolutionXML element that stores the page size
            string solutionXmlName = "PageSize";
            bool found = false;
            foreach (SolutionXML sol in diagram.SolutionXMLs)
            {
                if (sol.Name == solutionXmlName)
                {
                    sol.XmlValue = $"<PageSize Width=\"{newWidth}\" Height=\"{newHeight}\"/>";
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                SolutionXML newSol = new SolutionXML
                {
                    Name = solutionXmlName,
                    XmlValue = $"<PageSize Width=\"{newWidth}\" Height=\"{newHeight}\"/>"
                };
                diagram.SolutionXMLs.Add(newSol);
            }

            // Save the modified diagram back to a Visio file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Page dimensions updated and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
