using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    targetShape = shape;
                    break; // only need the first shape
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No shapes found on the first page.");
                    return;
                }

                // Set fill pattern to solid (value 1) and background color to green (#00FF00)
                targetShape.Fill.FillPattern.Value = 1;          // Solid fill
                targetShape.Fill.FillForegnd.Value = "#00FF00"; // Green foreground color

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Shape fill updated and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }