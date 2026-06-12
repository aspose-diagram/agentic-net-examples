using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
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
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the plain text of the shape (concatenated Txt runs)
                        string plainText = shape.Text?.Value?.Text ?? string.Empty;

                        // Check if the shape contains multiline text (contains line break characters)
                        if (plainText.Contains("\n") || plainText.Contains("\r"))
                        {
                            // Ensure the TextBlock object exists before modifying it
                            if (shape.TextBlock != null)
                            {
                                // Set a larger default tab stop (in inches) for better readability
                                // Example: 0.5 inches between tab stops
                                shape.TextBlock.DefaultTabStop.Value = 0.5;
                            }
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