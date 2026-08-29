using System;
using System.IO;
using Aspose.Diagram;

// Console application that extracts plain text from each page of a Visio diagram,
    // concatenates the text, and writes a summary report to a file.
    class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output report file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramTextExtractor <inputVisioPath> <outputReportPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Validate input file existence.
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // StringBuilder for efficient concatenation.
                System.Text.StringBuilder reportBuilder = new System.Text.StringBuilder();

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the plain text of the shape.
                        // shape.Text.Value.Text returns the concatenated text of all Txt runs.
                        string shapeText = shape.Text.Value.Text;

                        // Skip empty or whitespace-only text.
                        if (string.IsNullOrWhiteSpace(shapeText))
                            continue;

                        // Optional: clean up line breaks and commas.
                        shapeText = shapeText.Replace("\r\n", " ").Replace("\n", " ").Replace(",", " ");

                        // Append the cleaned text to the report.
                        reportBuilder.AppendLine(shapeText);
                    }
                }

                // Write the concatenated text to the output report file.
                File.WriteAllText(outputPath, reportBuilder.ToString());

                Console.WriteLine($"Summary report generated successfully at: {outputPath}");
            }
            catch (Exception ex)
            {
                // Output any errors that occur during processing.
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }