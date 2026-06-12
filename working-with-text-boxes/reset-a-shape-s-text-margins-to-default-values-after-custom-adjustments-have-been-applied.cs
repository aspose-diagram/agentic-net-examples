using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape has a TextBlock (all shapes have it, but check for safety)
                            if (shape.TextBlock != null)
                            {
                                // Reset text margins to default (0 inches)
                                shape.TextBlock.LeftMargin.Value = 0;
                                shape.TextBlock.RightMargin.Value = 0;
                                shape.TextBlock.TopMargin.Value = 0;
                                shape.TextBlock.BottomMargin.Value = 0;
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Text margins have been reset and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }