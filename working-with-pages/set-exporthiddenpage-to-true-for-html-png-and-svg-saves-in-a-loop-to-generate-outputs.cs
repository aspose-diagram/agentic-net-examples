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
                const string inputPath = "input.vsdx";

                // Ensure the diagram is disposed after use
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Define the export configurations for each format
                    var exportConfigs = new (string FormatName, string Extension, SaveOptions Options)[]
                    {
                        // HTML export
                        ("HTML", "html", new HTMLSaveOptions
                        {
                            ExportHiddenPage = true
                        }),

                        // PNG image export
                        ("PNG", "png", new ImageSaveOptions(SaveFileFormat.Png)
                        {
                            ExportHiddenPage = true
                        }),

                        // SVG vector export
                        ("SVG", "svg", new SVGSaveOptions
                        {
                            ExportHiddenPage = true
                        })
                    };

                    // Loop through each configuration and save the diagram
                    foreach (var config in exportConfigs)
                    {
                        string outputPath = $"output_{config.FormatName.ToLower()}.{config.Extension}";
                        diagram.Save(outputPath, config.Options);
                        Console.WriteLine($"Saved {config.FormatName} to: {outputPath}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }