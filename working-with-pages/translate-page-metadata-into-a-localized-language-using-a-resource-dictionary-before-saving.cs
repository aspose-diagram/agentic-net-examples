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

        // Resource dictionary that maps original page names to localized equivalents.
        var translationDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Sheet1", "Página1" },
            { "Sheet2", "Página2" },
            // Add additional translations as required.
        };

        try
        {
            // Load the Visio diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page and translate its metadata.
                foreach (Page page in diagram.Pages)
                {
                    // Translate the page name if a matching entry exists in the dictionary.
                    if (!string.IsNullOrEmpty(page.Name) && translationDictionary.TryGetValue(page.Name, out string localizedName))
                    {
                        page.Name = localizedName;
                    }

                    // Additional metadata translation can be added here following the same pattern.
                }

                // Save the modified diagram to the output path using VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}