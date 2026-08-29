using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram from file
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Find the first non-deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False) // ensure the shape is not deleted
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No suitable shape found on the page.");
                    return;
                }

                // Apply a 90‑degree rotation to the shape's text.
                // TxtAngle is expressed in radians.
                double angleRadians = Math.PI / 2; // 90 degrees
                targetShape.TextXForm.TxtAngle.Value = angleRadians;

                // Reposition the text to the left side of the shape.
                // Left side: set local pin X to the text block width and pin X to 0.
                targetShape.TextXForm.TxtLocPinX.Value = targetShape.TextXForm.TxtWidth.Value;
                targetShape.TextXForm.TxtPinX.Value = 0;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }