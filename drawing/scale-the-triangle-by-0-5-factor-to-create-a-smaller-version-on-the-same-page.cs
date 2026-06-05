using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Iterate through shapes to find the triangle
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a name and is a regular shape
                    if (shape.NameU != null && shape.Type == TypeValue.Shape && shape.NameU.Contains("Triangle"))
                    {
                        // Scale width and height by 0.5 (half size)
                        double originalWidth = shape.XForm.Width.Value;
                        double originalHeight = shape.XForm.Height.Value;

                        shape.XForm.Width.Value = originalWidth * 0.5;
                        shape.XForm.Height.Value = originalHeight * 0.5;

                        // Optionally, you can use the helper methods
                        // shape.SetWidth(originalWidth * 0.5);
                        // shape.SetHeight(originalHeight * 0.5);
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Triangle scaled and diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }