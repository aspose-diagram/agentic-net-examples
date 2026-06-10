using System.IO;
using System;
using Aspose.Diagram;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourcePath = "input.vsdx";
            // Path where the exported XML will be saved
            string exportPath = "CommentsExport.xml";

            // Load the diagram
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Create the root element for the XML document
                XDocument xmlDoc = new XDocument(new XElement("Comments"));

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the annotations (comments) collection of the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Build an XML element for each comment
                        XElement commentElement = new XElement("Comment",
                            new XAttribute("PageName", page.Name),
                            new XAttribute("MarkerIndex", annotation.MarkerIndex.Value),
                            new XAttribute("ReviewerID", annotation.ReviewerID.Value),
                            new XAttribute("ShapeID", annotation.ShapeID),
                            new XElement("Text", annotation.Comment.Value)
                        );

                        // Add the comment element to the root
                        xmlDoc.Root.Add(commentElement);
                    }
                }

                // Save the XML document to the specified file
                xmlDoc.Save(exportPath);
            }

            Console.WriteLine("Comments exported successfully to " + exportPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
