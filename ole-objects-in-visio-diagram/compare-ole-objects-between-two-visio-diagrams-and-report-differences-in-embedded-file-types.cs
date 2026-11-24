using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFile1> <VisioFile2>");
            return;
        }

        string filePath1 = args[0];
        string filePath2 = args[1];

        // Guard against missing input files.
        if (!File.Exists(filePath1))
        {
            Console.Error.WriteLine($"Error: File not found – {filePath1}");
            return;
        }
        if (!File.Exists(filePath2))
        {
            Console.Error.WriteLine($"Error: File not found – {filePath2}");
            return;
        }

        try
        {
            // Load the two Visio diagrams.
            Diagram diagram1 = new Diagram(filePath1);
            Diagram diagram2 = new Diagram(filePath2);

            // Extract OLE object information from each diagram.
            var oleMap1 = ExtractOleObjects(diagram1);
            var oleMap2 = ExtractOleObjects(diagram2);

            // Build a set of all keys (pageIndex_shapeId) present in either diagram.
            var allKeys = new HashSet<string>(oleMap1.Keys);
            allKeys.UnionWith(oleMap2.Keys);

            // Report differences.
            Console.WriteLine("OLE Object Comparison Report:");
            foreach (var key in allKeys)
            {
                bool inFirst = oleMap1.TryGetValue(key, out string source1);
                bool inSecond = oleMap2.TryGetValue(key, out string source2);

                if (inFirst && inSecond)
                {
                    if (!string.Equals(source1, source2, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"- Different OLE type at Page/Shape {KeyToReadable(key)}:");
                        Console.WriteLine($"    Diagram 1: {source1}");
                        Console.WriteLine($"    Diagram 2: {source2}");
                    }
                }
                else if (inFirst && !inSecond)
                {
                    Console.WriteLine($"- OLE object present only in Diagram 1 at {KeyToReadable(key)}: {source1}");
                }
                else if (!inFirst && inSecond)
                {
                    Console.WriteLine($"- OLE object present only in Diagram 2 at {KeyToReadable(key)}: {source2}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Extracts OLE objects from a diagram and returns a dictionary keyed by "pageIndex_shapeId".
    private static Dictionary<string, string> ExtractOleObjects(Diagram diagram)
    {
        var result = new Dictionary<string, string>();

        for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
        {
            Page page = diagram.Pages[pageIndex];
            foreach (Shape shape in page.Shapes)
            {
                // OLE objects are represented by non‑null ForeignData.
                if (shape.ForeignData != null)
                {
                    // ObjectSourceFullName may contain the embedded file type or application name.
                    string source = shape.ForeignData.ObjectSourceFullName ?? "Unknown";

                    // Use shape.ID as a stable identifier within the page.
                    string key = $"{pageIndex}_{shape.ID}";
                    result[key] = source;
                }
            }
        }

        return result;
    }

    // Converts the dictionary key back to a readable "Page X, Shape ID Y" format.
    private static string KeyToReadable(string key)
    {
        var parts = key.Split('_');
        if (parts.Length == 2 &&
            int.TryParse(parts[0], out int pageIdx) &&
            int.TryParse(parts[1], out int shapeId))
        {
            return $"Page {pageIdx + 1}, Shape ID {shapeId}";
        }
        return key;
    }
}