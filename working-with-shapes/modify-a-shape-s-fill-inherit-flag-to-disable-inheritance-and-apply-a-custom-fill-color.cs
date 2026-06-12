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

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve a shape to modify.
                // Here we simply take the first shape on the page.
                // Adjust the selection logic as needed (e.g., by ID or NameU).
                Shape shape = page.Shapes[0];

                // Disable fill inheritance by explicitly setting fill properties.
                // Set a solid fill pattern (1 = solid)
                shape.Fill.FillPattern.Value = 1;

                // Apply a custom foreground fill color (hex string)
                shape.Fill.FillForegnd.Value = "#FF5733"; // Example: a reddish orange

                // Optionally set background fill color and transparency
                shape.Fill.FillBkgnd.Value = "#FFFFFF"; // White background
                shape.Fill.FillForegndTrans.Value = 0;   // No transparency for foreground
                shape.Fill.FillBkgndTrans.Value = 0;    // No transparency for background

                // Save the modified diagram to a new file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Shape fill updated and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }