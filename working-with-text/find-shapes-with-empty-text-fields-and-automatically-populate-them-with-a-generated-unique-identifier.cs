using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output path – use second argument if supplied, otherwise create a default name.
        string outputPath;
        if (args.Length > 1)
        {
            outputPath = args[1];
        }
        else
        {
            string dir = Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();
            string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(dir, $"{nameWithoutExt}_updated.vsdx");
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape.
                    string currentText = shape.Text.Value.ToString();

                    // If the shape's text is empty or whitespace, populate it with a unique identifier.
                    if (string.IsNullOrWhiteSpace(currentText))
                    {
                        string uniqueId = Guid.NewGuid().ToString();

                        // Add a new Txt object containing the unique identifier.
                        // This creates the text content for the shape.
                        shape.Text.Value.Add(new Txt(uniqueId));
                    }
                }
            }

            // Save the modified diagram to the specified output file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram processed and saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}