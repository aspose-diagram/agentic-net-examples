using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input file path, page index (0‑based), output file path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramCleanup <inputPath> <pageIndex> <outputPath>");
                return;
            }

            string inputPath = args[0];
            if (!int.TryParse(args[1], out int pageIndex))
            {
                Console.WriteLine("Invalid page index.");
                return;
            }
            string outputPath = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Validate page index
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.WriteLine("Page index out of range.");
                return;
            }

            // Get the specified page
            Page page = diagram.Pages[pageIndex];

            // Iterate over all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve the plain text of the shape
                string originalText = shape.Text.Value.Text;

                // If there is no text, skip processing
                if (string.IsNullOrEmpty(originalText))
                    continue;

                // Split the text into lines, remove empty lines, and re‑join
                string[] lines = originalText.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
                string cleanedText = string.Join("\r\n", Array.FindAll(lines, line => !string.IsNullOrWhiteSpace(line)));

                // If cleaning removed any empty paragraphs, replace the shape's text
                if (!string.Equals(originalText, cleanedText, StringComparison.Ordinal))
                {
                    // Clear existing text runs
                    shape.Text.Value.Clear();

                    // Add the cleaned text as a single Txt run
                    shape.Text.Value.Add(new Txt(cleanedText));
                }
            }

            // Save the modified diagram (as VSDX in this example)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }