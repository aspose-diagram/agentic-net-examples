using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input Visio file and output SVG file
            string inputPath = "input.vsdx";
            string outputSvgPath = "output.svg";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Export the first page (index 0) to SVG
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                diagram.Save(outputSvgPath, svgOptions);
            }

            // Read the generated SVG content
            string svgContent = File.ReadAllText(outputSvgPath);

            // Define custom CSS to style shapes and connectors
            string customCss = @"
            <style type=""text/css"">
            .shape { stroke:#ff0000; fill:#00ff00; }
            .connector { stroke:#0000ff; fill:none; }
            </style>
            ";

            // Insert the CSS right after the opening <svg> tag
            int insertPos = svgContent.IndexOf('>') + 1;
            string modifiedSvg = svgContent.Insert(insertPos, "\n" + customCss + "\n");

            // Overwrite the SVG file with the CSS‑enhanced content
            File.WriteAllText(outputSvgPath, modifiedSvg);

            Console.WriteLine($"SVG exported to '{outputSvgPath}' with custom CSS applied.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
