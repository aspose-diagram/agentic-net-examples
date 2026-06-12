using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text of the shape
                        string shapeText = shape.Text.Value.ToString();

                        // Check if the shape contains a warning message (case‑insensitive)
                        if (!string.IsNullOrWhiteSpace(shapeText) &&
                            shapeText.IndexOf("warning", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Apply a fully transparent background
                            // Ensure a solid fill pattern is set (optional but safe)
                            shape.Fill.FillPattern.Value = 1;               // Solid fill
                            shape.Fill.FillBkgndTrans.Value = 100;          // 100 % transparent
                        }
                    }
                }

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