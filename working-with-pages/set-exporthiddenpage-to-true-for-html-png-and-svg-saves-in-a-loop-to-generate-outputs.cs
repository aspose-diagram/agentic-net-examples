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

            // Load the Visio diagram from a file.
            // Replace "input.vsdx" with the actual path to your diagram.
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Define the output formats and their corresponding save options.
                var outputs = new (string Extension, SaveOptions Options)[]
                {
                    // HTML export with hidden pages included.
                    ("html", new HTMLSaveOptions { ExportHiddenPage = true }),

                    // PNG image export with hidden pages included.
                    ("png", new ImageSaveOptions(SaveFileFormat.Png) { ExportHiddenPage = true }),

                    // SVG vector export with hidden pages included.
                    ("svg", new SVGSaveOptions { ExportHiddenPage = true })
                };

                // Loop through each format and save the diagram.
                foreach (var output in outputs)
                {
                    string outputPath = $"output.{output.Extension}";
                    diagram.Save(outputPath, output.Options);
                    Console.WriteLine($"Saved {outputPath}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
