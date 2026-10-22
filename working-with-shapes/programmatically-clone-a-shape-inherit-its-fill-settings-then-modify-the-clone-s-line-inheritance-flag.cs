using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape originalShape = null;
                Page sourcePage = null;

                // Locate the first non‑deleted shape in the document.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Del == BOOL.False)
                        {
                            originalShape = shape;
                            sourcePage = page;
                            break;
                        }
                    }
                    if (originalShape != null)
                        break;
                }

                if (originalShape == null || sourcePage == null)
                {
                    Console.Error.WriteLine("No suitable shape found to clone.");
                    return;
                }

                // Ensure the original shape has an associated master (required for adding a similar shape).
                if (originalShape.Master == null)
                {
                    Console.Error.WriteLine("The original shape does not have a master; cannot clone.");
                    return;
                }

                // Determine a position for the cloned shape (offset slightly to avoid overlap).
                double newPinX = originalShape.XForm.PinX.Value + 1.0;
                double newPinY = originalShape.XForm.PinY.Value;

                // Add a new shape on the same page using the same master as the original.
                // AddShape returns the new shape's ID (long).
                long clonedShapeId = sourcePage.AddShape(newPinX, newPinY, originalShape.Master.Name, false);

                // Retrieve the Shape instance for the newly added shape.
                Shape clonedShape = sourcePage.Shapes.GetShape(clonedShapeId);

                // Inherit fill settings from the original shape by enabling fill inheritance.
                // The Ufe.F formula "INHERITED()" tells Visio to use inherited fill values.
                clonedShape.Fill.FillForegnd.Ufe.F = "INHERITED()";

                // Modify the clone's line inheritance flag.
                // Assigning the current line color value to itself disables line inheritance.
                clonedShape.Line.LineColor.Value = clonedShape.Line.LineColor.Value;

                // Save the modified diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}