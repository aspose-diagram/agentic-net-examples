using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramConversionWithLocalization
{
    // Simple localization provider for progress messages
    internal static class LocalizationProvider
    {
        // Language -> (MessageKey -> Message)
        private static readonly Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            {
                "en", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "SelectLanguage", "Select language (en/es): " },
                    { "EnterInputPath", "Enter the path of the source diagram file: " },
                    { "EnterOutputPath", "Enter the desired output file path: " },
                    { "Loading", "Loading diagram..." },
                    { "Converting", "Converting diagram..." },
                    { "Saving", "Saving diagram..." },
                    { "Completed", "Conversion completed successfully." },
                    { "Error", "An error occurred: " }
                }
            },
            {
                "es", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "SelectLanguage", "Seleccione el idioma (en/es): " },
                    { "EnterInputPath", "Ingrese la ruta del archivo de diagrama origen: " },
                    { "EnterOutputPath", "Ingrese la ruta de salida deseada: " },
                    { "Loading", "Cargando diagrama..." },
                    { "Converting", "Convirtiendo diagrama..." },
                    { "Saving", "Guardando diagrama..." },
                    { "Completed", "Conversión completada con éxito." },
                    { "Error", "Ocurrió un error: " }
                }
            }
        };

        // Retrieves a localized message; falls back to English if missing
        public static string GetMessage(string language, string key)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "en";

            if (_messages.TryGetValue(language, out var langDict) && langDict.TryGetValue(key, out var message))
                return message;

            // Fallback to English
            if (_messages["en"].TryGetValue(key, out var fallback))
                return fallback;

            // If still not found, return the key itself
            return key;
        }
    }

    internal class Program
    {
        static void Main()
        {
            // Choose language
            Console.Write(LocalizationProvider.GetMessage("en", "SelectLanguage"));
            string language = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(language))
                language = "en";

            // Input diagram path
            Console.Write(LocalizationProvider.GetMessage(language, "EnterInputPath"));
            string inputPath = Console.ReadLine()?.Trim();

            // Output diagram path
            Console.Write(LocalizationProvider.GetMessage(language, "EnterOutputPath"));
            string outputPath = Console.ReadLine()?.Trim();

            try
            {
                // Loading phase
                Console.WriteLine(LocalizationProvider.GetMessage(language, "Loading"));
                Diagram diagram = new Diagram(inputPath);

                // Converting phase (placeholder for any processing)
                Console.WriteLine(LocalizationProvider.GetMessage(language, "Converting"));
                // Example: no additional processing; could add layout, etc.

                // Saving phase
                Console.WriteLine(LocalizationProvider.GetMessage(language, "Saving"));
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Completion message
                Console.WriteLine(LocalizationProvider.GetMessage(language, "Completed"));
            }
            catch (Exception ex)
            {
                // Localized error output
                Console.WriteLine($"{LocalizationProvider.GetMessage(language, "Error")}{ex.Message}");
            }
        }
    }
}