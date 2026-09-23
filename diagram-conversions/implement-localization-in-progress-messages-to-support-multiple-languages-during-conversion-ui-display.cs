using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Simple localization helper
    class Localizer
    {
        private readonly Dictionary<string, string> _messages;

        public Localizer(string languageCode)
        {
            // Define messages for supported languages
            var resources = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "en", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "Loading", "Loading diagram..." },
                        { "SavingPdf", "Saving diagram as PDF..." },
                        { "SavingPng", "Saving diagram as PNG..." },
                        { "PageStart", "Starting page {0} of {1}..." },
                        { "PageEnd", "Finished page {0} of {1}." },
                        { "ConversionComplete", "Conversion completed successfully." }
                    }
                },
                {
                    "es", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "Loading", "Cargando el diagrama..." },
                        { "SavingPdf", "Guardando el diagrama como PDF..." },
                        { "SavingPng", "Guardando el diagrama como PNG..." },
                        { "PageStart", "Iniciando página {0} de {1}..." },
                        { "PageEnd", "Finalizada página {0} de {1}." },
                        { "ConversionComplete", "Conversión completada con éxito." }
                    }
                }
            };

            if (!resources.TryGetValue(languageCode, out var selected))
            {
                // Fallback to English if language not supported
                selected = resources["en"];
            }

            _messages = selected;
        }

        public string Get(string key, params object[] args)
        {
            if (_messages.TryGetValue(key, out var format))
            {
                return args.Length > 0 ? string.Format(format, args) : format;
            }
            // Return key itself if missing
            return key;
        }
    }

    // Callback to display localized page saving progress for PDF export
    class LocalizedPageSavingCallback : IPageSavingCallback
    {
        private readonly Localizer _localizer;

        public LocalizedPageSavingCallback(Localizer localizer)
        {
            _localizer = localizer;
        }

        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine(_localizer.Get("PageStart", args.PageIndex + 1, args.PageCount));
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine(_localizer.Get("PageEnd", args.PageIndex + 1, args.PageCount));
        }
    }

    class Program
    {
        static void Main()
        {
            // Choose language (e.g., "en" for English, "es" for Spanish)
            Console.Write("Enter language code (en/es): ");
            string lang = Console.ReadLine()?.Trim() ?? "en";

            var localizer = new Localizer(lang);

            // Paths (adjust as needed)
            string inputPath = "input.vsdx";
            string pdfOutput = "output.pdf";
            string pngOutput = "output.png";

            // Load diagram
            Console.WriteLine(localizer.Get("Loading"));
            Diagram diagram = new Diagram(inputPath);

            // Save as PDF with localized page callbacks
            Console.WriteLine(localizer.Get("SavingPdf"));
            var pdfOptions = new PdfSaveOptions();
            pdfOptions.PageSavingCallback = new LocalizedPageSavingCallback(localizer);
            diagram.Save(pdfOutput, pdfOptions);

            // Save as PNG
            Console.WriteLine(localizer.Get("SavingPng"));
            var pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(pngOutput, pngOptions);

            Console.WriteLine(localizer.Get("ConversionComplete"));
        }
    }
}