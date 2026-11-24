using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <oldSolutionXmlPath> <newSolutionXmlPath>");
            return;
        }

        string oldPath = args[0];
        string newPath = args[1];

        // Guard against missing files.
        if (!File.Exists(oldPath))
        {
            Console.Error.WriteLine($"File not found: {oldPath}");
            return;
        }
        if (!File.Exists(newPath))
        {
            Console.Error.WriteLine($"File not found: {newPath}");
            return;
        }

        try
        {
            // Load both SolutionXML files.
            XDocument oldDoc = XDocument.Load(oldPath);
            XDocument newDoc = XDocument.Load(newPath);

            // Extract shape elements. Assumes each shape is represented by a <Shape> element
            // with an "ID" attribute (or "Name" if ID is missing).
            var oldShapes = ExtractShapes(oldDoc);
            var newShapes = ExtractShapes(newDoc);

            // Determine added shapes (present only in new).
            var added = new List<XElement>();
            foreach (var kvp in newShapes)
            {
                if (!oldShapes.ContainsKey(kvp.Key))
                    added.Add(kvp.Value);
            }

            // Determine removed shapes (present only in old).
            var removed = new List<XElement>();
            foreach (var kvp in oldShapes)
            {
                if (!newShapes.ContainsKey(kvp.Key))
                    removed.Add(kvp.Value);
            }

            // Determine modified shapes (present in both but content differs).
            var modified = new List<(XElement oldShape, XElement newShape)>();
            foreach (var kvp in oldShapes)
            {
                if (newShapes.TryGetValue(kvp.Key, out XElement newShape))
                {
                    // Compare the XML representation of the shape.
                    if (!XNode.DeepEquals(kvp.Value, newShape))
                        modified.Add((kvp.Value, newShape));
                }
            }

            // Output results.
            Console.WriteLine("=== Added Shapes ===");
            foreach (var shape in added)
                Console.WriteLine(ShapeSummary(shape));

            Console.WriteLine("\n=== Removed Shapes ===");
            foreach (var shape in removed)
                Console.WriteLine(ShapeSummary(shape));

            Console.WriteLine("\n=== Modified Shapes ===");
            foreach (var pair in modified)
            {
                Console.WriteLine("Old: " + ShapeSummary(pair.oldShape));
                Console.WriteLine("New: " + ShapeSummary(pair.newShape));
                Console.WriteLine("---");
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected errors.
            Console.Error.WriteLine($"Error processing files: {ex.Message}");
        }
    }

    // Helper to extract shapes into a dictionary keyed by ID (or Name if ID missing).
    private static Dictionary<string, XElement> ExtractShapes(XDocument doc)
    {
        var dict = new Dictionary<string, XElement>();
        foreach (XElement shape in doc.Descendants("Shape"))
        {
            // Prefer the "ID" attribute; fall back to "Name" if ID is absent.
            XAttribute idAttr = shape.Attribute("ID");
            XAttribute nameAttr = shape.Attribute("Name");
            string key = idAttr != null ? idAttr.Value : (nameAttr != null ? nameAttr.Value : Guid.NewGuid().ToString());

            // Ensure unique keys; if duplicate, append a suffix.
            if (dict.ContainsKey(key))
                key = key + "_" + Guid.NewGuid().ToString();

            dict[key] = shape;
        }
        return dict;
    }

    // Helper to create a concise description of a shape for console output.
    private static string ShapeSummary(XElement shape)
    {
        string id = shape.Attribute("ID")?.Value ?? "N/A";
        string name = shape.Attribute("Name")?.Value ?? "N/A";
        string type = shape.Attribute("Type")?.Value ?? "N/A";
        return $"Shape ID: {id}, Name: {name}, Type: {type}";
    }
}