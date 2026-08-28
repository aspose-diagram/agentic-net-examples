using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

public class VisioActiveXComparer
{
    // Represents a difference found between two matching shapes
    public class Difference
    {
        public string ShapeName { get; set; }
        public string PropertyName { get; set; }
        public string ValueInFirst { get; set; }
        public string ValueInSecond { get; set; }

        public override string ToString()
        {
            return $"Shape: {ShapeName}, Property: {PropertyName}, First: {ValueInFirst}, Second: {ValueInSecond}";
        }
    }

    // Compares two Visio files and returns a list of ActiveX control property differences
    public static List<Difference> CompareActiveXControls(string firstVisioPath, string secondVisioPath)
    {
        var differences = new List<Difference>();

        // Load the two diagrams using the provided constructors (lifecycle rule)
        using (var diagram1 = new Diagram(firstVisioPath))
        using (var diagram2 = new Diagram(secondVisioPath))
        {
            // Build a lookup of shapes from the second diagram by universal name (NameU)
            var secondShapeLookup = new Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
            foreach (Page page in diagram2.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (!string.IsNullOrEmpty(shape.NameU))
                    {
                        secondShapeLookup[shape.NameU] = shape;
                    }
                }
            }

            // Iterate through shapes in the first diagram
            foreach (Page page in diagram1.Pages)
            {
                foreach (Shape shape1 in page.Shapes)
                {
                    // Find matching shape in the second diagram
                    if (string.IsNullOrEmpty(shape1.NameU) || !secondShapeLookup.TryGetValue(shape1.NameU, out Shape shape2))
                        continue; // No matching shape; skip

                    // Both shapes must contain an ActiveX control
                    var ax1 = shape1.ActiveXControl;
                    var ax2 = shape2.ActiveXControl;
                    if (ax1 == null && ax2 == null)
                        continue; // Neither has ActiveX; nothing to compare
                    if (ax1 == null || ax2 == null)
                    {
                        differences.Add(new Difference
                        {
                            ShapeName = shape1.NameU,
                            PropertyName = "ActiveXControlPresence",
                            ValueInFirst = ax1 != null ? "Present" : "Absent",
                            ValueInSecond = ax2 != null ? "Present" : "Absent"
                        });
                        continue;
                    }

                    // Compare property values of the ActiveX controls.
                    // The ActiveXControl object exposes a collection of Property elements.
                    // Since the exact API is not detailed, we use reflection to enumerate public properties.
                    var props1 = ax1.GetType().GetProperties();
                    var props2 = ax2.GetType().GetProperties();

                    // Build a lookup for second control's properties for quick access
                    var secondPropLookup = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    foreach (var p in props2)
                    {
                        try { secondPropLookup[p.Name] = p.GetValue(ax2); }
                        catch { /* ignore inaccessible properties */ }
                    }

                    foreach (var p1 in props1)
                    {
                        object val1 = null;
                        try { val1 = p1.GetValue(ax1); } catch { /* ignore */ }

                        if (!secondPropLookup.TryGetValue(p1.Name, out object val2))
                        {
                            // Property exists only in first diagram
                            differences.Add(new Difference
                            {
                                ShapeName = shape1.NameU,
                                PropertyName = p1.Name,
                                ValueInFirst = val1?.ToString() ?? "null",
                                ValueInSecond = "Property missing"
                            });
                            continue;
                        }

                        // Compare values (using string representation for simplicity)
                        string str1 = val1?.ToString() ?? "null";
                        string str2 = val2?.ToString() ?? "null";

                        if (!string.Equals(str1, str2, StringComparison.Ordinal))
                        {
                            differences.Add(new Difference
                            {
                                ShapeName = shape1.NameU,
                                PropertyName = p1.Name,
                                ValueInFirst = str1,
                                ValueInSecond = str2
                            });
                        }
                    }

                    // Detect properties present only in the second control
                    foreach (var p2 in props2)
                    {
                        if (!secondPropLookup.ContainsKey(p2.Name))
                            continue; // already processed
                        if (!Array.Exists(props1, p => p.Name.Equals(p2.Name, StringComparison.OrdinalIgnoreCase)))
                        {
                            object val2 = null;
                            try { val2 = p2.GetValue(ax2); } catch { /* ignore */ }

                            differences.Add(new Difference
                            {
                                ShapeName = shape1.NameU,
                                PropertyName = p2.Name,
                                ValueInFirst = "Property missing",
                                ValueInSecond = val2?.ToString() ?? "null"
                            });
                        }
                    }
                }
            }
        }

        return differences;
    }

    // Example usage
    public static void Main()
    {
        try
        {

            string file1 = @"C:\Diagrams\DiagramA.vsdx";
            string file2 = @"C:\Diagrams\DiagramB.vsdx";

            List<Difference> diffs = CompareActiveXControls(file1, file2);

            Console.WriteLine($"Found {diffs.Count} differences in ActiveX control properties:");
            foreach (var diff in diffs)
            {
                Console.WriteLine(diff);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
