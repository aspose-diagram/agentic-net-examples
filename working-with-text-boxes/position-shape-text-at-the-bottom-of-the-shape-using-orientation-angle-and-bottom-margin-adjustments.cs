using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (or specify a page index/name)
                Page page = diagram.Pages[0];

                // Retrieve a shape to modify (here we use the first shape on the page)
                // Ensure the shape exists
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the page.");
                    return;
                }

                // Get the shape by its ID (cast to int as required)
                Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                // Clear existing text and add new text
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Sample Text"));

                // ----- Position text at the bottom of the shape -----
                // 1. Set the text block bottom margin (in inches)
                double bottomMarginInches = 0.1; // adjust as needed
                shape.TextBlock.BottomMargin.Value = bottomMarginInches;

                // 2. Align text to the bottom using TextXForm
                //    TxtLocPinY is set to the height of the text block,
                //    TxtPinY is set to 0 (bottom of the shape)
                shape.TextXForm.TxtLocPinY.Value = shape.TextXForm.TxtHeight.Value;
                shape.TextXForm.TxtPinY.Value = 0;

                // 3. Set text orientation angle (degrees -> radians)
                double angleDegrees = 0; // change to desired rotation
                double angleRadians = (Math.PI / 180) * angleDegrees;
                shape.TextXForm.TxtAngle.Value = angleRadians;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Diagram saved with text positioned at the bottom.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }