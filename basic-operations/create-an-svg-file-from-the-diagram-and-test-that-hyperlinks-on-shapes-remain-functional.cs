using System;
using System.IO;
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

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // List to store hyperlink addresses found in the diagram.
        System.Collections.Generic.List<string> hyperlinkAddresses = new System.Collections.Generic.List<string>();

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Hyperlinks collection.
                        if (shape.Hyperlinks != null)
                        {
                            // Iterate through each hyperlink.
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Store the address (URL) for later verification.
                                if (link != null && link.Address != null && !string.IsNullOrEmpty(link.Address.Value))
                                {
                                    hyperlinkAddresses.Add(link.Address.Value);
                                }
                            }
                        }
                    }
                }

                // Configure SVG save options.
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                svgOptions.SaveFormat = SaveFileFormat.Svg;   // Explicitly set the format.
                svgOptions.PageIndex = 0;                     // Export the first page.
                // SVGFitToViewPort expects a plain bool, not BOOL.
                svgOptions.SVGFitToViewPort = true;           // Preserve viewbox.

                // Save the diagram as SVG.
                diagram.Save(outputPath, svgOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing or SVG export: {ex.Message}");
            return;
        }

        // Verify that the SVG file was created.
        if (!File.Exists(outputPath))
        {
            Console.Error.WriteLine($"Failed to create SVG file: {outputPath}");
            return;
        }

        // Read the generated SVG content.
        string svgContent;
        try
        {
            svgContent = File.ReadAllText(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading SVG file: {ex.Message}");
            return;
        }

        // Test that each hyperlink address appears in the SVG output.
        bool allLinksPresent = true;
        foreach (string address in hyperlinkAddresses)
        {
            if (!svgContent.Contains(address))
            {
                Console.Error.WriteLine($"Hyperlink address not found in SVG: {address}");
                allLinksPresent = false;
            }
        }

        if (!allLinksPresent)
        {
            Console.Error.WriteLine("One or more hyperlinks were not exported correctly to the SVG file.");
            return;
        }

        // Success message.
        Console.WriteLine($"SVG file created successfully at: {outputPath}");
        Console.WriteLine("All hyperlinks were verified in the SVG output.");
    }
}