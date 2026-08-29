using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (must exist)
                string inputPath = "sample.vsdx";

                // Path for the modified output file
                string outputPath = "sample_modified.vsdx";

                // Load the diagram from the source file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Access the first page (index 0)
                    Page page = diagram.Pages[0];

                    // Add a simple rectangle shape to the page
                    // Parameters: pinX, pinY, width, height (all in inches)
                    long shapeId = page.DrawRectangle(2.0, 2.0, 2.0, 1.0);

                    // Retrieve the newly created shape to set some properties
                    Shape shape = page.Shapes.GetShape(shapeId);
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Added Shape"));
                    shape.Fill.FillForegnd.Value = "#FFCC00"; // Fill color
                    shape.Line.LineColor.Value = "#000000";   // Border color

                    // Save the modified diagram to a new file in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                // Verify the file size after saving
                FileInfo fileInfo = new FileInfo(outputPath);
                Console.WriteLine($"Saved file size: {fileInfo.Length} bytes");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }