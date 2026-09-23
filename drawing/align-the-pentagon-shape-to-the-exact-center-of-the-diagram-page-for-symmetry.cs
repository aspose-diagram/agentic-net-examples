using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Get page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate center coordinates
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a master and that the master name is "Pentagon"
                    if (shape.Master != null && shape.Master.Name == "Pentagon")
                    {
                        // Align the shape's pin (center) to the page center
                        shape.XForm.PinX.Value = centerX;
                        shape.XForm.PinY.Value = centerY;
                    }
                }

                // Save the modified diagram
                string outputPath = "output_centered.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }