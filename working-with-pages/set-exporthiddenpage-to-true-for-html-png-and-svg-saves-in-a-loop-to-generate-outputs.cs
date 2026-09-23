using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define output formats and corresponding save actions
                var formats = new (string Extension, Action<string> SaveAction)[]
                {
                    ("html", outputPath =>
                    {
                        var htmlOptions = new HTMLSaveOptions();
                        htmlOptions.ExportHiddenPage = true;
                        diagram.Save(outputPath, htmlOptions);
                    }),
                    ("png", outputPath =>
                    {
                        var imageOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        imageOptions.ExportHiddenPage = true;
                        diagram.Save(outputPath, imageOptions);
                    }),
                    ("svg", outputPath =>
                    {
                        var svgOptions = new SVGSaveOptions();
                        svgOptions.ExportHiddenPage = true;
                        diagram.Save(outputPath, svgOptions);
                    })
                };

                // Export the diagram in each format
                foreach (var (extension, saveAction) in formats)
                {
                    string outputPath = $"output.{extension}";
                    try
                    {
                        saveAction(outputPath);
                        Console.WriteLine($"Saved {outputPath} with ExportHiddenPage = true.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to save {outputPath}: {ex.Message}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
