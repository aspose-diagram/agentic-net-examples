using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        // Retrieves OLE objects from a diagram.
        // Returns a list of tuples: Shape ID, Shape NameU, and detected file type (extension or source name).
        static List<(long ShapeId, string ShapeName, string FileType)> GetOleObjects(Diagram diagram)
        {
            var oleList = new List<(long, string, string)>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is an OLE object.
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // ObjectSourceFullName may contain a file name or application identifier.
                        string source = shape.ForeignData.ObjectSourceFullName ?? string.Empty;

                        // Attempt to extract a file extension; if none, use the raw source string.
                        string fileType = Path.GetExtension(source);
                        if (string.IsNullOrEmpty(fileType))
                        {
                            fileType = source;
                        }

                        oleList.Add((shape.ID, shape.NameU ?? string.Empty, fileType));
                    }
                }
            }

            return oleList;
        }

        static void Main(string[] args)
        {
            // Expect two file paths as command‑line arguments.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: OleComparison <DiagramPath1> <DiagramPath2>");
                return;
            }

            string diagramPath1 = args[0];
            string diagramPath2 = args[1];

            // Load the two Visio diagrams.
            Diagram diagram1 = new Diagram(diagramPath1);
            Diagram diagram2 = new Diagram(diagramPath2);

            // Extract OLE information from each diagram.
            List<(long ShapeId, string ShapeName, string FileType)> oleObjects1 = GetOleObjects(diagram1);
            List<(long ShapeId, string ShapeName, string FileType)> oleObjects2 = GetOleObjects(diagram2);

            // Display OLE objects found in the first diagram.
            Console.WriteLine("OLE objects in first diagram:");
            foreach (var item in oleObjects1)
            {
                Console.WriteLine($"  Shape ID: {item.ShapeId}, Name: {item.ShapeName}, Type: {item.FileType}");
            }

            // Display OLE objects found in the second diagram.
            Console.WriteLine("\nOLE objects in second diagram:");
            foreach (var item in oleObjects2)
            {
                Console.WriteLine($"  Shape ID: {item.ShapeId}, Name: {item.ShapeName}, Type: {item.FileType}");
            }

            // Build sets of file types for comparison.
            HashSet<string> types1 = new HashSet<string>(oleObjects1.Select(o => o.FileType), StringComparer.OrdinalIgnoreCase);
            HashSet<string> types2 = new HashSet<string>(oleObjects2.Select(o => o.FileType), StringComparer.OrdinalIgnoreCase);

            // Determine differences.
            IEnumerable<string> onlyInFirst = types1.Except(types2);
            IEnumerable<string> onlyInSecond = types2.Except(types1);

            Console.WriteLine("\nDifferences in embedded OLE file types:");
            if (onlyInFirst.Any())
            {
                Console.WriteLine("  Types only in first diagram:");
                foreach (string t in onlyInFirst)
                {
                    Console.WriteLine($"    {t}");
                }
            }
            else
            {
                Console.WriteLine("  No unique types in first diagram.");
            }

            if (onlyInSecond.Any())
            {
                Console.WriteLine("  Types only in second diagram:");
                foreach (string t in onlyInSecond)
                {
                    Console.WriteLine($"    {t}");
                }
            }
            else
            {
                Console.WriteLine("  No unique types in second diagram.");
            }

            // Optional: report matching types with different counts.
            var commonTypes = types1.Intersect(types2);
            foreach (string type in commonTypes)
            {
                int count1 = oleObjects1.Count(o => string.Equals(o.FileType, type, StringComparison.OrdinalIgnoreCase));
                int count2 = oleObjects2.Count(o => string.Equals(o.FileType, type, StringComparison.OrdinalIgnoreCase));
                if (count1 != count2)
                {
                    Console.WriteLine($"  Type '{type}' count differs: first diagram = {count1}, second diagram = {count2}");
                }
            }
        }
    }