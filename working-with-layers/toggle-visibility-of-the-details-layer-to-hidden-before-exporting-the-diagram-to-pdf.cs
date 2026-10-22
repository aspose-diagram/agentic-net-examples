using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        // Guard: verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page to locate the 'Details' layer.
                foreach (Page page in diagram.Pages)
                {
                    // Access the layer collection of the current page.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Compare the layer name using the .Value property.
                        if (layer.Name.Value == "Details")
                        {
                            // Hide the layer by setting its Visible property to FALSE.
                            layer.Visible.Value = BOOL.False;
                            // Optionally hide the layer from printing as well.
                            layer.Print.Value = BOOL.False;
                        }
                    }
                }

                // Prepare PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format to PDF (required by the API).
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the modified diagram as PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram exported successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}