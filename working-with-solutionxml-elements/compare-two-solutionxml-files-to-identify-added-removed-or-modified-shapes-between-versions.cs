using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Simple representation of a shape extracted from SolutionXML
    class ShapeInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
    }

    static void Main(string[] args)
    {
        // Expect exactly two file paths: oldSolutionXml and newSolutionXml
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: Program <oldSolutionXmlPath> <newSolutionXmlPath>");
            return;
        }

        string oldPath = args[0];
        string newPath = args[1];

        // Guard against missing files
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
            // Load both XML documents
            XmlDocument oldDoc = new XmlDocument();
            oldDoc.Load(oldPath);
            XmlDocument newDoc = new XmlDocument();
            newDoc.Load(newPath);

            // Parse shapes from each document
            var oldShapes = ParseShapes(oldDoc);
            var newShapes = ParseShapes(newDoc);

            // Determine added, removed, and modified shapes
            var added = new List<ShapeInfo>();
            var removed = new List<ShapeInfo>();
            var modified = new List<(ShapeInfo OldShape, ShapeInfo NewShape)>();

            // Shapes present in new but not in old -> added
            foreach (var kvp in newShapes)
            {
                if (!oldShapes.ContainsKey(kvp.Key))
                {
                    added.Add(kvp.Value);
                }
                else
                {
                    // Present in both -> check for modifications
                    if (IsShapeModified(oldShapes[kvp.Key], kvp.Value))
                    {
                        modified.Add((oldShapes[kvp.Key], kvp.Value));
                    }
                }
            }

            // Shapes present in old but not in new -> removed
            foreach (var kvp in oldShapes)
            {
                if (!newShapes.ContainsKey(kvp.Key))
                {
                    removed.Add(kvp.Value);
                }
            }

            // Output results
            Console.WriteLine("=== Added Shapes ===");
            foreach (var shape in added)
            {
                Console.WriteLine($"ID: {shape.Id}, Name: {shape.Name}");
            }

            Console.WriteLine("\n=== Removed Shapes ===");
            foreach (var shape in removed)
            {
                Console.WriteLine($"ID: {shape.Id}, Name: {shape.Name}");
            }

            Console.WriteLine("\n=== Modified Shapes ===");
            foreach (var pair in modified)
            {
                Console.WriteLine($"ID: {pair.OldShape.Id}, Name: {pair.OldShape.Name}");
                Console.WriteLine("  Changes:");
                foreach (var attr in GetChangedAttributes(pair.OldShape, pair.NewShape))
                {
                    Console.WriteLine($"    {attr.Key}: '{attr.Value.oldValue}' => '{attr.Value.newValue}'");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors, including Aspose related ones
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Parses all <Shape> elements in the provided XmlDocument
    static Dictionary<string, ShapeInfo> ParseShapes(XmlDocument doc)
    {
        var dict = new Dictionary<string, ShapeInfo>();
        // Select any element named "Shape" regardless of namespace
        XmlNodeList shapeNodes = doc.SelectNodes("//*[local-name()='Shape']");

        foreach (XmlNode node in shapeNodes)
        {
            var shape = new ShapeInfo();

            // Prefer ID attribute; fallback to Name attribute as key
            var idAttr = node.Attributes["ID"] ?? node.Attributes["Id"] ?? node.Attributes["Id"];
            var nameAttr = node.Attributes["Name"] ?? node.Attributes["NameU"];

            shape.Id = idAttr?.Value ?? Guid.NewGuid().ToString(); // generate temporary ID if missing
            shape.Name = nameAttr?.Value ?? string.Empty;

            // Capture all attributes for comparison
            if (node.Attributes != null)
            {
                foreach (XmlAttribute attr in node.Attributes)
                {
                    shape.Attributes[attr.Name] = attr.Value;
                }
            }

            // Use ID as the dictionary key; if ID missing, use Name
            string key = shape.Id;
            if (string.IsNullOrEmpty(key))
                key = shape.Name;

            if (!string.IsNullOrEmpty(key) && !dict.ContainsKey(key))
                dict[key] = shape;
        }

        return dict;
    }

    // Determines if two ShapeInfo objects differ in any attribute (excluding ID and Name)
    static bool IsShapeModified(ShapeInfo oldShape, ShapeInfo newShape)
    {
        // Compare attribute counts first for quick exit
        if (oldShape.Attributes.Count != newShape.Attributes.Count)
            return true;

        foreach (var kvp in oldShape.Attributes)
        {
            if (!newShape.Attributes.TryGetValue(kvp.Key, out string newVal))
                return true;
            if (kvp.Value != newVal)
                return true;
        }

        return false;
    }

    // Returns a dictionary of changed attributes between two shapes
    static Dictionary<string, (string oldValue, string newValue)> GetChangedAttributes(ShapeInfo oldShape, ShapeInfo newShape)
    {
        var changes = new Dictionary<string, (string, string)>();
        foreach (var kvp in oldShape.Attributes)
        {
            if (newShape.Attributes.TryGetValue(kvp.Key, out string newVal))
            {
                if (kvp.Value != newVal)
                {
                    changes[kvp.Key] = (kvp.Value, newVal);
                }
            }
            else
            {
                changes[kvp.Key] = (kvp.Value, "<missing>");
            }
        }

        // Detect new attributes added in the new shape
        foreach (var kvp in newShape.Attributes)
        {
            if (!oldShape.Attributes.ContainsKey(kvp.Key))
            {
                changes[kvp.Key] = ("<missing>", kvp.Value);
            }
        }

        return changes;
    }
}