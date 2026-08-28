using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – change as needed.
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output SVG file path.
        string outputPath = "page.svg";

        try
        {
            // Load the Visio diagram from the file.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0) of the diagram.
            Page page = diagram.Pages[0];

            // Create SVG save options (no built‑in CSS support, will inject later).
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Export only the selected page.
                PageIndex = 0,

                // Fit the SVG viewbox to the page size.
                SVGFitToViewPort = true,

                // Do not export hidden pages.
                ExportHiddenPage = false
            };

            // Save the diagram (only the selected page) as SVG using the options.
            diagram.Save(outputPath, svgOptions);

            // Dispose the diagram to release resources.
            diagram.Dispose();

            // Define custom CSS that should style all shapes and connectors.
            string customCss = @"
                .shape { fill:#ffcccc; stroke:#ff0000; stroke-width:1px; }
                .connector { fill:none; stroke:#0000ff; stroke-width:2px; }
            ";

            // Ensure the SVG file was created before attempting to modify it.
            if (!File.Exists(outputPath))
            {
                Console.Error.WriteLine($"SVG file not found after save: {outputPath}");
                return;
            }

            // Read the generated SVG content.
            string svgContent = File.ReadAllText(outputPath);

            // Locate the end of the opening <svg> tag to insert a <style> block.
            int insertPos = svgContent.IndexOf('>');
            if (insertPos != -1)
            {
                // Build a <style> element containing the custom CSS wrapped in CDATA.
                string styleBlock = $"<style type=\"text/css\"><![CDATA[{customCss}]]></style>\n";

                // Insert the style block immediately after the opening <svg> tag.
                svgContent = svgContent.Insert(insertPos + 1, styleBlock);

                // Overwrite the SVG file with the modified content.
                File.WriteAllText(outputPath, svgContent);
            }
            else
            {
                Console.Error.WriteLine("Failed to locate <svg> tag in the generated file.");
            }

            Console.WriteLine($"Page exported to SVG successfully with custom CSS: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during SVG export: {ex.Message}");
        }
    }
}