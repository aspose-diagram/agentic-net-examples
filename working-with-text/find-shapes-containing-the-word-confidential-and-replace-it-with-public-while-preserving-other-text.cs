using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get the plain text of the shape
                        string currentText = shape.Text.Value.ToString();

                        // If the shape has no text, move on
                        if (string.IsNullOrEmpty(currentText))
                            continue;

                        // Replace the target word if it exists
                        if (currentText.Contains("Confidential"))
                        {
                            string newText = currentText.Replace("Confidential", "Public");

                            // Clear existing text runs and add the updated text
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt(newText));
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