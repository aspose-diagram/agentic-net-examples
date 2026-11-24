using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ----- Set built‑in document properties (metadata) -----
                // Set the title of the document (built‑in property).
                diagram.DocumentProps.Title = "Sample Diagram";

                // Set the creation date/time of the document (built‑in property).
                diagram.DocumentProps.TimeCreated = DateTime.Now;

                // ----- Add a custom property for the author -----
                // Create a new custom property.
                CustomProp authorProp = new CustomProp();
                authorProp.Name = "Author";
                // Use the PropType enum to specify a string type.
                authorProp.PropType = PropType.String;
                // Assign the author name to the custom value.
                authorProp.CustomValue.ValueString = "John Doe";

                // Add the custom property to the document's custom properties collection.
                diagram.DocumentProps.CustomProps.Add(authorProp);

                // ----- Prepare PDF save options -----
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Explicitly set the format tracker (required by the API).
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF. The metadata set above will be embedded in the PDF.
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully converted to PDF with metadata. Output: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}