using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output_localized.vsdx";

            // Simple resource dictionary: original text -> localized text
            var resourceDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Page1", "Seite1" },
                { "Page2", "Seite2" },
                { "Title", "Titel" },
                { "Author", "Autor" }
                // Add additional translations as needed
            };

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page and translate its metadata
                foreach (Page page in diagram.Pages)
                {
                    // Translate page Name
                    if (resourceDict.TryGetValue(page.Name, out string localizedName))
                    {
                        page.Name = localizedName;
                    }

                    // Translate page universal NameU
                    if (resourceDict.TryGetValue(page.NameU, out string localizedNameU))
                    {
                        page.NameU = localizedNameU;
                    }

                    // Example: translate user-defined cells (custom properties) on shapes within the page
                    foreach (Shape shape in page.Shapes)
                    {
                        foreach (User user in shape.Users)
                        {
                            // Translate the cell's name if a translation exists
                            if (resourceDict.TryGetValue(user.Name, out string localizedCellName))
                            {
                                user.Name = localizedCellName;
                            }

                            // Translate the cell's value if a translation exists
                            if (resourceDict.TryGetValue(user.Value.Val, out string localizedCellValue))
                            {
                                user.Value.Val = localizedCellValue;
                            }
                        }
                    }
                }

                // Save the diagram with localized metadata
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram saved with localized metadata.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
