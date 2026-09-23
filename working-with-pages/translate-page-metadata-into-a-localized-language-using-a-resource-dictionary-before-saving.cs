using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Paths to the source and the localized output diagram
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output_localized.vsdx";

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Resource dictionary that maps original text to its localized version
                var resourceDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Title", "Titre" },
                    { "Company", "Entreprise" },
                    // Add additional translations as needed
                };

                // Translate built‑in document properties (Title and Company are supported)
                if (!string.IsNullOrEmpty(diagram.DocumentProps.Title) &&
                    resourceDictionary.TryGetValue(diagram.DocumentProps.Title, out var localizedTitle))
                {
                    diagram.DocumentProps.Title = localizedTitle;
                }

                if (!string.IsNullOrEmpty(diagram.DocumentProps.Company) &&
                    resourceDictionary.TryGetValue(diagram.DocumentProps.Company, out var localizedCompany))
                {
                    diagram.DocumentProps.Company = localizedCompany;
                }

                // Translate custom document properties
                var customProps = diagram.DocumentProps.CustomProps;
                for (int i = 0; i < customProps.Count; i++)
                {
                    var prop = customProps[i];

                    // Translate the property name if a translation exists
                    if (resourceDictionary.TryGetValue(prop.Name, out var localizedName))
                    {
                        prop.Name = localizedName;
                    }

                    // Translate the property value if a translation exists
                    string currentValue = prop.CustomValue.ValueString;
                    if (!string.IsNullOrEmpty(currentValue) &&
                        resourceDictionary.TryGetValue(currentValue, out var localizedValue))
                    {
                        prop.CustomValue.ValueString = localizedValue;
                    }

                    // Ensure the property type is string
                    prop.PropType = PropType.String;
                }

                // Save the diagram with the localized metadata
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}