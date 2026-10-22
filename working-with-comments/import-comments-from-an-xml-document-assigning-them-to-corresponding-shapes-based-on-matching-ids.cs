using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: diagramPath, xmlPath, outputPath
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <xmlPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string xmlPath = args[1];
        string outputPath = args[2];

        // Guard for input files
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

        // Load XML document containing comments
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

        // Load Visio diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate over each <Comment> element in the XML
                foreach (XElement commentElement in xDoc.Root.Elements("Comment"))
                {
                    // Expected attributes: ShapeID (numeric) and Text (string)
                    XAttribute shapeIdAttr = commentElement.Attribute("ShapeID");
                    XAttribute textAttr = commentElement.Attribute("Text");

                    if (shapeIdAttr == null || textAttr == null)
                    {
                        Console.Error.WriteLine("Comment element missing ShapeID or Text attribute; skipping.");
                        continue;
                    }

                    if (!long.TryParse(shapeIdAttr.Value, out long shapeId))
                    {
                        Console.Error.WriteLine($"Invalid ShapeID value: {shapeIdAttr.Value}; skipping.");
                        continue;
                    }

                    string commentText = textAttr.Value;

                    // Search for the shape with the given ID across all pages
                    bool shapeFound = false;
                    foreach (Page page in diagram.Pages)
                    {
                        // Attempt to retrieve the shape by ID
                        Shape shape = page.Shapes.GetShape(shapeId);
                        if (shape != null)
                        {
                            shapeFound = true;

                            // Use shape's PinX and PinY as comment location
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;

                            // Add comment to the page at the shape's location
                            page.AddComment(pinX, pinY, commentText);

                            // Retrieve the newly added annotation (last in the collection)
                            int annCount = page.PageSheet.Annotations.Count;
                            if (annCount > 0)
                            {
                                Annotation ann = page.PageSheet.Annotations[annCount - 1];
                                // Link the annotation to the shape
                                ann.ShapeID = (int)shapeId;
                            }

                            // No need to continue searching other pages for this shape
                            break;
                        }
                    }

                    if (!shapeFound)
                    {
                        Console.Error.WriteLine($"Shape with ID {shapeId} not found in any page; comment skipped.");
                    }
                }

                // Save the modified diagram to the output path
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to {outputPath}");
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