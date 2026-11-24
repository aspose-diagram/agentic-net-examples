using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <commentsXmlPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string xmlPath = args[1];
        string outputPath = args[2];

        // Guard against missing input files.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML document containing comments.
        XDocument xDoc;
        try
        {
            xDoc = XDocument.Load(xmlPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load XML file: {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over each <Comment> element in the XML.
                foreach (XElement commentElem in xDoc.Root.Elements("Comment"))
                {
                    // Extract required fields from XML.
                    XElement shapeIdElem = commentElem.Element("ShapeID");
                    XElement textElem = commentElem.Element("Text");
                    if (shapeIdElem == null || textElem == null)
                    {
                        Console.Error.WriteLine("Comment element missing ShapeID or Text.");
                        continue;
                    }

                    long shapeId;
                    if (!long.TryParse(shapeIdElem.Value, out shapeId))
                    {
                        Console.Error.WriteLine($"Invalid ShapeID value: {shapeIdElem.Value}");
                        continue;
                    }

                    string commentText = textElem.Value ?? string.Empty;

                    bool commentAdded = false;

                    // Search all pages for the shape with the matching ID.
                    foreach (Page page in diagram.Pages)
                    {
                        // Attempt to retrieve the shape by ID.
                        Shape shape = null;
                        try
                        {
                            shape = page.Shapes.GetShape(shapeId);
                        }
                        catch
                        {
                            // GetShape throws if the ID does not exist on this page; ignore and continue.
                        }

                        if (shape != null)
                        {
                            // Add the comment to the page, associating it with the found shape.
                            try
                            {
                                page.AddComment(shape, commentText);
                                commentAdded = true;
                                Console.WriteLine($"Added comment to shape ID {shapeId} on page {page.Name ?? "Unnamed"}.");
                            }
                            catch (Exception ex)
                            {
                                Console.Error.WriteLine($"Failed to add comment to shape ID {shapeId}: {ex.Message}");
                            }
                            break; // Shape IDs are unique; stop searching pages.
                        }
                    }

                    if (!commentAdded)
                    {
                        Console.Error.WriteLine($"Shape with ID {shapeId} not found in any page.");
                    }
                }

                // Save the modified diagram to the specified output path.
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to {outputPath}.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}