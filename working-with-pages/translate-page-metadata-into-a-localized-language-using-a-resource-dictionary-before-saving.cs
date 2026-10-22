using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Prepare a simple resource dictionary for translation.
        // Keys are original metadata strings; values are localized equivalents.
        Dictionary<string, string> resourceDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Home", "Accueil" },
            { "Report", "Rapport" },
            { "Summary", "Résumé" }
            // Add more translations as needed.
        };

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page and translate its metadata if a translation exists.
                foreach (Page page in diagram.Pages)
                {
                    // Translate the page's Name property.
                    if (!string.IsNullOrEmpty(page.Name) && resourceDictionary.TryGetValue(page.Name, out string localizedName))
                    {
                        page.Name = localizedName;
                    }

                    // Translate the page's universal NameU property.
                    if (!string.IsNullOrEmpty(page.NameU) && resourceDictionary.TryGetValue(page.NameU, out string localizedNameU))
                    {
                        page.NameU = localizedNameU;
                    }

                    // Additional page metadata can be translated here if needed,
                    // using other accessible properties such as page.BackPage, etc.
                }

                // Save the modified diagram to the specified output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}