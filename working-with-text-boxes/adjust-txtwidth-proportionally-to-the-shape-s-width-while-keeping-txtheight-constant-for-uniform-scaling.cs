using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a TextXForm (text block) and XForm (geometry)
                        if (shape.TextXForm != null && shape.XForm != null)
                        {
                            // Preserve the current TxtHeight (do not modify)
                            double currentTxtHeight = shape.TextXForm.TxtHeight.Value;

                            // Set TxtWidth to match the shape's width for proportional scaling
                            shape.TextXForm.TxtWidth.Value = shape.XForm.Width.Value;

                            // Re-assign the preserved TxtHeight (optional, demonstrates intent)
                            shape.TextXForm.TxtHeight.Value = currentTxtHeight;
                        }
                    }
                }

                // Save the modified diagram (replace with your desired output path)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }