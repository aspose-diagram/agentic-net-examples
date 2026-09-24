using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Use the first page in the document
            Page page = diagram.Pages[0];

            // Find a source shape to clone (skip connectors and deleted shapes)
            Shape sourceShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Del == BOOL.False && !shp.OneD) // ensure it's a visible 2‑D shape
                {
                    sourceShape = shp;
                    break;
                }
            }

            if (sourceShape == null)
            {
                Console.Error.WriteLine("No suitable shape found to clone.");
                return;
            }

            // Retrieve the master name of the source shape (used for cloning)
            string masterName = sourceShape.Master?.Name ?? "Rectangle";

            // Determine a position for the cloned shape (offset by 2 inches)
            double newPinX = sourceShape.XForm.PinX.Value + 2.0;
            double newPinY = sourceShape.XForm.PinY.Value + 2.0;

            // Add a new shape using the same master; isCalculate = false
            long cloneId = page.AddShape(newPinX, newPinY, masterName, false);

            // Retrieve the cloned shape instance
            Shape cloneShape = page.Shapes.GetShape(cloneId);

            // -------------------- Inherit Fill Settings --------------------
            // Copy fill pattern and colors from the source shape to the clone
            cloneShape.Fill.FillPattern.Value = sourceShape.Fill.FillPattern.Value;
            cloneShape.Fill.FillForegnd.Value = sourceShape.Fill.FillForegnd.Value;
            cloneShape.Fill.FillBkgnd.Value = sourceShape.Fill.FillBkgnd.Value;
            cloneShape.Fill.FillForegndTrans.Value = sourceShape.Fill.FillForegndTrans.Value;
            cloneShape.Fill.FillBkgndTrans.Value = sourceShape.Fill.FillBkgndTrans.Value;

            // -------------------- Modify Line Inheritance Flag --------------------
            // Change the line color of the clone to demonstrate a different line setting
            // (this effectively breaks line inheritance from the original)
            cloneShape.Line.LineColor.Value = "#FF0000"; // red line
            cloneShape.Line.LineWeight.Value = 0.03;     // thicker line

            // Save the modified diagram to the output file using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}