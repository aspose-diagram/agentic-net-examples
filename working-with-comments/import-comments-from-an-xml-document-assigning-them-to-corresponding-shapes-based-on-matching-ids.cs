using System.IO;
using System;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;

class ImportComments
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with the provided load rule if available)
            Diagram diagram = LoadDiagram("input.vsdx");

            // Load the XML file containing comments
            XDocument xmlDoc = XDocument.Load("comments.xml");

            // Iterate through each comment element in the XML
            foreach (XElement commentElem in xmlDoc.Root.Elements("Comment"))
            {
                // Extract the shape ID and comment text
                long shapeId = (long)commentElem.Attribute("ShapeID");
                string commentText = commentElem.Value;

                // Search for the shape across all pages
                foreach (Page page in diagram.Pages)
                {
                    // Find the shape with the matching ID on the current page
                    Shape shape = page.Shapes.FirstOrDefault(s => s.ID == shapeId);
                    if (shape != null)
                    {
                        // Add the comment to the shape using the page's AddComment method
                        page.AddComment(shapeId, commentText);
                        break; // Shape found and comment added; move to next comment
                    }
                }
            }

            // Save the modified diagram (replace with the provided save rule if available)
            SaveDiagram(diagram, "output.vsdx");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Placeholder for the provided load rule
    static Diagram LoadDiagram(string path)
    {
        // The actual implementation should be supplied by the lifecycle rule.
        return new Diagram(path);
    }

    // Placeholder for the provided save rule
    static void SaveDiagram(Diagram diagram, string path)
    {
        // The actual implementation should be supplied by the lifecycle rule.
        diagram.Save(path, SaveFileFormat.Vsdx);
    }
}
