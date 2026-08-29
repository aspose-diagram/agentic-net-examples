using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments count.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputVisioPath>");
            return;
        }

        // Input Visio file path.
        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path.
        string outputPath = args[1];

        try
        {
            // Load the diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page (you can change the index as needed).
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape to clone.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes to clone.");
                return;
            }

            // Retrieve the first shape on the page as the source shape.
            Shape sourceShape = page.Shapes.GetShape(page.Shapes[0].ID);

            // Verify the source shape has an associated master (required for cloning).
            if (sourceShape.Master == null)
            {
                Console.Error.WriteLine("Source shape does not have a master; cannot clone.");
                return;
            }

            // Capture the master name to reuse for the duplicate shape.
            string masterName = sourceShape.Master.Name;

            // Capture the original position.
            double originalPinX = sourceShape.XForm.PinX.Value;
            double originalPinY = sourceShape.XForm.PinY.Value;

            // Define an offset to place the cloned shape (e.g., 2 inches to the right).
            double offsetX = 2.0;
            double offsetY = 0.0;

            // Add a new shape on the same page using the same master and offset position.
            long newShapeId = page.AddShape(originalPinX + offsetX, originalPinY + offsetY, masterName, false);

            // Retrieve the newly added shape instance.
            Shape clonedShape = page.Shapes.GetShape(newShapeId);

            // ---- Copy visual properties from the source shape to the cloned shape ----

            // Copy text runs.
            clonedShape.Text.Value.Clear(); // Remove any default placeholder text.
            foreach (var fmt in sourceShape.Text.Value)
            {
                if (fmt is Txt txt)
                {
                    // Preserve each text run's content.
                    clonedShape.Text.Value.Add(new Txt(txt.Text));
                }
            }

            // Copy fill foreground color (if set).
            clonedShape.Fill.FillForegnd.Value = sourceShape.Fill.FillForegnd.Value;

            // Copy line color and weight.
            clonedShape.Line.LineColor.Value = sourceShape.Line.LineColor.Value;
            clonedShape.Line.LineWeight.Value = sourceShape.Line.LineWeight.Value;

            // Copy shape width and height.
            clonedShape.XForm.Width.Value = sourceShape.XForm.Width.Value;
            clonedShape.XForm.Height.Value = sourceShape.XForm.Height.Value;

            // Optionally copy other properties (e.g., line pattern) as needed.
            clonedShape.Line.LinePattern.Value = sourceShape.Line.LinePattern.Value;

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Cloned shape saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}