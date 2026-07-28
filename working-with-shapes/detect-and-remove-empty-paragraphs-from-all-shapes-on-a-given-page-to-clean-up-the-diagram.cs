using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect at least input file path and page index.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramCleanup <inputFilePath> <pageIndex> [outputFilePath]");
                return;
            }

            string inputPath = args[0];
            if (!int.TryParse(args[1], out int pageIndex))
            {
                Console.WriteLine("Invalid page index.");
                return;
            }

            string outputPath = args.Length > 2 ? args[2] : "cleaned_output.vsdx";

            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Validate page index.
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.WriteLine($"Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                return;
            }

            // Access the specified page.
            Page page = diagram.Pages[pageIndex];

            // Iterate through all shapes on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // Retrieve the plain text of the shape.
                string plainText = shape.Text.Value.Text ?? string.Empty;

                // Split the text into lines/paragraphs.
                string[] lines = plainText.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);

                // Filter out empty or whitespace-only paragraphs.
                string[] nonEmptyLines = lines.Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

                // If any empty paragraphs were found, rebuild the text without them.
                if (nonEmptyLines.Length != lines.Length)
                {
                    shape.Text.Value.Clear();
                    string newText = string.Join("\r\n", nonEmptyLines);
                    shape.Text.Value.Add(new Txt(newText));
                }
            }

            // Save the cleaned diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }