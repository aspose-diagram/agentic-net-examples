using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Choose the page (first page in this example)
                Page page = diagram.Pages[0];

                // Retrieve the target shape by its ID (replace with actual ID)
                // For demonstration, we use the first shape on the page
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Desired orientation angle in degrees (e.g., 30 degrees)
                double angleDegrees = 30.0;
                double angleRadians = (Math.PI / 180.0) * angleDegrees;

                // Set the text rotation angle (radians)
                shape.TextXForm.TxtAngle.Value = angleRadians;

                // Position text at the bottom of the shape
                // TxtLocPinY should be set to the height of the text block
                // TxtPinY should be set to 0 so the bottom aligns with the shape's bottom edge
                shape.TextXForm.TxtLocPinY.Value = shape.TextXForm.TxtHeight.Value;
                shape.TextXForm.TxtPinY.Value = 0.0;

                // Adjust bottom margin (in inches, e.g., 0.1 inch)
                shape.TextBlock.BottomMargin.Value = 0.1;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Text repositioned and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }