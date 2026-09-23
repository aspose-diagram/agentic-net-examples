using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output XML file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramCommentExport <inputVisioFile> <outputXmlFile>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the Visio diagram from the specified file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare an XML document with a root element for comments
                XDocument xmlDoc = new XDocument(new XElement("Comments"));

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page name directly (Page.Name is a string)
                    string pageName = page.Name;

                    // Iterate through annotations (comments) on the current page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Extract required fields from the annotation
                        long markerId = annotation.MarkerIndex.Value;      // unique comment identifier
                        string commentText = annotation.Comment.Value;    // comment text
                        int reviewerId = annotation.ReviewerID.Value;     // reviewer index
                        int shapeId = annotation.ShapeID;                 // associated shape ID (primitive int)

                        // Build an XML element representing this comment
                        XElement commentElement = new XElement("Comment",
                            new XAttribute("Page", pageName),
                            new XAttribute("MarkerId", markerId),
                            new XAttribute("ReviewerId", reviewerId),
                            new XAttribute("ShapeId", shapeId),
                            new XElement("Text", commentText)
                        );

                        // Add the comment element to the root of the XML document
                        xmlDoc.Root.Add(commentElement);
                    }
                }

                // Save the XML document to the specified output path
                xmlDoc.Save(outputPath);
                Console.WriteLine($"Comments exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error exporting comments: {ex.Message}");
        }
    }
}