using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputXmlFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "Comments.xml";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create the root element for the XML document
                XDocument xmlDoc = new XDocument(new XElement("Comments"));

                // Iterate over each page in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    // Retrieve the page with explicit typing
                    Page page = diagram.Pages[pageIndex];

                    // Determine a readable page name
                    string pageName = page.Name ?? $"Page_{pageIndex + 1}";

                    // Create an XML element for this page
                    XElement pageElement = new XElement("Page",
                        new XAttribute("Name", pageName));

                    // Iterate over all annotations (comments) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Extract comment details safely
                        string commentText = annotation.Comment?.Value ?? string.Empty;
                        int reviewerId = annotation.ReviewerID?.Value ?? 0;
                        int shapeId = annotation.ShapeID;
                        DateTime commentDate = annotation.Date?.Value ?? DateTime.MinValue;

                        // Build the XML element for the individual comment
                        XElement commentElement = new XElement("Comment",
                            new XAttribute("ReviewerID", reviewerId),
                            new XAttribute("ShapeID", shapeId),
                            new XAttribute("Date", commentDate.ToString("o")),
                            commentText);

                        // Add the comment to the page element
                        pageElement.Add(commentElement);
                    }

                    // Add the page element to the root of the document
                    xmlDoc.Root.Add(pageElement);
                }

                // Save the XML document to the specified output path
                xmlDoc.Save(outputPath);
                Console.WriteLine($"Comments exported successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}