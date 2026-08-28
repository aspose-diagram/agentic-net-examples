using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect: input diagram path, source master name, target master name, output diagram path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <input.vsdx> <sourceMaster> <targetMaster> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string sourceMasterName = args[1];
        string targetMasterName = args[2];
        string outputPath = args[3];

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that use the source master
                var shapesToReplace = new System.Collections.Generic.List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    // Compare master name (case‑sensitive as per Visio naming)
                    if (shape.Master != null && shape.Master.Name == sourceMasterName)
                    {
                        shapesToReplace.Add(shape.ID);
                    }
                }

                // Process each shape that needs replacement
                foreach (long oldShapeId in shapesToReplace)
                {
                    // Retrieve the original shape
                    Shape oldShape = page.Shapes.GetShape(oldShapeId);

                    // Preserve geometric data
                    double pinX = oldShape.XForm.PinX.Value;
                    double pinY = oldShape.XForm.PinY.Value;
                    double width = oldShape.XForm.Width.Value;
                    double height = oldShape.XForm.Height.Value;

                    // Preserve plain text content
                    string plainText = oldShape.Text.Value.ToString();

                    // Add a new shape based on the target master at the same location/size
                    long newShapeId = diagram.AddShape(pinX, pinY, width, height, targetMasterName, page.ID);
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Transfer the text to the new shape
                    newShape.Text.Value.Clear();
                    newShape.Text.Value.Add(new Txt(plainText));

                    // Rewire all connections that referenced the old shape to point to the new shape
                    foreach (Connect conn in page.Connects)
                    {
                        if (conn.FromSheet == oldShapeId)
                            conn.FromSheet = newShapeId;
                        if (conn.ToSheet == oldShapeId)
                            conn.ToSheet = newShapeId;
                    }

                    // Mark the old shape as deleted (Visio uses the Del cell)
                    oldShape.Del = BOOL.True;
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Master replacement completed. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}