using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the saved Visio file
            string outputPath = "output.vsdx";

            // Resource dictionary for localization (original text -> localized text)
            var localizationMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Page1", "Seite1" },
                { "Page2", "Seite2" },
                // Add more mappings as needed
            };

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and translate metadata
                foreach (Page page in diagram.Pages)
                {
                    // Translate page name if a mapping exists
                    if (localizationMap.TryGetValue(page.Name, out string localizedName))
                    {
                        page.Name = localizedName;
                    }

                    // Translate universal page name if needed
                    if (localizationMap.TryGetValue(page.NameU, out string localizedNameU))
                    {
                        page.NameU = localizedNameU;
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram saved with localized page metadata.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
