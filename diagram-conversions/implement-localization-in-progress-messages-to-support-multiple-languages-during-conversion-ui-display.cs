using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramConversion
{
    // Simple localization helper
    public static class Localizer
    {
        // Language code -> (key -> message)
        private static readonly Dictionary<string, Dictionary<string, string>> _messages = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "en", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Loading", "Loading diagram..." },
                    { "Saving", "Saving diagram..." },
                    { "Completed", "Conversion completed successfully." },
                    { "Error", "An error occurred: {0}" }
                }
            },
            { "es", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Loading", "Cargando el diagrama..." },
                    { "Saving", "Guardando el diagrama..." },
                    { "Completed", "Conversión completada con éxito." },
                    { "Error", "Ocurrió un error: {0}" }
                }
            },
            { "fr", new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Loading", "Chargement du diagramme..." },
                    { "Saving", "Enregistrement du diagramme..." },
                    { "Completed", "Conversion terminée avec succès." },
                    { "Error", "Une erreur s'est produite : {0}" }
                }
            }
        };

        // Selected language (default to English)
        public static string Language { get; set; } = "en";

        // Retrieve a localized message; fallback to English if missing
        public static string Get(string key, params object[] args)
        {
            if (!_messages.TryGetValue(Language, out var langDict) || !langDict.TryGetValue(key, out var template))
            {
                // Fallback to English
                _messages["en"].TryGetValue(key, out template);
                template ??= key; // if still missing, use key itself
            }

            return args != null && args.Length > 0 ? string.Format(template, args) : template;
        }
    }

    class Program
    {
        static void Main()
        {
            // Prompt user for language selection
            Console.WriteLine("Select language (en/es/fr):");
            string inputLang = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(inputLang) && Localizer.Language != null)
            {
                Localizer.Language = inputLang;
            }

            // Paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.png";

            try
            {
                // Show localized loading message
                Console.WriteLine(Localizer.Get("Loading"));

                // Load diagram using Aspose.Diagram
                Diagram diagram = new Diagram(inputPath);

                // Show localized saving message
                Console.WriteLine(Localizer.Get("Saving"));

                // Prepare PNG save options
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

                // Save diagram to PNG
                diagram.Save(outputPath, saveOptions);

                // Show localized completion message
                Console.WriteLine(Localizer.Get("Completed"));
            }
            catch (Exception ex)
            {
                // Show localized error message
                Console.WriteLine(Localizer.Get("Error", ex.Message));
            }
        }
    }
}