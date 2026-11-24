using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class LocalizedProgressCallback : IPageSavingCallback
{
    private readonly string _language;
    private readonly Dictionary<string, string> _startMessages;
    private readonly Dictionary<string, string> _endMessages;

    public LocalizedProgressCallback(string language)
    {
        _language = language?.ToLowerInvariant() ?? "en";

        // Define start messages per language
        _startMessages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "en", "Saving page {0} of {1}..." },
            { "es", "Guardando página {0} de {1}..." },
            { "fr", "Enregistrement de la page {0} sur {1}..." },
            { "de", "Seite {0} von {1} wird gespeichert..." }
        };

        // Define end messages per language
        _endMessages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "en", "Finished page {0}." },
            { "es", "Página {0} terminada." },
            { "fr", "Page {0} terminée." },
            { "de", "Seite {0} abgeschlossen." }
        };
    }

    // Called before a page starts saving
    public void PageStartSaving(PageStartSavingArgs args)
    {
        try
        {
            string format = _startMessages.ContainsKey(_language) ? _startMessages[_language] : _startMessages["en"];
            Console.WriteLine(string.Format(format, args.PageIndex + 1, args.PageCount));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in PageStartSaving callback: {ex.Message}");
        }
    }

    // Called after a page has been saved
    public void PageEndSaving(PageEndSavingArgs args)
    {
        try
        {
            string format = _endMessages.ContainsKey(_language) ? _endMessages[_language] : _endMessages["en"];
            Console.WriteLine(string.Format(format, args.PageIndex + 1));

            // Example: stop after first page (optional logic)
            // args.HasMorePages = false;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error in PageEndSaving callback: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output paths; optional language code
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath> [languageCode]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string language = args.Length >= 3 ? args[2] : "en";

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare PDF save options with progress callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new LocalizedProgressCallback(language);

            // Save the diagram to PDF while tracking progress
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}