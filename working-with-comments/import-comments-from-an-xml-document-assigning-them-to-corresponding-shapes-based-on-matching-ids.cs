using System.IO;
using System;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <diagramPath> <commentsXmlPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string commentsXmlPath = args[1];
        string outputPath = args[2];

        // Load the Visio diagram
        Diagram diagram = new Diagram(diagramPath);

        // Load the XML containing comments
        XDocument xmlDoc = XDocument.Load(commentsXmlPath);

        // Iterate over each comment entry in the XML
        foreach (var commentNode in xmlDoc.Descendants("Comment"))
        {
            // Extract ShapeID and comment text
            long shapeId = (long)commentNode.Element("ShapeID");
            string commentText = (string)commentNode.Element("Text") ?? string.Empty;

            // Find the shape and its page that matches the ShapeID
            Shape targetShape = null;
            Page targetPage = null;

            foreach (Page page in diagram.Pages)
            {
                try
                {
                    // GetShape throws if the ID does not exist on this page
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null && shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        targetPage = page;
                        break;
                    }
                }
                catch
                {
                    // Shape not on this page; continue searching
                }
            }

            // If the shape was found, add the comment to it
            if (targetShape != null && targetPage != null)
            {
                targetPage.AddComment(targetShape, commentText);
            }
            else
            {
                Console.WriteLine($"Warning: Shape with ID {shapeId} not found or is deleted.");
            }
        }

        // Save the updated diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
