using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Dictionary to hold shape counts per page
                Dictionary<string, int> shapeCountsPerPage = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                bool solutionXmlFound = false;

                // Attempt to read SolutionXML elements from the diagram
                foreach (SolutionXML solutionXml in diagram.SolutionXMLs)
                {
                    // Mark that at least one SolutionXML element exists
                    solutionXmlFound = true;

                    // The XML content is stored in the XmlValue property
                    string xmlContent = solutionXml.XmlValue;
                    if (string.IsNullOrWhiteSpace(xmlContent))
                    {
                        continue; // Skip empty entries
                    }

                    // Parse the XML string
                    XDocument xDoc = XDocument.Parse(xmlContent);

                    // Iterate over each <Page> element in the SolutionXML
                    foreach (XElement pageElem in xDoc.Descendants("Page"))
                    {
                        // Determine the page name (fallback to "UnnamedPage")
                        string pageName = (string)pageElem.Attribute("Name") ??
                                          (string)pageElem.Attribute("NameU") ??
                                          "UnnamedPage";

                        // Count all descendant <Shape> elements within this page element
                        int shapeCount = pageElem.Descendants("Shape").Count();

                        // Store or overwrite the count for this page
                        shapeCountsPerPage[pageName] = shapeCount;
                    }
                }

                // If no SolutionXML was present, fall back to counting shapes via the Diagram object
                if (!solutionXmlFound)
                {
                    int pageIndex = 0; // Manual counter because Page has no Index property
                    foreach (Page page in diagram.Pages)
                    {
                        pageIndex++;

                        // Use the page's Name if available; otherwise generate a default name
                        string pageName = !string.IsNullOrEmpty(page.Name)
                            ? page.Name
                            : $"Page_{pageIndex}";

                        int shapeCount = 0;
                        foreach (Shape shape in page.Shapes)
                        {
                            // Exclude logically deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            shapeCount++;
                        }

                        shapeCountsPerPage[pageName] = shapeCount;
                    }
                }

                // Output the summary report
                Console.WriteLine("Shape Count Summary per Page:");
                Console.WriteLine(new string('-', 40));

                int totalShapes = 0;
                foreach (KeyValuePair<string, int> kvp in shapeCountsPerPage)
                {
                    Console.WriteLine($"Page: {kvp.Key} - Shapes: {kvp.Value}");
                    totalShapes += kvp.Value;
                }

                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"Total Shapes Across All Pages: {totalShapes}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}