using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramPlaceholderUpdater <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the plain text of the shape.
                    string currentText = shape.Text.Value.ToString();

                    // If the text is null, empty, or whitespace, set a placeholder.
                    if (string.IsNullOrWhiteSpace(currentText))
                    {
                        // Clear any existing text runs.
                        shape.Text.Value.Clear();

                        // Add the placeholder text.
                        shape.Text.Value.Add(new Txt("Placeholder"));
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }