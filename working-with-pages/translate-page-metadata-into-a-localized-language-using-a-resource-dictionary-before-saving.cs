using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output Visio file path (localized version)
                string outputPath = "output_localized.vsdx";

                // Load the diagram from file
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Resource dictionary that maps original page names to localized strings
                    var resourceDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "Page1", "Página1" },
                        { "Page2", "Página2" },
                        // Add additional mappings as required
                    };

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Localize the page's Name if a translation exists
                        if (resourceDictionary.TryGetValue(page.Name, out string localizedName))
                        {
                            page.Name = localizedName;
                        }

                        // Localize the page's universal NameU if a translation exists
                        if (resourceDictionary.TryGetValue(page.NameU, out string localizedNameU))
                        {
                            page.NameU = localizedNameU;
                        }

                        // If there were custom page-level metadata stored elsewhere,
                        // you would translate it here using the same dictionary.
                    }

                    // Save the modified diagram using VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram localization completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }