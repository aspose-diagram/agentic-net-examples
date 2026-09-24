using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Folder containing the VSDX files
        string folderPath = @"C:\Diagrams";

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Get all VSDX files in the folder
        string[] files = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

        foreach (string filePath in files)
        {
            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Apply a uniform preset theme to every page
                foreach (Page page in diagram.Pages)
                {
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // Save the diagram back to the same file
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }
    }
}
