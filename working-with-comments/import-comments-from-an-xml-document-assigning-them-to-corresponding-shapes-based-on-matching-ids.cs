using System.IO;
using System;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ImportComments
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (lifecycle rule: load)
            Diagram diagram = new Diagram("input.vsdx");

            // Load the XML file containing comments
            XDocument xmlDoc = XDocument.Load("comments.xml");

            // Assume comments are on the first page
            Page page = diagram.Pages[0];

            // Iterate through each comment element in the XML
            foreach (XElement commentElem in xmlDoc.Root.Elements("Comment"))
            {
                // Extract the shape ID and comment text
                long shapeId = (long)commentElem.Attribute("ShapeID");
                string commentText = commentElem.Value;

                // Add the comment to the shape (method overload: AddComment(long, string))
                page.AddComment(shapeId, commentText);
            }

            // Save the updated diagram (lifecycle rule: save)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
