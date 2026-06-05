using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two file paths as command‑line arguments
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: OleComparison <DiagramPath1> <DiagramPath2>");
                return;
            }

            string path1 = args[0];
            string path2 = args[1];

            // Load the two Visio diagrams
            Diagram diagram1 = new Diagram(path1);
            Diagram diagram2 = new Diagram(path2);

            // Extract OLE object information from each diagram
            var oleMap1 = GetOleObjects(diagram1);
            var oleMap2 = GetOleObjects(diagram2);

            // Compare OLE objects present in both diagrams
            foreach (var kvp in oleMap1)
            {
                long shapeId = kvp.Key;
                string ext1 = kvp.Value;

                if (oleMap2.TryGetValue(shapeId, out string ext2))
                {
                    if (!string.Equals(ext1, ext2, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Shape ID {shapeId}: Different embedded file types. Diagram1 = \"{ext1}\", Diagram2 = \"{ext2}\"");
                    }
                }
                else
                {
                    Console.WriteLine($"Shape ID {shapeId}: OLE object present only in Diagram1 (type \"{ext1}\")");
                }
            }

            // Report OLE objects that exist only in Diagram2
            foreach (var kvp in oleMap2)
            {
                if (!oleMap1.ContainsKey(kvp.Key))
                {
                    Console.WriteLine($"Shape ID {kvp.Key}: OLE object present only in Diagram2 (type \"{kvp.Value}\")");
                }
            }
        }

        /// <summary>
        /// Retrieves a map of shape IDs to the file extension of embedded OLE objects.
        /// Only shapes that are foreign OLE objects with valid ObjectData are considered.
        /// </summary>
        private static Dictionary<long, string> GetOleObjects(Diagram diagram)
        {
            var result = new Dictionary<long, string>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is an OLE foreign object
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // Ensure there is binary data to examine
                        if (shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                        {
                            string source = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                            string extension = string.Empty;

                            if (!string.IsNullOrEmpty(source))
                            {
                                extension = Path.GetExtension(source).ToLowerInvariant();
                            }

                            result[shape.ID] = extension;
                        }
                    }
                }
            }

            return result;
        }
    }