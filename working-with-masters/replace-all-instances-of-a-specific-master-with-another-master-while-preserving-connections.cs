using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Name of the master to be replaced
                string oldMasterName = "OldMaster";
                // Name of the master that will replace the old one
                string newMasterName = "NewMaster";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure the new master exists in the diagram.
                // If it is not present, you can import it from a stencil file:
                // diagram.AddMaster("stencil.vssx", newMasterName);
                if (!diagram.Masters.IsExist(newMasterName))
                {
                    throw new Exception($"The master '{newMasterName}' does not exist in the diagram.");
                }

                // Mapping from old shape IDs to newly created shape IDs
                var idMap = new System.Collections.Generic.Dictionary<long, long>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Collect shapes that use the old master
                    var shapesToReplace = new System.Collections.Generic.List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master != null && shape.Master.Name == oldMasterName)
                        {
                            shapesToReplace.Add(shape);
                        }
                    }

                    // Replace each shape
                    foreach (Shape oldShape in shapesToReplace)
                    {
                        // Preserve geometry
                        double pinX = oldShape.XForm.PinX.Value;
                        double pinY = oldShape.XForm.PinY.Value;
                        double width = oldShape.XForm.Width.Value;
                        double height = oldShape.XForm.Height.Value;

                        // Preserve text
                        string text = oldShape.Text.Value.ToString();

                        // Add a new shape with the new master at the same location and size
                        long newShapeId = diagram.AddShape(pinX, pinY, width, height, newMasterName, page.ID);
                        Shape newShape = page.Shapes.GetShape(newShapeId);

                        // Set the text of the new shape
                        newShape.Text.Value.Clear();
                        newShape.Text.Value.Add(new Txt(text));

                        // Record the ID mapping
                        idMap[oldShape.ID] = newShapeId;

                        // Mark the old shape as deleted
                        oldShape.Del = BOOL.True;
                    }

                    // Update connections on the current page using the ID map
                    foreach (Connect conn in page.Connects)
                    {
                        if (idMap.ContainsKey(conn.FromSheet))
                        {
                            conn.FromSheet = idMap[conn.FromSheet];
                        }
                        if (idMap.ContainsKey(conn.ToSheet))
                        {
                            conn.ToSheet = idMap[conn.ToSheet];
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }