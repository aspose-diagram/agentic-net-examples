using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, output HTML file, output PDF file.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputHtmlPath> <outputPdfPath>");
            return;
        }

        string visioPath = args[0];
        string htmlPath = args[1];
        string pdfPath = args[2];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Export the entire diagram to a single HTML file.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            diagram.Save(htmlPath, htmlOptions);

            // Dispose the diagram to free resources.
            diagram.Dispose();

            // Convert the generated HTML to PDF using Aspose.Pdf (headless rendering).
            // Fully qualified names are used to avoid ambiguity with Aspose.Diagram types.
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(htmlPath);
            pdfDocument.Save(pdfPath, Aspose.Pdf.SaveFormat.Pdf);
            pdfDocument.Dispose();

            Console.WriteLine("Conversion completed successfully.");
            Console.WriteLine($"HTML output: {htmlPath}");
            Console.WriteLine($"PDF snapshot: {pdfPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine("An error occurred during conversion:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}