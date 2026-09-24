using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure input and output arguments are provided.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputPdfPath>");
            return;
        }

        // Input Visio file path.
        string inputPath = args[0];
        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path.
        string outputPath = args[1];
        // Guard: ensure the directory for the output exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
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

        bool validationFailed = false;

        // Iterate through all pages.
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes.
                if (shape.Del == BOOL.True) continue;

                // Process only shapes that have a master (i.e., inherit formatting).
                if (shape.Master == null) continue;

                // Ensure gradient fill objects are present.
                GradientFill shapeGrad = shape.Fill?.GradientFill;
                GradientFill inheritGrad = shape.InheritFill?.GradientFill;
                if (shapeGrad == null || inheritGrad == null) continue;

                // Compare gradient enabled flag.
                if (shapeGrad.GradientEnabled?.Value != inheritGrad.GradientEnabled?.Value)
                {
                    Console.Error.WriteLine($"Gradient enabled mismatch on shape ID {shape.ID} (Page {page.ID}).");
                    validationFailed = true;
                    continue;
                }

                // If gradient is not enabled, no further checks needed.
                if (shapeGrad.GradientEnabled?.Value != BOOL.True) continue;

                // Compare gradient direction.
                if (shapeGrad.GradientDir?.Value != inheritGrad.GradientDir?.Value)
                {
                    Console.Error.WriteLine($"Gradient direction mismatch on shape ID {shape.ID} (Page {page.ID}).");
                    validationFailed = true;
                }

                // Compare number of gradient stops.
                int shapeStopCount = shapeGrad.GradientStops?.Count ?? 0;
                int inheritStopCount = inheritGrad.GradientStops?.Count ?? 0;
                if (shapeStopCount != inheritStopCount)
                {
                    Console.Error.WriteLine($"Gradient stop count mismatch on shape ID {shape.ID} (Page {page.ID}).");
                    validationFailed = true;
                    continue;
                }

                // Compare each gradient stop's position and color.
                for (int i = 0; i < shapeStopCount; i++)
                {
                    GradientStop shapeStop = shapeGrad.GradientStops[i];
                    GradientStop inheritStop = inheritGrad.GradientStops[i];

                    // Position comparison (double values).
                    if (Math.Abs(shapeStop.Position?.Value - inheritStop.Position?.Value ?? 0) > 1e-6)
                    {
                        Console.Error.WriteLine($"Gradient stop position mismatch on shape ID {shape.ID}, stop {i} (Page {page.ID}).");
                        validationFailed = true;
                    }

                    // Color comparison (hex strings, case-insensitive).
                    string shapeColor = shapeStop.Color?.Value?.Trim().ToUpperInvariant() ?? "";
                    string inheritColor = inheritStop.Color?.Value?.Trim().ToUpperInvariant() ?? "";
                    if (shapeColor != inheritColor)
                    {
                        Console.Error.WriteLine($"Gradient stop color mismatch on shape ID {shape.ID}, stop {i} (Page {page.ID}).");
                        validationFailed = true;
                    }
                }
            }
        }

        if (validationFailed)
        {
            Console.Error.WriteLine("Validation failed: some shapes do not correctly inherit the master's gradient.");
        }
        else
        {
            Console.WriteLine("All shapes correctly inherit the master's gradient.");
        }

        // Export the diagram to PDF.
        try
        {
            // Configure PDF save options (default font fallback).
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                DefaultFont = "Arial"
            };

            // Save the diagram as PDF.
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Diagram exported to PDF successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error exporting to PDF: {ex.Message}");
        }
    }
}