using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Find the first non-deleted shape to serve as the source for cloning
                Shape originalShape = null;
                foreach (Shape s in page.Shapes)
                {
                    if (s.Del == BOOL.False)
                    {
                        originalShape = s;
                        break;
                    }
                }

                // Guard: ensure a source shape was found
                if (originalShape == null)
                {
                    Console.Error.WriteLine("No suitable shape found to clone.");
                    return;
                }

                // Determine a position for the cloned shape (offset by 2 inches on X)
                double newPinX = originalShape.XForm.PinX.Value + 2.0;
                double newPinY = originalShape.XForm.PinY.Value;

                // Retrieve the master name of the original shape
                string masterName = originalShape.Master?.Name;
                if (string.IsNullOrEmpty(masterName))
                {
                    Console.Error.WriteLine("Original shape does not have an associated master.");
                    return;
                }

                // Add a new shape on the same page using the same master
                long clonedId = page.AddShape(newPinX, newPinY, masterName, false);

                // Retrieve the newly added shape instance
                Shape clonedShape = page.Shapes.GetShape((int)clonedId);

                // Copy fill settings from the original shape to the clone
                clonedShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;
                clonedShape.Fill.FillBkgnd.Value = originalShape.Fill.FillBkgnd.Value;
                clonedShape.Fill.FillPattern.Value = originalShape.Fill.FillPattern.Value;
                clonedShape.Fill.FillForegndTrans.Value = originalShape.Fill.FillForegndTrans.Value;
                clonedShape.Fill.FillBkgndTrans.Value = originalShape.Fill.FillBkgndTrans.Value;
                // Updated property names: Shadow* -> Shdw*
                clonedShape.Fill.ShdwForegnd.Value = originalShape.Fill.ShdwForegnd.Value; // corrected
                clonedShape.Fill.ShdwBkgnd.Value = originalShape.Fill.ShdwBkgnd.Value;   // corrected
                clonedShape.Fill.ShdwPattern.Value = originalShape.Fill.ShdwPattern.Value; // corrected
                clonedShape.Fill.ShapeShdwType.Value = originalShape.Fill.ShapeShdwType.Value;
                clonedShape.Fill.ShapeShdwObliqueAngle.Value = originalShape.Fill.ShapeShdwObliqueAngle.Value;
                clonedShape.Fill.ShapeShdwOffsetX.Value = originalShape.Fill.ShapeShdwOffsetX.Value;
                clonedShape.Fill.ShapeShdwOffsetY.Value = originalShape.Fill.ShapeShdwOffsetY.Value;
                clonedShape.Fill.ShapeShdwScaleFactor.Value = originalShape.Fill.ShapeShdwScaleFactor.Value;

                // Modify the clone's line inheritance flag (enable inheritance)
                clonedShape.Line.LineColor.Ufe.F = "INHERITED()";

                // Save the modified diagram to the output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
