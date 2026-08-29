using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output file paths.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputDiagramPath> <outputDiagramPath> [actualName]");
            return;
        }

        // Input diagram file path.
        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output diagram file path.
        string outputPath = args[1];

        // Determine the actual name to replace the placeholder with.
        string actualName = args.Length >= 3 ? args[2] : null;
        if (string.IsNullOrEmpty(actualName))
        {
            Console.Write("Enter the actual name to replace '[Name]': ");
            actualName = Console.ReadLine();
            if (string.IsNullOrEmpty(actualName))
            {
                Console.Error.WriteLine("No name provided. Exiting.");
                return;
            }
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked for deletion.
                    if (shape.Del == BOOL.True) continue;

                    // Ensure the shape has a Text collection.
                    if (shape.Text == null) continue;

                    // Retrieve the plain text of the shape.
                    string currentText = shape.Text.Value.Text;

                    // Check if the placeholder exists in the shape's text.
                    if (currentText != null && currentText.Contains("[Name]"))
                    {
                        // Replace all occurrences of the placeholder with the actual name.
                        string newText = currentText.Replace("[Name]", actualName);

                        // Clear existing text runs.
                        shape.Text.Value.Clear();

                        // Add the updated text as a new Txt run.
                        shape.Text.Value.Add(new Txt(newText));
                    }
                }
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}