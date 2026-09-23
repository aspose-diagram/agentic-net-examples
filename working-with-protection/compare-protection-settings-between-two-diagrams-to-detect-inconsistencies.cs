using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths as arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramProtectionComparer <DiagramPath1> <DiagramPath2>");
            return;
        }

        string path1 = args[0];
        // Guard: ensure first file exists
        if (!File.Exists(path1))
        {
            Console.Error.WriteLine($"File not found: {path1}");
            return;
        }

        string path2 = args[1];
        // Guard: ensure second file exists
        if (!File.Exists(path2))
        {
            Console.Error.WriteLine($"File not found: {path2}");
            return;
        }

        Diagram diagram1;
        Diagram diagram2;
        try
        {
            // Load the two diagrams (may throw if file is invalid)
            diagram1 = new Diagram(path1);
            diagram2 = new Diagram(path2);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagrams: {ex.Message}");
            return;
        }

        // Compare global document protection settings
        CompareDocumentProtection(diagram1, diagram2);

        // Compare shape‑level protection settings
        CompareShapeProtection(diagram1, diagram2);
    }

    private static void CompareDocumentProtection(Diagram d1, Diagram d2)
    {
        // DocumentSettings protection properties are of type BOOL, not BoolValue
        var protections = new (string Name, BOOL D1, BOOL D2)[]
        {
            ("ProtectBkgnds", d1.DocumentSettings.ProtectBkgnds, d2.DocumentSettings.ProtectBkgnds),
            ("ProtectMasters", d1.DocumentSettings.ProtectMasters, d2.DocumentSettings.ProtectMasters),
            ("ProtectShapes",  d1.DocumentSettings.ProtectShapes,  d2.DocumentSettings.ProtectShapes),
            ("ProtectStyles",  d1.DocumentSettings.ProtectStyles,  d2.DocumentSettings.ProtectStyles)
        };

        foreach (var (name, val1, val2) in protections)
        {
            if (val1 != val2)
            {
                Console.WriteLine($"Document protection mismatch: {name} - Diagram1={val1}, Diagram2={val2}");
            }
        }
    }

    private static void CompareShapeProtection(Diagram d1, Diagram d2)
    {
        // Build a lookup for shapes in diagram2: key = pageId|shapeId
        var shapeMap2 = new System.Collections.Generic.Dictionary<string, Shape>();
        foreach (Page page in d2.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                string key = $"{page.ID}_{shape.ID}";
                shapeMap2[key] = shape;
            }
        }

        // List of protection properties to compare (all are BoolValue cells)
        string[] protectionProps = new string[]
        {
            "LockMoveX", "LockMoveY", "LockWidth", "LockHeight", "LockRotate",
            "LockVtxEdit", "LockAspect", "LockBegin", "LockEnd", "LockDelete",
            "LockFormat", "LockFromGroupFormat", "LockGroup", "LockSelect",
            "LockTextEdit", "LockThemeColors", "LockThemeEffects", "LockThemeFonts",
            "LockThemeIndex", "LockCustProp", "LockCalcWH", "LockCrop"
        };

        foreach (Page page1 in d1.Pages)
        {
            foreach (Shape shape1 in page1.Shapes)
            {
                string key = $"{page1.ID}_{shape1.ID}";
                if (!shapeMap2.TryGetValue(key, out Shape shape2))
                {
                    Console.WriteLine($"Shape missing in Diagram2: PageID={page1.ID}, ShapeID={shape1.ID}");
                    continue;
                }

                foreach (string propName in protectionProps)
                {
                    // Use reflection to get the BoolValue property from shape.Protection
                    var propInfo = typeof(Protection).GetProperty(propName);
                    if (propInfo == null) continue; // safety

                    var bv1 = propInfo.GetValue(shape1.Protection) as BoolValue;
                    var bv2 = propInfo.GetValue(shape2.Protection) as BoolValue;

                    BOOL v1 = bv1?.Value ?? BOOL.False;
                    BOOL v2 = bv2?.Value ?? BOOL.False;

                    if (v1 != v2)
                    {
                        Console.WriteLine($"Protection mismatch on PageID={page1.ID}, ShapeID={shape1.ID}, Property={propName}: Diagram1={v1}, Diagram2={v2}");
                    }
                }
            }
        }

        // Detect shapes present in Diagram2 but not in Diagram1
        var shapeSet1 = new System.Collections.Generic.HashSet<string>();
        foreach (Page page in d1.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                shapeSet1.Add($"{page.ID}_{shape.ID}");
            }
        }

        foreach (Page page2 in d2.Pages)
        {
            foreach (Shape shape2 in page2.Shapes)
            {
                string key = $"{page2.ID}_{shape2.ID}";
                if (!shapeSet1.Contains(key))
                {
                    Console.WriteLine($"Shape missing in Diagram1: PageID={page2.ID}, ShapeID={shape2.ID}");
                }
            }
        }
    }
}