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
                // Path for the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text of the shape
                        string shapeText = shape.Text.Value.ToString();

                        // Check if the text block is empty or whitespace
                        if (string.IsNullOrWhiteSpace(shapeText))
                        {
                            // Assign a light gray placeholder background color to the text block
                            // Using RGB() string format as required by the API
                            shape.TextBlock.TextBkgnd.Ufe.F = "RGB(200,200,200)";
                            // Ensure the background is fully opaque (0% transparency)
                            shape.TextBlock.TextBkgndTrans.Value = 0;
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