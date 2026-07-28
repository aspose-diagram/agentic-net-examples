using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram from a file.
                // Replace "input.vsdx" with the actual path to your diagram.
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Export to HTML with hidden pages included.
                    HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                    htmlOptions.ExportHiddenPage = true;
                    diagram.Save("output.html", htmlOptions);

                    // Export to PNG image with hidden pages included.
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    pngOptions.ExportHiddenPage = true;
                    diagram.Save("output.png", pngOptions);

                    // Export to SVG with hidden pages included.
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    svgOptions.ExportHiddenPage = true;
                    diagram.Save("output.svg", svgOptions);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }