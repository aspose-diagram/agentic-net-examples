using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect exactly two arguments: paths to the two Visio files.
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <diagram1.vsdx> <diagram2.vsdx>");
            return;
        }

        string diagramPath1 = args[0];
        string diagramPath2 = args[1];

        // Guard: ensure both files exist.
        if (!File.Exists(diagramPath1))
        {
            Console.Error.WriteLine($"File not found: {diagramPath1}");
            return;
        }
        if (!File.Exists(diagramPath2))
        {
            Console.Error.WriteLine($"File not found: {diagramPath2}");
            return;
        }

        Diagram diagram1 = null;
        Diagram diagram2 = null;

        try
        {
            // Load the first diagram.
            diagram1 = new Diagram(diagramPath1);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram 1: {ex.Message}");
            return;
        }

        try
        {
            // Load the second diagram.
            diagram2 = new Diagram(diagramPath2);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram 2: {ex.Message}");
            return;
        }

        // Extract OLE object file extensions from each diagram.
        HashSet<string> oleExtensions1 = ExtractOleExtensions(diagram1);
        HashSet<string> oleExtensions2 = ExtractOleExtensions(diagram2);

        // Determine differences.
        var onlyInFirst = new List<string>();
        var onlyInSecond = new List<string>();

        foreach (var ext in oleExtensions1)
        {
            if (!oleExtensions2.Contains(ext))
                onlyInFirst.Add(ext);
        }

        foreach (var ext in oleExtensions2)
        {
            if (!oleExtensions1.Contains(ext))
                onlyInSecond.Add(ext);
        }

        // Report results.
        Console.WriteLine("=== OLE Object File Type Comparison ===");
        Console.WriteLine($"Diagram 1: {diagramPath1}");
        Console.WriteLine($"Diagram 2: {diagramPath2}");
        Console.WriteLine();

        Console.WriteLine("File types present in Diagram 1:");
        foreach (var ext in oleExtensions1)
            Console.WriteLine($"  {ext}");
        Console.WriteLine();

        Console.WriteLine("File types present in Diagram 2:");
        foreach (var ext in oleExtensions2)
            Console.WriteLine($"  {ext}");
        Console.WriteLine();

        if (onlyInFirst.Count == 0 && onlyInSecond.Count == 0)
        {
            Console.WriteLine("Both diagrams contain the same set of OLE object file types.");
        }
        else
        {
            if (onlyInFirst.Count > 0)
            {
                Console.WriteLine("File types only in Diagram 1:");
                foreach (var ext in onlyInFirst)
                    Console.WriteLine($"  {ext}");
            }

            if (onlyInSecond.Count > 0)
            {
                Console.WriteLine("File types only in Diagram 2:");
                foreach (var ext in onlyInSecond)
                    Console.WriteLine($"  {ext}");
            }
        }
    }

    /// <summary>
    /// Extracts a set of file extensions from OLE objects embedded in the given diagram.
    /// </summary>
    /// <param name="diagram">The diagram to inspect.</param>
    /// <returns>A HashSet containing lower‑cased extensions (including the leading dot).</returns>
    private static HashSet<string> ExtractOleExtensions(Diagram diagram)
    {
        var extensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape contains foreign (OLE) data.
                    if (shape.ForeignData != null && shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                    {
                        // Attempt to get the source file name of the OLE object.
                        string sourceFullName = shape.ForeignData.ObjectSourceFullName;

                        if (!string.IsNullOrEmpty(sourceFullName))
                        {
                            string ext = Path.GetExtension(sourceFullName);
                            if (!string.IsNullOrEmpty(ext))
                                extensions.Add(ext.ToLowerInvariant());
                        }
                        else
                        {
                            // If source name is unavailable, we can still attempt to infer type from the binary data.
                            // For this example we record it as "<unknown>".
                            extensions.Add("<unknown>");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while extracting OLE extensions: {ex.Message}");
        }

        return extensions;
    }
}