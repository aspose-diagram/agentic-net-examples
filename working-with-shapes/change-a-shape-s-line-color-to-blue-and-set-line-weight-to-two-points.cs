using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through pages and shapes to find the first non-deleted shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Set line color to blue (hex format)
                        shape.Line.LineColor.Value = "#0000FF";

                        // Set line weight to 2 points.
                        // LineWeight is measured in inches; 1 point = 1/72 inch.
                        shape.Line.LineWeight.Value = 2.0 / 72.0;

                        // Assuming only one shape needs the change; exit loops after modification
                        goto SaveDiagram;
                    }
                }

                SaveDiagram:
                // Save the modified diagram (replace with desired output path)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }