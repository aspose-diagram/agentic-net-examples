using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Process only background pages
                    if (page.Background == BOOL.True)
                    {
                        // Collect IDs of image (foreign) shapes on the background page
                        List<long> imageShapeIds = new List<long>();
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.Type == TypeValue.Foreign)
                            {
                                imageShapeIds.Add(shape.ID);
                            }
                        }

                        // Replace each background image with a solid color fill
                        foreach (long shapeId in imageShapeIds)
                        {
                            Shape shape = page.Shapes.GetShape(shapeId);

                            // Set solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // Solid fill

                            // Choose a solid color (light gray in this example)
                            shape.Fill.FillForegnd.Value = "#CCCCCC";

                            // Remove any outline by setting line pattern to none
                            shape.Line.LinePattern.Value = 0;
                        }
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