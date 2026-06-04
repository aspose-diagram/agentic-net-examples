using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Simple localization helper
    public static class Localizer
    {
        private static readonly Dictionary<string, Dictionary<string, string>> _messages = new()
        {
            {
                "en", new Dictionary<string, string>
                {
                    { "SelectLanguage", "Select language (en/es): " },
                    { "EnterInputPath", "Enter the path of the Visio file to convert: " },
                    { "EnterOutputPath", "Enter the desired output PNG file path: " },
                    { "LoadingDiagram", "Loading diagram..." },
                    { "DiagramLoaded", "Diagram loaded successfully." },
                    { "Converting", "Converting diagram to PNG..." },
                    { "ConversionCompleted", "Conversion completed." },
                    { "SavingFile", "Saving file..." },
                    { "FileSaved", "File saved at: {0}" },
                    { "Error", "Error: {0}" }
                }
            },
            {
                "es", new Dictionary<string, string>
                {
                    { "SelectLanguage", "Seleccione el idioma (en/es): " },
                    { "EnterInputPath", "Ingrese la ruta del archivo Visio a convertir: " },
                    { "EnterOutputPath", "Ingrese la ruta de salida del archivo PNG: " },
                    { "LoadingDiagram", "Cargando diagrama..." },
                    { "DiagramLoaded", "Diagrama cargado correctamente." },
                    { "Converting", "Convirtiendo diagrama a PNG..." },
                    { "ConversionCompleted", "Conversión completada." },
                    { "SavingFile", "Guardando archivo..." },
                    { "FileSaved", "Archivo guardado en: {0}" },
                    { "Error", "Error: {0}" }
                }
            }
        };

        private static string _currentLang = "en";

        public static void SetLanguage(string langCode)
        {
            if (_messages.ContainsKey(langCode))
                _currentLang = langCode;
        }

        public static string Get(string key, params object[] args)
        {
            if (_messages.TryGetValue(_currentLang, out var dict) && dict.TryGetValue(key, out var msg))
                return args.Length > 0 ? string.Format(msg, args) : msg;
            // Fallback to key if not found
            return key;
        }
    }

    class Program
    {
        static void Main()
        {
            // Select language
            Console.Write(Localizer.Get("SelectLanguage"));
            string lang = Console.ReadLine()?.Trim().ToLower() ?? "en";
            Localizer.SetLanguage(lang);

            // Input file path
            Console.Write(Localizer.Get("EnterInputPath"));
            string inputPath = Console.ReadLine()?.Trim();

            // Output file path
            Console.Write(Localizer.Get("EnterOutputPath"));
            string outputPath = Console.ReadLine()?.Trim();

            try
            {
                // Load diagram
                Console.WriteLine(Localizer.Get("LoadingDiagram"));
                Diagram diagram = new Diagram(inputPath);
                Console.WriteLine(Localizer.Get("DiagramLoaded"));

                // Convert to PNG
                Console.WriteLine(Localizer.Get("Converting"));
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine(Localizer.Get("ConversionCompleted"));

                // Save file (already saved by Save method, but keep message for UI flow)
                Console.WriteLine(Localizer.Get("SavingFile"));
                // No additional action needed; Save already performed.
                Console.WriteLine(Localizer.Get("FileSaved", outputPath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(Localizer.Get("Error", ex.Message));
            }
        }
    }
}