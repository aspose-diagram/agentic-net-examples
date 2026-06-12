using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramPlaceholderUpdater <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the plain text of the shape.
                    string currentText = shape.Text.Value.Text;

                    // If the shape has no text (null, empty, or whitespace), populate it.
                    if (string.IsNullOrWhiteSpace(currentText))
                    {
                        // Clear any existing text runs.
                        shape.Text.Value.Clear();

                        // Add a default placeholder text run.
                        shape.Text.Value.Add(new Txt("Placeholder"));

                        Console.WriteLine($"Updated shape ID {shape.ID} on page \"{page.Name}\" with placeholder text.");
                    }
                }
            }

            // Save the modified diagram to the output file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }