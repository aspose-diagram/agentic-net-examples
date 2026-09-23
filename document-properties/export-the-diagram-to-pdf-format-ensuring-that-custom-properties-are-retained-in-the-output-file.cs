using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Ensure a custom property exists (it will be retained in the PDF)
                // Create a new custom property
                CustomProp customProp = new CustomProp();
                customProp.Name = "ExportedBy";
                customProp.PropType = PropType.String;
                customProp.CustomValue.ValueString = "Aspose.Diagram Exporter";

                // Add the custom property to the document's custom properties collection
                diagram.DocumentProps.CustomProps.Add(customProp);

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";               // Fallback font
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;    // Explicitly set format

                // Export the diagram to PDF while preserving custom properties
                string outputPath = "output.pdf";
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram exported to PDF successfully: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }