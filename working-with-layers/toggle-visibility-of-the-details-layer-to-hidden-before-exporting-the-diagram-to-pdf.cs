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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through the layers collection of the current page.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Compare the layer name using the .Value property.
                        if (layer.Name.Value == "Details")
                        {
                            // Hide the layer by setting its Visible property to FALSE.
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Ensure hidden pages are not exported.
                pdfOptions.ExportHiddenPage = false;
                // Explicitly set the format for the options object.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the modified diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}