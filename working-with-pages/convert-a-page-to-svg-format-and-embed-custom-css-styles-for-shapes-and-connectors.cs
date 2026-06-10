using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Configure SVG save options for the desired page
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                PageIndex = 0,               // 0‑based index of the page to render
                SVGFitToViewPort = true      // Fit the generated SVG to the viewport
            };

            // Save the selected page as an SVG file (temporary location)
            string tempSvgPath = "page0.svg";
            diagram.Save(tempSvgPath, svgOptions);

            // Define custom CSS that will style shapes and connectors in the SVG
            string customCss = @"
                .shape { fill:#ffcc00; stroke:#333333; stroke-width:2; }
                .connector { stroke:#0066ff; stroke-dasharray:5,5; }
            ";

            // Read the generated SVG, inject the CSS inside a <style> element
            string svgContent = File.ReadAllText(tempSvgPath, Encoding.UTF8);
            int insertPos = svgContent.IndexOf('>');
            if (insertPos != -1)
            {
                insertPos++; // position after the closing '>' of the opening <svg> tag
                string styleTag = $"<style type=\"text/css\"><![CDATA[{customCss}]]></style>";
                svgContent = svgContent.Insert(insertPos, styleTag);
                File.WriteAllText(tempSvgPath, svgContent, Encoding.UTF8);
            }

            // Move the final SVG to the desired output location
            string finalSvgPath = "output.svg";
            File.Copy(tempSvgPath, finalSvgPath, true);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
