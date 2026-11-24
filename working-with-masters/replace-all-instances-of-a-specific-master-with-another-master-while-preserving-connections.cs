using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram, output diagram, source master name, target master name
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <output.vsdx> <sourceMasterName> <targetMasterName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string sourceMasterName = args[2];
        string targetMasterName = args[3];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Verify that both source and target masters exist in the diagram
            if (!diagram.Masters.IsExist(sourceMasterName))
            {
                Console.Error.WriteLine($"Source master \"{sourceMasterName}\" does not exist in the diagram.");
                return;
            }

            if (!diagram.Masters.IsExist(targetMasterName))
            {
                Console.Error.WriteLine($"Target master \"{targetMasterName}\" does not exist in the diagram.");
                return;
            }

            // Iterate through each page and replace shapes that use the source master
            foreach (Page page in diagram.Pages)
            {
                // Map old shape IDs to newly created shape IDs for connection updates
                Dictionary<long, long> shapeIdMap = new Dictionary<long, long>();

                // Collect shapes to replace to avoid modifying the collection while iterating
                List<Shape> shapesToReplace = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has an associated master and matches the source master name
                    if (shape.Master != null && shape.Master.Name == sourceMasterName)
                    {
                        shapesToReplace.Add(shape);
                    }
                }

                // Replace each identified shape
                foreach (Shape oldShape in shapesToReplace)
                {
                    // Preserve geometry of the old shape
                    double pinX = oldShape.XForm.PinX.Value;
                    double pinY = oldShape.XForm.PinY.Value;
                    double width = oldShape.XForm.Width.Value;
                    double height = oldShape.XForm.Height.Value;

                    // Add a new shape using the target master at the same position and size
                    long newShapeIdLong = page.AddShape(pinX, pinY, width, height, targetMasterName);
                    // GetShape expects an int ID, so cast safely
                    int newShapeId = (int)newShapeIdLong;
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Copy text content from the old shape to the new shape
                    if (oldShape.Text != null && oldShape.Text.Value != null)
                    {
                        // Clear existing text runs
                        newShape.Text.Value.Clear();
                        // Add each text run from the old shape
                        foreach (var txt in oldShape.Text.Value)
                        {
                            if (txt is Txt t)
                            {
                                newShape.Text.Value.Add(new Txt(t.Text));
                            }
                        }
                    }

                    // Record the ID mapping for later connection updates
                    shapeIdMap[oldShape.ID] = newShapeIdLong;

                    // Remove the old shape from the page
                    // Fixed: Remove expects a Shape object, not an ID
                    page.Shapes.Remove(oldShape);
                }

                // Update connections to point to the new shape IDs
                foreach (Connect conn in page.Connects)
                {
                    if (shapeIdMap.TryGetValue(conn.FromSheet, out long newFrom))
                    {
                        conn.FromSheet = newFrom;
                    }

                    if (shapeIdMap.TryGetValue(conn.ToSheet, out long newTo))
                    {
                        conn.ToSheet = newTo;
                    }
                }
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
