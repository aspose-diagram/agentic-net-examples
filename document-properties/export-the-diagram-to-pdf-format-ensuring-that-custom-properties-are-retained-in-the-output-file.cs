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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Report the number of custom document properties (they are retained automatically on save).
                int customPropCount = diagram.DocumentProps.CustomProps.Count;
                Console.WriteLine($"Diagram contains {customPropCount} custom property(ies).");

                // List each custom property: Name, Type, and Value.
                for (int i = 0; i < customPropCount; i++)
                {
                    CustomProp prop = diagram.DocumentProps.CustomProps[i];
                    string name = prop.Name;
                    string type = prop.PropType.ToString();
                    string value = prop.CustomValue?.ValueString ?? string.Empty;
                    Console.WriteLine($"Custom Property - Name: {name}, Type: {type}, Value: {value}");
                }

                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Set the target format explicitly.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Optionally set a default font to avoid missing‑font warnings.
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF. Custom properties are preserved by default.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Diagram successfully saved to PDF: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}