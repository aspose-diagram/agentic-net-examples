using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace SolutionXmlComparer
{
    // Simple DTO to hold shape information for comparison
    public class ShapeInfo
    {
        public long Id { get; set; }
        public int PageIndex { get; set; }
        public string NameU { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }

    public class Program
    {
        // Entry point
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: SolutionXmlComparer <oldDiagramPath> <newDiagramPath>");
                return;
            }

            string oldDiagramPath = args[0];
            string newDiagramPath = args[1];

            // Load the two diagrams
            Diagram oldDiagram = new Diagram(oldDiagramPath);
            Diagram newDiagram = new Diagram(newDiagramPath);

            // Extract shape information from both diagrams
            var oldShapes = ExtractShapes(oldDiagram);
            var newShapes = ExtractShapes(newDiagram);

            // Identify removed shapes (present in old, missing in new)
            foreach (var kvp in oldShapes)
            {
                if (!newShapes.ContainsKey(kvp.Key))
                {
                    Console.WriteLine($"Removed Shape: NameU='{kvp.Value.NameU}', ID={kvp.Value.Id}, Page={kvp.Value.PageIndex}");
                }
            }

            // Identify added shapes (present in new, missing in old)
            foreach (var kvp in newShapes)
            {
                if (!oldShapes.ContainsKey(kvp.Key))
                {
                    Console.WriteLine($"Added Shape: NameU='{kvp.Value.NameU}', ID={kvp.Value.Id}, Page={kvp.Value.PageIndex}");
                }
            }

            // Identify modified shapes (same NameU exists in both but properties differ)
            foreach (var kvp in oldShapes)
            {
                if (newShapes.TryGetValue(kvp.Key, out ShapeInfo newInfo))
                {
                    ShapeInfo oldInfo = kvp.Value;
                    bool nameChanged = !string.Equals(oldInfo.NameU, newInfo.NameU, StringComparison.Ordinal);
                    bool textChanged = !string.Equals(oldInfo.Text, newInfo.Text, StringComparison.Ordinal);

                    if (nameChanged || textChanged)
                    {
                        Console.WriteLine($"Modified Shape: NameU='{oldInfo.NameU}' (ID={oldInfo.Id})");
                        if (nameChanged)
                        {
                            Console.WriteLine($"  NameU changed from '{oldInfo.NameU}' to '{newInfo.NameU}'");
                        }
                        if (textChanged)
                        {
                            Console.WriteLine($"  Text changed from '{oldInfo.Text}' to '{newInfo.Text}'");
                        }
                    }
                }
            }
        }

        // Helper method to extract shapes from a diagram into a dictionary keyed by NameU (or ID if NameU is empty)
        private static Dictionary<string, ShapeInfo> ExtractShapes(Diagram diagram)
        {
            var dict = new Dictionary<string, ShapeInfo>(StringComparer.Ordinal);
            for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
            {
                Page page = diagram.Pages[pageIdx];
                foreach (Shape shape in page.Shapes)
                {
                    // Use NameU as primary key; fallback to ID string if NameU is empty
                    string key = !string.IsNullOrEmpty(shape.NameU) ? shape.NameU : shape.ID.ToString();

                    // Retrieve plain text from the shape
                    string plainText = shape.Text?.Value?.Text ?? string.Empty;

                    var info = new ShapeInfo
                    {
                        Id = shape.ID,
                        PageIndex = pageIdx,
                        NameU = shape.NameU ?? string.Empty,
                        Text = plainText
                    };

                    // If duplicate keys occur, keep the first occurrence (unlikely for NameU)
                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(key, info);
                    }
                }
            }
            return dict;
        }
    }
}