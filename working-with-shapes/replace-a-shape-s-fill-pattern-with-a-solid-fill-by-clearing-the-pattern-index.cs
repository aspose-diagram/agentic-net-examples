using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // Set the fill pattern to solid (pattern index = 1)
                shape.Fill.FillPattern.Value = 1;

                // Optionally, set a foreground color for the solid fill
                shape.Fill.FillForegnd.Value = "#FF0000"; // Red color

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Shape fill pattern updated and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }