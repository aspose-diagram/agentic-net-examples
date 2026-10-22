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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputRoot = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output root folder exists (create if necessary).
        try
        {
            if (!Directory.Exists(outputRoot))
                Directory.CreateDirectory(outputRoot);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputRoot}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Counter to generate fallback page folder names.
                int pageCounter = 1;

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Determine a safe folder name for the page.
                    string pageFolderName;
                    if (!string.IsNullOrWhiteSpace(page.NameU))
                    {
                        pageFolderName = page.NameU;
                    }
                    else
                    {
                        pageFolderName = $"Page_{pageCounter}";
                    }

                    // Combine with the root output path and create the directory.
                    string pageFolderPath = Path.Combine(outputRoot, pageFolderName);
                    try
                    {
                        Directory.CreateDirectory(pageFolderPath);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to create page folder '{pageFolderPath}': {ex.Message}");
                        // Continue to next page if folder creation fails.
                        pageCounter++;
                        continue;
                    }

                    // Prepare SVG save options (reuse for all shapes).
                    SVGSaveOptions svgOptions = new SVGSaveOptions();

                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a filename for the shape SVG using its unique ID.
                        string shapeFileName = $"Shape_{shape.ID}.svg";
                        string shapeFilePath = Path.Combine(pageFolderPath, shapeFileName);

                        try
                        {
                            // Export the shape to an SVG file.
                            shape.ToSvg(shapeFilePath, svgOptions);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID} on page '{pageFolderName}': {ex.Message}");
                        }
                    }

                    pageCounter++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}