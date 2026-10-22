using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Determine output PDF path. Use second argument if supplied; otherwise, replace extension with .pdf.
        string outputPath = args.Length > 1 ? args[1] : Path.ChangeExtension(inputPath, ".pdf");

        // Derive the prefix from the input file name (without extension).
        string prefix = Path.GetFileNameWithoutExtension(inputPath);

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and shapes to rename them.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Preserve the original universal name.
                        string originalName = shape.NameU;

                        // Construct the new name with the derived prefix.
                        string newName = $"{prefix}_{originalName}";

                        // Apply the new name to both NameU and Name properties.
                        shape.NameU = newName;
                        shape.Name = newName;
                    }
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker as required.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the modified diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Successfully saved PDF to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}