using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the two Visio documents (replace with actual file paths)
                Diagram oldDiagram = new Diagram("oldDiagram.vsdx");
                Diagram newDiagram = new Diagram("newDiagram.vsdx");

                // Retrieve the first SolutionXML from each document (adjust index/name as needed)
                string oldXml = oldDiagram.SolutionXMLs[0].XmlValue;
                string newXml = newDiagram.SolutionXMLs[0].XmlValue;

                // Perform the comparison
                var result = CompareSolutionXml(oldXml, newXml);

                // Output the differences
                Console.WriteLine("Added Shapes:");
                foreach (var shape in result.Added) Console.WriteLine($"  ID={shape.ID}, NameU={shape.NameU}");

                Console.WriteLine("\nRemoved Shapes:");
                foreach (var shape in result.Removed) Console.WriteLine($"  ID={shape.ID}, NameU={shape.NameU}");

                Console.WriteLine("\nModified Shapes:");
                foreach (var mod in result.Modified)
                    Console.WriteLine($"  ID={mod.ID}, NameU={mod.NameU} (changed)");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Holds shape information extracted from the XML
        private class ShapeInfo
        {
            public string ID { get; set; }
            public string NameU { get; set; }
            public XElement Element { get; set; }
        }

        // Holds the comparison result
        private class ComparisonResult
        {
            public List<ShapeInfo> Added { get; } = new List<ShapeInfo>();
            public List<ShapeInfo> Removed { get; } = new List<ShapeInfo>();
            public List<ShapeInfo> Modified { get; } = new List<ShapeInfo>();
        }

        // Compares two SolutionXML strings and identifies added, removed, and modified shapes
        private static ComparisonResult CompareSolutionXml(string oldXml, string newXml)
        {
            // Parse the XML strings
            XDocument oldDoc = XDocument.Parse(oldXml);
            XDocument newDoc = XDocument.Parse(newXml);

            // Extract shape elements (Visio stores shapes under <Shapes> collection)
            var oldShapes = ExtractShapes(oldDoc);
            var newShapes = ExtractShapes(newDoc);

            // Index shapes by their ID for fast lookup
            var oldDict = oldShapes.ToDictionary(s => s.ID);
            var newDict = newShapes.ToDictionary(s => s.ID);

            var result = new ComparisonResult();

            // Identify added shapes (present in new, not in old)
            foreach (var kvp in newDict)
            {
                if (!oldDict.ContainsKey(kvp.Key))
                    result.Added.Add(kvp.Value);
            }

            // Identify removed shapes (present in old, not in new)
            foreach (var kvp in oldDict)
            {
                if (!newDict.ContainsKey(kvp.Key))
                    result.Removed.Add(kvp.Value);
            }

            // Identify modified shapes (same ID, but differing XML)
            foreach (var kvp in newDict)
            {
                if (oldDict.TryGetValue(kvp.Key, out ShapeInfo oldShape))
                {
                    // Simple comparison: check if the serialized XML differs
                    if (!XNode.DeepEquals(oldShape.Element, kvp.Value.Element))
                        result.Modified.Add(kvp.Value);
                }
            }

            return result;
        }

        // Helper to extract ShapeInfo objects from a Visio SolutionXML document
        private static List<ShapeInfo> ExtractShapes(XDocument doc)
        {
            // Visio shape elements are typically under //Shapes/Shape
            XNamespace ns = doc.Root.GetDefaultNamespace();
            var shapeElements = doc.Descendants(ns + "Shape");

            var list = new List<ShapeInfo>();
            foreach (var elem in shapeElements)
            {
                var idAttr = elem.Attribute("ID");
                var nameUAttr = elem.Attribute("NameU");

                // Skip elements without an ID (unlikely but safe)
                if (idAttr == null) continue;

                list.Add(new ShapeInfo
                {
                    ID = idAttr.Value,
                    NameU = nameUAttr?.Value ?? string.Empty,
                    Element = elem
                });
            }
            return list;
        }
    }