using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ----- Set built-in document properties -----
                // Set creation time (TimeCreated) to current time
                // This property may be read‑only in some versions; wrap in try/catch to avoid failure
                try
                {
                    diagram.DocumentProps.TimeCreated = DateTime.Now;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Warning: Unable to set TimeCreated property: {ex.Message}");
                }

                // ----- Add a custom property for Author -----
                // Create a new custom property named "Author"
                CustomProp authorProp = new CustomProp();
                authorProp.Name = "Author";
                // Set the property type to string
                authorProp.PropType = PropType.String;
                // Assign the author value
                authorProp.CustomValue.ValueString = "John Doe";

                // Add the custom property to the document's custom properties collection
                diagram.DocumentProps.CustomProps.Add(authorProp);

                // ----- Prepare PDF save options -----
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Optionally set a default font to avoid missing‑font warnings
                pdfOptions.DefaultFont = "Arial";

                // ----- Save the diagram as PDF -----
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram successfully converted to PDF with metadata at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}