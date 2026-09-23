using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two Visio files to compare
                string diagramPath1 = @"C:\Visio\DiagramA.vsdx";
                string diagramPath2 = @"C:\Visio\DiagramB.vsdx";

                // Load the diagrams
                Diagram diagram1 = new Diagram(diagramPath1);
                Diagram diagram2 = new Diagram(diagramPath2);

                // Extract OLE objects from each diagram
                var oleMap1 = ExtractOleObjects(diagram1);
                var oleMap2 = ExtractOleObjects(diagram2);

                // Compare the extracted OLE objects and report differences
                Console.WriteLine("=== OLE Object Comparison Report ===");
                CompareOleMaps(oleMap1, oleMap2);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Returns a dictionary where the key is a shape identifier (NameU if available, otherwise ID)
        // and the value is the embedded file type description.
        private static Dictionary<string, string> ExtractOleObjects(Diagram diagram)
        {
            var oleDict = new Dictionary<string, string>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is a foreign (OLE) shape and contains embedded object data
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                    {
                        // Determine a stable identifier for the shape
                        string key = !string.IsNullOrWhiteSpace(shape.NameU) ? shape.NameU : shape.ID.ToString();

                        // Retrieve the source full name which usually contains the file extension or application name
                        string sourceInfo = shape.ForeignData.ObjectSourceFullName ?? "Unknown";

                        // Store the information
                        if (!oleDict.ContainsKey(key))
                        {
                            oleDict.Add(key, sourceInfo);
                        }
                    }
                }
            }

            return oleDict;
        }

        // Compares two OLE dictionaries and writes differences to the console
        private static void CompareOleMaps(Dictionary<string, string> map1, Dictionary<string, string> map2)
        {
            // Check for shapes present in diagram1
            foreach (var kvp in map1)
            {
                string shapeId = kvp.Key;
                string type1 = kvp.Value;

                if (map2.TryGetValue(shapeId, out string type2))
                {
                    if (!string.Equals(type1, type2, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Shape '{shapeId}': Type differs. DiagramA = '{type1}', DiagramB = '{type2}'.");
                    }
                }
                else
                {
                    Console.WriteLine($"Shape '{shapeId}' exists in DiagramA but not in DiagramB. Type = '{type1}'.");
                }
            }

            // Check for shapes present only in diagram2
            foreach (var kvp in map2)
            {
                string shapeId = kvp.Key;
                if (!map1.ContainsKey(shapeId))
                {
                    Console.WriteLine($"Shape '{shapeId}' exists in DiagramB but not in DiagramA. Type = '{kvp.Value}'.");
                }
            }
        }
    }