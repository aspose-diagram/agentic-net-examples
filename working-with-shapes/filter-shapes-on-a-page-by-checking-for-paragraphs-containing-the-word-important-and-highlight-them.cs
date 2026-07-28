using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (replace with actual paths as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_highlighted.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve the plain text of the shape
                            string shapeText = shape.Text.Value.Text;

                            // Check if the text contains the word "Important"
                            if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains("Important"))
                            {
                                // Highlight the shape by setting a yellow fill color
                                shape.Fill.FillForegnd.Value = "#FFFF00";
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Processing completed. Highlighted diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }