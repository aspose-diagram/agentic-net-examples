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
                string outputPath = "output_truncated.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the plain text of the shape
                        string fullText = shape.Text.Value.Text;

                        // Check if the text exceeds 100 characters
                        if (!string.IsNullOrEmpty(fullText) && fullText.Length > 100)
                        {
                            // Truncate to 100 characters and append ellipsis
                            string truncated = fullText.Substring(0, 100) + "...";

                            // Replace the shape's text with the truncated version
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt(truncated));
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