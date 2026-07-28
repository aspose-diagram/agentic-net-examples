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

                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram contains no pages.");

                Page page = diagram.Pages[0];
                if (page.Shapes.Count == 0)
                    throw new Exception("The first page contains no shapes.");

                // Retrieve the first shape on the page
                Shape shape = page.Shapes.GetShape(0);

                // Apply a solid background color to the shape's text block.
                // The TextBkgnd cell defines the background color; using an RGB formula.
                shape.TextBlock.TextBkgnd.Ufe.F = "RGB(255,0,0)"; // Red background

                // Ensure the background is fully opaque (0% transparency)
                shape.TextBlock.TextBkgndTrans.Value = 0;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Text background color applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }