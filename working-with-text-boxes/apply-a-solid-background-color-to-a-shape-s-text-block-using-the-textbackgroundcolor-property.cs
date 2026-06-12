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

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page
                // Ensure there is at least one shape
                if (page.Shapes.Count == 0)
                {
                    throw new Exception("No shapes found on the first page.");
                }

                // Get the shape by its ID
                long shapeId = page.Shapes[0].ID;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Apply a solid background color to the shape's text block
                // Using RGB string format as required by the API
                shape.TextBlock.TextBkgnd.Ufe.F = "RGB(255,0,0)"; // Red background

                // Optionally set background transparency (0 = opaque)
                shape.TextBlock.TextBkgndTrans.Value = 0;

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