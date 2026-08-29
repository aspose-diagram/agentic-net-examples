using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (modify as needed or pass via command line)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text of the shape
                        string plainText = shape.Text.Value.ToString();

                        // Check if the shape contains multiline text (contains line break)
                        if (!string.IsNullOrEmpty(plainText) && (plainText.Contains("\n") || plainText.Contains("\r")))
                        {
                            // Set a different default tab stop (e.g., 0.5 inches) for better readability
                            shape.TextBlock.DefaultTabStop.Value = 0.5;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }