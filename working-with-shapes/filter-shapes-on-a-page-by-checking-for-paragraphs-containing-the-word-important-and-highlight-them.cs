using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_highlighted.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.False)
                            {
                                // Get the plain text of the shape
                                string plainText = shape.Text.Value.Text;

                                // Check if the text contains the word "Important"
                                if (!string.IsNullOrEmpty(plainText) && plainText.Contains("Important"))
                                {
                                    // Highlight the shape by setting a yellow fill color
                                    shape.Fill.FillForegnd.Value = "#FFFF00";

                                    // Optionally, set a red outline to make the highlight more visible
                                    shape.Line.LineColor.Value = "#FF0000";
                                }
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }