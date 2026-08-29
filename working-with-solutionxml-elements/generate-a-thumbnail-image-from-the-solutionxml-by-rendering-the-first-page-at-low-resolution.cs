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

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output thumbnail image path
            string outputPath = "thumbnail.png";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram does not contain any pages.");
            }

            // Configure image save options for a low‑resolution thumbnail
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
            options.PageIndex = 0;          // render the first page
            options.PageCount = 1;          // only one page
            options.Resolution = 72;        // low DPI
            options.Scale = 0.5f;           // reduce size to 50%

            // Save the thumbnail image
            diagram.Save(outputPath, options);

            // Optional: display any SolutionXML entries present in the diagram
            Console.WriteLine("SolutionXML entries:");
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                Console.WriteLine($"Name: {solXml.Name}");
                Console.WriteLine($"XmlValue: {solXml.XmlValue}");
            }

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Thumbnail generated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
