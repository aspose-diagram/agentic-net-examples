using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input Visio file path (first argument) and optional output path (second argument)
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DiagramBatchUpdate <inputFilePath> [outputFilePath]");
                return;
            }

            string inputPath = args[0];
            string outputPath = args.Length > 1 ? args[1] : System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(inputPath) ?? "",
                System.IO.Path.GetFileNameWithoutExtension(inputPath) + "_updated.vsdx");

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the current width of the shape (in inches)
                        double width = shape.XForm.Width.Value;

                        // Calculate a new line weight based on the width.
                        // Example: 1% of the width (adjust factor as needed)
                        double newLineWeight = width * 0.01;

                        // Ensure the line weight is a positive value
                        if (newLineWeight < 0.001)
                            newLineWeight = 0.001; // minimum visible weight

                        // Update the shape's line weight (in inches)
                        shape.Line.LineWeight.Value = newLineWeight;
                    }
                }

                // Save the updated diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }