using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source VSDX file
                string inputPath = "inputDiagram.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Set a custom default font for rendering.
                // This font name must be installed on the system or located in a font folder added to FontConfigs.
                FontConfigs.DefaultFontName = "Calibri";

                // Optional: add a custom font folder if the font is not in the default system fonts.
                // FontConfigs.SetFontFolder(@"C:\CustomFonts", true);

                // Save the diagram to PDF to trigger rendering with the custom default font.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Calibri"; // Ensure the save options also use the custom font.

                diagram.Save("outputDiagram.pdf", pdfOptions);

                Console.WriteLine("Diagram loaded and saved with custom default font.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }