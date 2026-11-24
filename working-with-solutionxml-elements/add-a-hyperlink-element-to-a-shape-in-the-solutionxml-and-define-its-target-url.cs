using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Find the first shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No shape found on the first page to attach a hyperlink.");
                    return;
                }

                // -------------------------------------------------
                // Add a Hyperlink to the shape
                // -------------------------------------------------
                Hyperlink link = new Hyperlink();
                link.Name = "MyWebLink";
                link.Address.Value = "https://example.com";
                // Open the hyperlink in a new window
                link.NewWindow.Value = BOOL.True;
                // Add the hyperlink to the shape's collection
                targetShape.Hyperlinks.Add(link);

                // -------------------------------------------------
                // Create a SolutionXML element that stores the link
                // -------------------------------------------------
                SolutionXML solXml = new SolutionXML();
                solXml.Name = "MySolutionXML";
                // Store a simple XML representation of the hyperlink
                solXml.XmlValue = "<Hyperlink Name=\"MyWebLink\" Address=\"https://example.com\" NewWindow=\"true\" />";
                // Add the SolutionXML element to the diagram
                diagram.SolutionXMLs.Add(solXml);

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}