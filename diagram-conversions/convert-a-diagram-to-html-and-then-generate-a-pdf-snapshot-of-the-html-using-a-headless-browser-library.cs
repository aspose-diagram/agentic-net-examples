using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output file paths
        string baseName = Path.GetFileNameWithoutExtension(inputPath);
        string outputDirectory = Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();
        string htmlPath = Path.Combine(outputDirectory, $"{baseName}.html");
        string pdfPath = Path.Combine(outputDirectory, $"{baseName}.pdf");

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Export the entire diagram to HTML
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                diagram.Save(htmlPath, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML export: {ex.Message}");
            return;
        }

        // Verify that the HTML file was created before attempting PDF conversion
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML export failed, file not found: {htmlPath}");
            return;
        }

        try
        {
            // Convert the generated HTML to PDF using Aspose.Pdf (fully qualified types)
            // Load the HTML file
            Aspose.Pdf.HtmlLoadOptions loadOptions = new Aspose.Pdf.HtmlLoadOptions();
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(htmlPath, loadOptions);

            // Save the PDF snapshot
            pdfDocument.Save(pdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF generation: {ex.Message}");
            return;
        }

        // Final confirmation
        Console.WriteLine($"HTML file created at: {htmlPath}");
        Console.WriteLine($"PDF snapshot created at: {pdfPath}");
    }
}