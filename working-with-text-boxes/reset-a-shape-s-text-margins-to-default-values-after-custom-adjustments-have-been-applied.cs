using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (replace with actual path)
                string inputPath = "input.vsdx";

                // Output Visio file path
                string outputPath = "output_reset_margins.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a TextBlock (all shapes have it, but check for safety)
                        if (shape.TextBlock != null)
                        {
                            // Reset text margins to default (0 inches). 
                            // Using DoubleValue with MeasureConst.IN for inches.
                            shape.TextBlock.LeftMargin = new DoubleValue(0, MeasureConst.IN);
                            shape.TextBlock.RightMargin = new DoubleValue(0, MeasureConst.IN);
                            shape.TextBlock.TopMargin = new DoubleValue(0, MeasureConst.IN);
                            shape.TextBlock.BottomMargin = new DoubleValue(0, MeasureConst.IN);
                        }
                    }
                }

                // Save the modified diagram back to a file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up resources
                diagram.Dispose();

                Console.WriteLine("Text margins have been reset and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }