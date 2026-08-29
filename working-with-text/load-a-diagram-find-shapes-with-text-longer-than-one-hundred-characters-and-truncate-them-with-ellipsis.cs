using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths.
                // You can replace these with your own paths or pass them via command‑line arguments.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the plain text of the shape.
                        string fullText = shape.Text.Value.Text;

                        // If the text exceeds 100 characters, truncate it and add an ellipsis.
                        if (!string.IsNullOrEmpty(fullText) && fullText.Length > 100)
                        {
                            string truncated = fullText.Substring(0, 100) + "…";

                            // Replace the shape's text with the truncated version.
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt(truncated));
                        }
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }