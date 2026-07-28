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

                // Access the first page
                Page page = diagram.Pages[0];

                // Find the first shape that is not marked as deleted
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception("No non-deleted shape found on the first page.");
                }

                // Disable fill inheritance by explicitly setting fill properties
                // Set solid fill pattern (1 = solid)
                targetShape.Fill.FillPattern.Value = 1;
                // Apply a custom fill color (hex string)
                targetShape.Fill.FillForegnd.Value = "#FF5733";

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }