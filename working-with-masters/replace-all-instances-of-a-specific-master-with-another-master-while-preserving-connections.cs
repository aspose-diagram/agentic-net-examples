using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Names of the masters to replace
                string oldMasterName = "OldMaster";
                string newMasterName = "NewMaster";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect IDs of shapes that use the old master
                    List<long> shapesToReplace = new List<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master != null && shape.Master.Name == oldMasterName)
                        {
                            shapesToReplace.Add(shape.ID);
                        }
                    }

                    // Replace each identified shape with a new shape based on the new master
                    foreach (long oldShapeId in shapesToReplace)
                    {
                        Shape oldShape = page.Shapes.GetShape(oldShapeId);

                        // Preserve geometry and text
                        double pinX = oldShape.XForm.PinX.Value;
                        double pinY = oldShape.XForm.PinY.Value;
                        double width = oldShape.XForm.Width.Value;
                        double height = oldShape.XForm.Height.Value;
                        string text = oldShape.Text.Value.ToString();

                        // Add a new shape using the new master at the same location
                        long newShapeId = page.AddShape(pinX, pinY, newMasterName);
                        Shape newShape = page.Shapes.GetShape(newShapeId);

                        // Apply original size and text to the new shape
                        newShape.XForm.Width.Value = width;
                        newShape.XForm.Height.Value = height;
                        newShape.Text.Value.Clear();
                        newShape.Text.Value.Add(new Txt(text));

                        // Rewire connections from the old shape to the new shape
                        foreach (Connect conn in page.Connects)
                        {
                            if (conn.FromSheet == oldShapeId)
                            {
                                conn.FromSheet = newShapeId;
                            }
                            if (conn.ToSheet == oldShapeId)
                            {
                                conn.ToSheet = newShapeId;
                            }
                        }

                        // Mark the old shape as deleted
                        oldShape.Del = BOOL.True;
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