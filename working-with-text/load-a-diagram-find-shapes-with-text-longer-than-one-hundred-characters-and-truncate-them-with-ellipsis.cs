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
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Get the plain text of the shape
                        string text = shape.Text.Value.Text;

                        // If the text exceeds 100 characters, truncate and add an ellipsis
                        if (!string.IsNullOrEmpty(text) && text.Length > 100)
                        {
                            string truncated = text.Substring(0, 100) + "…";

                            // Replace the shape's text with the truncated version
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt(truncated));
                        }
                    }
                }

                // Save the modified diagram (preserving the original format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }