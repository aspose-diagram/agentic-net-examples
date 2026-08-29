using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the plain text of the shape
                        string shapeText = shape.Text.Value.ToString();

                        // If the text block is empty or whitespace, apply a placeholder background color
                        if (string.IsNullOrWhiteSpace(shapeText))
                        {
                            // Set a solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // 1 = solid

                            // Assign a light yellow background color as a visual cue
                            shape.Fill.FillBkgnd.Value = "#FFFF99";
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }