using System;
using Aspose.Diagram;
using System.Xml.Linq;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioPath = "input.vsdx";
                // Output XML file path
                string xmlOutputPath = "comments.xml";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Create the root element for the XML document
                    XElement root = new XElement("Comments");

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the annotations (comments) collection for the current page
                        foreach (Annotation annotation in page.PageSheet.Annotations)
                        {
                            // Build an XML element representing the comment
                            XElement commentElement = new XElement("Comment",
                                new XAttribute("PageName", page.Name ?? string.Empty),
                                new XAttribute("MarkerIndex", annotation.MarkerIndex.Value),
                                new XAttribute("ShapeID", annotation.ShapeID),
                                new XAttribute("ReviewerID", annotation.ReviewerID.Value),
                                new XAttribute("CommentText", annotation.Comment.Value ?? string.Empty)
                            );

                            // Add the comment element to the root
                            root.Add(commentElement);
                        }
                    }

                    // Create the XDocument and save it to the specified file
                    XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                    doc.Save(xmlOutputPath);
                }

                Console.WriteLine("Comments exported successfully to " + xmlOutputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }