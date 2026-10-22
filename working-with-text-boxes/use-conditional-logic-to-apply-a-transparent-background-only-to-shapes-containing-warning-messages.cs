using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // First pass: save to a memory stream to collect warnings
            var warningCollector = new WarningCollector();

            // Use PdfSaveOptions (any SaveOptions works for warning collection)
            PdfSaveOptions warningOptions = new PdfSaveOptions();
            warningOptions.WarningCallback = warningCollector;

            using (var ms = new MemoryStream())
            {
                diagram.Save(ms, warningOptions);
            }

            // Apply transparent background to shapes that generated warnings
            foreach (int shapeId in warningCollector.ShapeIds)
            {
                // Iterate through all pages to find the shape by ID
                foreach (Page page in diagram.Pages)
                {
                    // GetShape expects an int ID
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        // Ensure the shape has a Fill object before setting transparency
                        if (shape.Fill != null)
                        {
                            // Set foreground fill transparency to fully transparent (1.0)
                            shape.Fill.FillForegndTrans.Value = 1.0;
                            // Optionally set a solid fill pattern to make the change effective
                            shape.Fill.FillPattern.Value = 1; // Solid fill
                        }
                    }
                }
            }

            // Save the modified diagram to the desired output format (VSDX)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}

/// <summary>
/// Implements IWarningCallback to collect shape IDs that generated warnings during save.
/// </summary>
class WarningCollector : IWarningCallback
{
    // List to store shape IDs extracted from warning descriptions
    public List<int> ShapeIds { get; } = new List<int>();

    public void Warning(WarningInfo warning)
    {
        // Attempt to extract a shape ID from the warning description.
        // Expected format example: "Shape ID: 5 - Font not found..."
        if (warning != null && !string.IsNullOrEmpty(warning.Description))
        {
            string desc = warning.Description;
            const string marker = "Shape ID:";
            int idx = desc.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
            if (idx >= 0)
            {
                int start = idx + marker.Length;
                // Find the next non-digit character
                int end = start;
                while (end < desc.Length && char.IsDigit(desc[end]))
                {
                    end++;
                }

                string idStr = desc.Substring(start, end - start).Trim();
                if (int.TryParse(idStr, out int shapeId))
                {
                    // Avoid duplicates
                    if (!ShapeIds.Contains(shapeId))
                    {
                        ShapeIds.Add(shapeId);
                    }
                }
            }
        }
    }
}