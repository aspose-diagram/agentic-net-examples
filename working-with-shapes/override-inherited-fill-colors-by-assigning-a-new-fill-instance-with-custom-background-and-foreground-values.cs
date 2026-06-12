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

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page and override fill colors
                foreach (Shape shape in page.Shapes)
                {
                    // Set a solid fill pattern (optional, 1 = solid)
                    shape.Fill.FillPattern.Value = 1;

                    // Override foreground (fill) color
                    shape.Fill.FillForegnd.Value = "#FF0000"; // Red

                    // Override background color
                    shape.Fill.FillBkgnd.Value = "#00FF00"; // Green
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }