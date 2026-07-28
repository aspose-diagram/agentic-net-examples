using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only shapes that contain text
                        string plainText = shape.Text.Value.ToString();
                        if (string.IsNullOrWhiteSpace(plainText))
                            continue;

                        // Ensure the text block is not rotated
                        shape.TextXForm.TxtAngle.Value = 0; // radians

                        // Calculate the left edge X coordinate of the shape
                        double shapeLeftX = shape.XForm.PinX.Value - (shape.XForm.Width.Value / 2.0);

                        // Position the text block so its left edge aligns with the shape's left edge
                        shape.TextXForm.TxtPinX.Value = shapeLeftX;

                        // Set the local pin to the width of the text block to achieve left alignment
                        // (TxtLocPinX equals TxtWidth aligns the left side of the text block)
                        shape.TextXForm.TxtLocPinX.Value = shape.TextXForm.TxtWidth.Value;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }