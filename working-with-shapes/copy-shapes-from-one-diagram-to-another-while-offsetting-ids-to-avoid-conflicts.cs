using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram path, target diagram path, output diagram path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <source.vsdx> <target.vsdx> <output.vsdx>");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load source diagram
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            // Load target diagram (the diagram that will receive the copied shapes)
            using (Diagram targetDiagram = new Diagram(targetPath))
            {
                // Ensure both diagrams have at least one page
                if (sourceDiagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Source diagram contains no pages.");
                    return;
                }
                if (targetDiagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Target diagram contains no pages.");
                    return;
                }

                // Use the first page of each diagram for this example
                Page sourcePage = sourceDiagram.Pages[0];
                Page targetPage = targetDiagram.Pages[0];

                // Iterate through each shape on the source page
                foreach (Shape srcShape in sourcePage.Shapes)
                {
                    // Skip deleted shapes
                    if (srcShape.Del == BOOL.True)
                        continue;

                    // Determine master name; fallback to a basic shape if master is missing
                    string masterName = srcShape.Master != null ? srcShape.Master.Name : "Rectangle";

                    // Retrieve position and size from the source shape
                    double pinX = srcShape.XForm.PinX != null ? srcShape.XForm.PinX.Value : 0.0;
                    double pinY = srcShape.XForm.PinY != null ? srcShape.XForm.PinY.Value : 0.0;
                    double width = srcShape.XForm.Width != null ? srcShape.XForm.Width.Value : 1.0;
                    double height = srcShape.XForm.Height != null ? srcShape.XForm.Height.Value : 1.0;

                    // Add a new shape to the target diagram using the same master and position.
                    // The AddShape method returns the new shape's ID.
                    long newShapeId = targetDiagram.AddShape(pinX, pinY, masterName, 0);

                    // Retrieve the newly added shape object.
                    Shape newShape = targetPage.Shapes.GetShape(newShapeId);

                    // Apply size (Width/Height) to the new shape.
                    if (newShape.XForm.Width != null) newShape.XForm.Width.Value = width;
                    if (newShape.XForm.Height != null) newShape.XForm.Height.Value = height;

                    // Copy visual formatting (line, fill) from source to target shape.
                    if (srcShape.Line != null && newShape.Line != null)
                    {
                        if (srcShape.Line.LineColor != null && newShape.Line.LineColor != null)
                            newShape.Line.LineColor.Value = srcShape.Line.LineColor.Value;
                        if (srcShape.Line.LineWeight != null && newShape.Line.LineWeight != null)
                            newShape.Line.LineWeight.Value = srcShape.Line.LineWeight.Value;
                    }
                    if (srcShape.Fill != null && newShape.Fill != null)
                    {
                        if (srcShape.Fill.FillForegnd != null && newShape.Fill.FillForegnd != null)
                            newShape.Fill.FillForegnd.Value = srcShape.Fill.FillForegnd.Value;
                        if (srcShape.Fill.FillBkgnd != null && newShape.Fill.FillBkgnd != null)
                            newShape.Fill.FillBkgnd.Value = srcShape.Fill.FillBkgnd.Value;
                    }

                    // Copy shape text if any
                    if (srcShape.Text != null && srcShape.Text.Value != null && srcShape.Text.Value.Count > 0)
                    {
                        // Clear existing text in the new shape
                        newShape.Text.Value.Clear();
                        // Copy each text run
                        foreach (var txtItem in srcShape.Text.Value)
                        {
                            if (txtItem is Txt txt)
                            {
                                newShape.Text.Value.Add(new Txt(txt.Text));
                            }
                        }
                    }
                }

                // Save the combined diagram to the specified output path.
                targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Shapes copied successfully. Output saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}