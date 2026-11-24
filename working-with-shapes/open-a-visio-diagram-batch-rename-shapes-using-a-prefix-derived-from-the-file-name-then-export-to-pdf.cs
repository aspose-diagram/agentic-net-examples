using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output PDF file path as arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (ensures proper disposal)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Derive prefix from the input file name (without extension)
                string prefix = Path.GetFileNameWithoutExtension(inputPath);

                // Iterate through all pages and shapes to rename them
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build new name using the prefix and the shape's ID to ensure uniqueness
                        string newName = $"{prefix}_{shape.ID}";
                        // Rename the shape (NameU is the universal name)
                        shape.NameU = newName;
                    }
                }

                // Export the modified diagram to PDF
                diagram.Save(outputPath, SaveFileFormat.Pdf);
            }

            Console.WriteLine($"Diagram processed and saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}