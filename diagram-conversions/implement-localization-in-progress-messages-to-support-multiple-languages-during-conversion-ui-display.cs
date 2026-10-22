using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Supported languages
    enum Language
    {
        English,
        Spanish
    }

    // Localization dictionaries
    static readonly Dictionary<string, string> EnglishMessages = new Dictionary<string, string>
    {
        { "SelectLanguage", "Select language (en/es): " },
        { "InvalidLanguage", "Invalid selection. Defaulting to English." },
        { "InputPathPrompt", "Enter the path to the Visio file to convert: " },
        { "OutputPathPrompt", "Enter the desired output PDF path: " },
        { "FileNotFound", "File not found: {0}" },
        { "ConversionStart", "Starting diagram conversion..." },
        { "PageStart", "Saving page {0} of {1}..." },
        { "PageEnd", "Finished page {0} of {1}." },
        { "ConversionComplete", "Diagram conversion completed successfully." },
        { "ConversionError", "An error occurred during conversion: {0}" }
    };

    static readonly Dictionary<string, string> SpanishMessages = new Dictionary<string, string>
    {
        { "SelectLanguage", "Seleccione el idioma (en/es): " },
        { "InvalidLanguage", "Selección no válida. Se usará inglés por defecto." },
        { "InputPathPrompt", "Ingrese la ruta del archivo Visio a convertir: " },
        { "OutputPathPrompt", "Ingrese la ruta de salida del PDF: " },
        { "FileNotFound", "Archivo no encontrado: {0}" },
        { "ConversionStart", "Iniciando la conversión del diagrama..." },
        { "PageStart", "Guardando página {0} de {1}..." },
        { "PageEnd", "Página {0} de {1} completada." },
        { "ConversionComplete", "Conversión del diagrama completada con éxito." },
        { "ConversionError", "Ocurrió un error durante la conversión: {0}" }
    };

    // Retrieves a localized message based on the selected language
    static string GetMessage(string key, Language lang)
    {
        switch (lang)
        {
            case Language.Spanish:
                if (SpanishMessages.TryGetValue(key, out var esMsg))
                    return esMsg;
                break;
            case Language.English:
            default:
                if (EnglishMessages.TryGetValue(key, out var enMsg))
                    return enMsg;
                break;
        }
        // Fallback to key if not found
        return key;
    }

    static void Main(string[] args)
    {
        // Prompt user for language selection
        Console.Write(GetMessage("SelectLanguage", Language.English));
        string langInput = Console.ReadLine()?.Trim().ToLowerInvariant();
        Language selectedLanguage = Language.English;
        if (langInput == "es")
            selectedLanguage = Language.Spanish;
        else if (langInput != "en")
            Console.WriteLine(GetMessage("InvalidLanguage", selectedLanguage));

        // Prompt for input file path
        Console.Write(GetMessage("InputPathPrompt", selectedLanguage));
        string inputPath = Console.ReadLine()?.Trim();

        // Guard: ensure input file exists
        if (string.IsNullOrEmpty(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine(string.Format(GetMessage("FileNotFound", selectedLanguage), inputPath));
            return;
        }

        // Prompt for output file path
        Console.Write(GetMessage("OutputPathPrompt", selectedLanguage));
        string outputPath = Console.ReadLine()?.Trim();

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare PDF save options and assign the progress callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new ProgressCallback(selectedLanguage);

            // Inform user that conversion is starting
            Console.WriteLine(GetMessage("ConversionStart", selectedLanguage));

            // Save the diagram as PDF using the options
            diagram.Save(outputPath, pdfOptions);

            // Inform user that conversion completed
            Console.WriteLine(GetMessage("ConversionComplete", selectedLanguage));
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine(string.Format(GetMessage("ConversionError", selectedLanguage), ex.Message));
        }
    }

    // Implementation of IPageSavingCallback to report progress
    class ProgressCallback : IPageSavingCallback
    {
        private readonly Language _language;

        public ProgressCallback(Language language)
        {
            _language = language;
        }

        // Called before a page starts saving
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // args.PageIndex is zero‑based; display as 1‑based for user friendliness
            int pageNumber = args.PageIndex + 1;
            int totalPages = args.PageCount;
            Console.WriteLine(string.Format(GetMessage("PageStart", _language), pageNumber, totalPages));
        }

        // Called after a page has been saved
        public void PageEndSaving(PageEndSavingArgs args)
        {
            int pageNumber = args.PageIndex + 1;
            int totalPages = args.PageCount;
            Console.WriteLine(string.Format(GetMessage("PageEnd", _language), pageNumber, totalPages));

            // Continue processing remaining pages
            args.HasMorePages = true;
        }
    }
}