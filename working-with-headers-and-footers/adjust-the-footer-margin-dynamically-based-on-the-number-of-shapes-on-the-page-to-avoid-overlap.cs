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
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Determine the maximum number of shapes on any page
                    int maxShapeCount = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        int shapeCount = page.Shapes.Count;
                        if (shapeCount > maxShapeCount)
                            maxShapeCount = shapeCount;
                    }

                    // Calculate a dynamic footer margin (in inches)
                    // Base margin: 0.5 inches, plus 0.05 inches per shape on the busiest page
                    double baseMargin = 0.5;
                    double extraPerShape = 0.05;
                    double calculatedMargin = baseMargin + (maxShapeCount * extraPerShape);

                    // Apply the calculated margin to the document's footer
                    diagram.HeaderFooter.FooterMargin.Value = calculatedMargin;

                    // Save the updated diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Footer margin adjusted and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }