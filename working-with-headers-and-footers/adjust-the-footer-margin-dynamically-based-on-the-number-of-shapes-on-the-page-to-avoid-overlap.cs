using System;
using Aspose.Diagram;

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

                // Determine the maximum number of non‑deleted shapes on any page
                int maxShapeCount = 0;
                foreach (Page page in diagram.Pages)
                {
                    int shapeCount = 0;
                    foreach (Shape shape in page.Shapes)
                    {
                        // Exclude shapes that are marked as deleted locally
                        if (shape.Del == BOOL.False)
                        {
                            shapeCount++;
                        }
                    }

                    if (shapeCount > maxShapeCount)
                    {
                        maxShapeCount = shapeCount;
                    }
                }

                // Calculate a dynamic footer margin:
                // Base margin of 0.5 inches plus 0.05 inches for each shape on the busiest page
                double baseMargin = 0.5;          // inches
                double extraPerShape = 0.05;      // inches per shape
                double calculatedMargin = baseMargin + (extraPerShape * maxShapeCount);

                // Apply the calculated margin to the document's footer
                diagram.HeaderFooter.FooterMargin.Value = calculatedMargin;

                // Optionally, set footer text with automatic page numbering
                diagram.HeaderFooter.FooterRight = "Page: &p";

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }