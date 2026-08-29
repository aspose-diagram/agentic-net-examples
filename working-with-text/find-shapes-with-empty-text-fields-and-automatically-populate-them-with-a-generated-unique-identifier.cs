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
                Console.WriteLine("Usage: DiagramTextPopulator <inputVisioFile> <outputVisioFile>");
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
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text of the shape.
                    string currentText = shape.Text.Value.ToString();

                    // If the shape has no text (null, empty or whitespace), assign a unique identifier.
                    if (string.IsNullOrWhiteSpace(currentText))
                    {
                        string uniqueId = Guid.NewGuid().ToString();

                        // Clear any existing text runs and add the new identifier.
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(uniqueId));

                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' populated with ID: {uniqueId}");
                    }
                }
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }