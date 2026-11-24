using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a report builder.
                var report = new System.Text.StringBuilder();
                report.AppendLine("Shape Count Summary per Page");
                report.AppendLine("==============================");

                // Flag to indicate whether we processed any SolutionXML.
                bool processedSolutionXml = false;

                // Iterate over all SolutionXML elements in the document.
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    // Ensure the XML content is not null or empty.
                    if (string.IsNullOrWhiteSpace(solutionXml.XmlValue))
                        continue;

                    processedSolutionXml = true;

                    // Parse the XML content.
                    XDocument xDoc;
                    try
                    {
                        xDoc = XDocument.Parse(solutionXml.XmlValue);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Warning: Failed to parse SolutionXML content. {ex.Message}");
                        continue;
                    }

                    // Count shapes per page based on the XML hierarchy.
                    // This assumes a generic structure where <Page> elements contain <Shape> elements.
                    foreach (var pageElement in xDoc.Descendants("Page"))
                    {
                        // Retrieve a page identifier (could be an attribute or element text).
                        string pageId = pageElement.Attribute("id")?.Value ??
                                        pageElement.Attribute("name")?.Value ??
                                        "Unknown";

                        // Count all descendant <Shape> elements within this page.
                        int shapeCount = pageElement.Descendants("Shape").Count();

                        report.AppendLine($"Page ID {pageId}: {shapeCount} shape(s) (derived from SolutionXML)");
                    }
                }

                // If no SolutionXML was present or usable, fall back to direct diagram inspection.
                if (!processedSolutionXml)
                {
                    int pageIndex = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        pageIndex++;
                        int shapeCount = 0;
                        foreach (Shape shape in page.Shapes)
                        {
                            // Exclude logically deleted shapes.
                            if (shape.Del == BOOL.False)
                            {
                                shapeCount++;
                            }
                        }
                        report.AppendLine($"Page {pageIndex}: {shapeCount} shape(s) (derived from diagram)");
                    }
                }

                // Output the report to the console.
                Console.WriteLine(report.ToString());
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}
