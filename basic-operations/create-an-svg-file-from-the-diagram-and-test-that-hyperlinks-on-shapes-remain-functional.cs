using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Collect all hyperlink addresses from the diagram.
        List<string> hyperlinkAddresses = new List<string>();
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Hyperlinks != null)
                {
                    foreach (Hyperlink link in shape.Hyperlinks)
                    {
                        if (link != null && link.Address != null && link.Address.Value != null)
                        {
                            hyperlinkAddresses.Add(link.Address.Value);
                        }
                    }
                }
            }
        }

        // Export the diagram to SVG using SVGSaveOptions.
        try
        {
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            // Export the first page only; adjust as needed.
            svgOptions.PageIndex = 0;
            // Set the format explicitly.
            svgOptions.SaveFormat = SaveFileFormat.Svg;
            // Optional: fit SVG to viewport.
            svgOptions.SVGFitToViewPort = true;
            // Export comments if needed.
            svgOptions.IsExportComments = false;

            diagram.Save(outputPath, svgOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving SVG: {ex.Message}");
            return;
        }

        // Verify that the generated SVG contains the hyperlink addresses.
        string svgContent;
        try
        {
            svgContent = File.ReadAllText(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading generated SVG: {ex.Message}");
            return;
        }

        bool allLinksFound = true;
        foreach (string address in hyperlinkAddresses)
        {
            if (!svgContent.Contains(address))
            {
                Console.Error.WriteLine($"Hyperlink address not found in SVG: {address}");
                allLinksFound = false;
            }
        }

        if (allLinksFound)
        {
            Console.WriteLine("SVG export completed successfully. All hyperlinks are present.");
        }
        else
        {
            Console.Error.WriteLine("SVG export completed, but some hyperlinks are missing.");
        }
    }
}