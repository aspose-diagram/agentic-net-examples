using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string sourcePath = "input.vsdx";
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        string outputFolder = "output";
        Directory.CreateDirectory(outputFolder);

        try
        {
            Diagram diagram = new Diagram(sourcePath);

            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                options.PageIndex = i;

                string pageName = diagram.Pages[i].Name;
                if (string.IsNullOrWhiteSpace(pageName))
                    pageName = $"Page_{i + 1}";
                string outputPath = Path.Combine(outputFolder, $"{pageName}.png");

                try
                {
                    diagram.Save(outputPath, options);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save page {i + 1}: {ex.Message}");
                }
            }

            Console.WriteLine("All pages have been exported as PNG images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}