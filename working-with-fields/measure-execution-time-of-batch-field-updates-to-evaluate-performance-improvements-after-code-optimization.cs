using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Define batch replacements: old text => new text.
            var replacements = new Dictionary<string, string>
            {
                { "OldValue", "NewValue" },
                { "Placeholder", "ActualText" }
            };

            // Start timing the batch field update operation.
            Stopwatch sw = Stopwatch.StartNew();

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over shapes using index to avoid collection modification issues.
                for (int s = 0; s < page.Shapes.Count; s++)
                {
                    Shape shape = page.Shapes[s];

                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process only shapes that contain text fields.
                    if (shape.Fields.Count > 0)
                    {
                        for (int f = 0; f < shape.Fields.Count; f++)
                        {
                            Field field = shape.Fields[f];

                            // Original field value.
                            string original = field.Value.Val ?? string.Empty;
                            string updated = original;

                            // Apply all replacements.
                            foreach (KeyValuePair<string, string> kvp in replacements)
                            {
                                if (!string.IsNullOrEmpty(kvp.Key))
                                    updated = updated.Replace(kvp.Key, kvp.Value);
                            }

                            // Update the field only if a change occurred.
                            if (!updated.Equals(original, StringComparison.Ordinal))
                            {
                                field.Value.Val = updated;
                                // Ensure unit is undefined as per API requirements.
                                field.Value.Ufev.Unit = MeasureConst.Undefined;
                                field.Value.Ufev.F = string.Empty;

                                // Clear formatting (optional, shown for completeness).
                                field.Format.Val = string.Empty;
                                field.Format.Ufev.F = string.Empty;
                                field.Format.Ufev.Unit = MeasureConst.Undefined;
                            }
                        }
                    }
                }
            }

            // Stop timing.
            sw.Stop();
            Console.WriteLine($"Batch field update completed in {sw.ElapsedMilliseconds} ms.");

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}