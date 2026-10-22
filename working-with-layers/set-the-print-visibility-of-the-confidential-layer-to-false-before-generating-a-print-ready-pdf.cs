using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages to locate the 'Confidential' layer.
            foreach (Page page in diagram.Pages)
            {
                // Layers are stored inside the page's PageSheet.
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Compare layer name using the .Value property.
                    if (layer.Name.Value == "Confidential")
                    {
                        // Set the layer's print visibility to false.
                        layer.Print.Value = BOOL.False;
                    }
                }
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set the format.

            // Save the diagram as a print‑ready PDF.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"PDF successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}