using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the placeholder background color (yellow) using RGB string format
                const string placeholderColor = "RGB(255,255,0)";

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the plain text of the shape
                        string shapeText = shape.Text.Value.ToString();

                        // Check if the shape's text block is empty or whitespace
                        if (string.IsNullOrWhiteSpace(shapeText))
                        {
                            // Assign the placeholder background color to the text block
                            shape.TextBlock.TextBkgnd.Ufe.F = placeholderColor;
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