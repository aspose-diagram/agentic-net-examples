using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – adjust as needed
                string diagramPath = "input.vsdx";
                string xmlCommentsPath = "comments.xml";
                string outputDiagramPath = "output_with_comments.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Load the XML containing comments
                XDocument xDoc = XDocument.Load(xmlCommentsPath);

                // Build a lookup of shape ID to list of comment texts
                var commentsByShapeId = new Dictionary<long, List<string>>();

                foreach (XElement commentElem in xDoc.Root.Elements("Comment"))
                {
                    // Expect an attribute named "ShapeId"
                    XAttribute idAttr = commentElem.Attribute("ShapeId");
                    if (idAttr == null) continue;

                    if (!long.TryParse(idAttr.Value, out long shapeId)) continue;

                    string commentText = commentElem.Value ?? string.Empty;

                    if (!commentsByShapeId.TryGetValue(shapeId, out List<string> list))
                    {
                        list = new List<string>();
                        commentsByShapeId[shapeId] = list;
                    }
                    list.Add(commentText);
                }

                // Iterate through all pages and shapes, attaching comments where applicable
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True) continue;

                        if (commentsByShapeId.TryGetValue(shape.ID, out List<string> shapeComments))
                        {
                            foreach (string txt in shapeComments)
                            {
                                // Add a shape‑level comment
                                page.AddComment(shape, txt);
                            }
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }