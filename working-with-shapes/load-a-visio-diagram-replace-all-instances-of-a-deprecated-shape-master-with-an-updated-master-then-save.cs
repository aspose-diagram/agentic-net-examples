using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <deprecatedMasterName> <updatedMasterName> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string deprecatedMasterName = args[1];
        string updatedMasterName = args[2];
        string outputPath = args[3];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that both masters exist in the diagram.
                Master deprecatedMaster = null;
                Master updatedMaster = null;

                // Attempt to retrieve masters by name.
                foreach (Master m in diagram.Masters)
                {
                    if (m.Name == deprecatedMasterName)
                        deprecatedMaster = m;
                    if (m.Name == updatedMasterName)
                        updatedMaster = m;
                }

                if (deprecatedMaster == null)
                {
                    Console.Error.WriteLine($"Deprecated master \"{deprecatedMasterName}\" not found in the diagram.");
                    return;
                }

                if (updatedMaster == null)
                {
                    Console.Error.WriteLine($"Updated master \"{updatedMasterName}\" not found in the diagram.");
                    return;
                }

                // Iterate through all pages and replace shapes that use the deprecated master.
                foreach (Page page in diagram.Pages)
                {
                    // Collect shape IDs to replace to avoid modifying the collection while iterating.
                    var shapesToReplace = new System.Collections.Generic.List<long>();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has an associated master.
                        if (shape.Master != null && shape.Master.Name == deprecatedMasterName)
                        {
                            shapesToReplace.Add(shape.ID);
                        }
                    }

                    // Replace each identified shape.
                    foreach (long shapeId in shapesToReplace)
                    {
                        Shape oldShape = page.Shapes.GetShape(shapeId);

                        // Preserve geometry.
                        double pinX = oldShape.XForm.PinX.Value;
                        double pinY = oldShape.XForm.PinY.Value;
                        double width = oldShape.XForm.Width.Value;
                        double height = oldShape.XForm.Height.Value;

                        // Mark the old shape for deletion.
                        oldShape.Del = BOOL.True;

                        // Add a new shape using the updated master.
                        long newShapeId = page.AddShape(pinX, pinY, updatedMasterName, false);
                        Shape newShape = page.Shapes.GetShape(newShapeId);

                        // Apply the original size to the new shape.
                        newShape.XForm.Width.Value = width;
                        newShape.XForm.Height.Value = height;

                        // Optionally, copy text from the old shape to the new one.
                        if (oldShape.Text != null && oldShape.Text.Value != null && oldShape.Text.Value.Count > 0)
                        {
                            // Clear existing text.
                            newShape.Text.Value.Clear();

                            // Copy each text segment.
                            foreach (var txt in oldShape.Text.Value)
                            {
                                // The collection holds Txt objects; add them directly.
                                newShape.Text.Value.Add(txt);
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}