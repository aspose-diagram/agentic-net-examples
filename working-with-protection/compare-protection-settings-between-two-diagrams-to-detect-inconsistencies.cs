using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Get file paths for the two diagrams
        string path1;
        string path2;

        if (args.Length >= 2)
        {
            path1 = args[0];
            path2 = args[1];
        }
        else
        {
            Console.Write("Enter path for first diagram: ");
            path1 = Console.ReadLine();
            Console.Write("Enter path for second diagram: ");
            path2 = Console.ReadLine();
        }

        // Load diagrams
        Diagram diagram1 = new Diagram(path1);
        Diagram diagram2 = new Diagram(path2);

        // Compare protection settings
        CompareDocumentProtection(diagram1, diagram2);
        CompareShapeProtection(diagram1, diagram2);
    }

    // Compare global document protection flags
    static void CompareDocumentProtection(Diagram d1, Diagram d2)
    {
        Console.WriteLine("=== Document Protection Comparison ===");

        CompareBool("ProtectBkgnds", d1.DocumentSettings.ProtectBkgnds, d2.DocumentSettings.ProtectBkgnds);
        CompareBool("ProtectMasters", d1.DocumentSettings.ProtectMasters, d2.DocumentSettings.ProtectMasters);
        CompareBool("ProtectShapes", d1.DocumentSettings.ProtectShapes, d2.DocumentSettings.ProtectShapes);
        CompareBool("ProtectStyles", d1.DocumentSettings.ProtectStyles, d2.DocumentSettings.ProtectStyles);
    }

    // Compare shape‑level protection flags page by page
    static void CompareShapeProtection(Diagram d1, Diagram d2)
    {
        Console.WriteLine("=== Shape Protection Comparison ===");

        if (d1.Pages.Count != d2.Pages.Count)
        {
            Console.WriteLine($"Page count mismatch: Diagram1 has {d1.Pages.Count}, Diagram2 has {d2.Pages.Count}");
            // Continue with the minimum number of pages
        }

        int pageCount = Math.Min(d1.Pages.Count, d2.Pages.Count);
        for (int i = 0; i < pageCount; i++)
        {
            Page page1 = d1.Pages[i];
            Page page2 = d2.Pages[i];

            Console.WriteLine($"--- Page {i + 1} (Name: {page1.Name}) ---");

            // Build a lookup for shapes in diagram2 by ID for quick access
            var shapeLookup2 = new System.Collections.Generic.Dictionary<long, Shape>();
            foreach (Shape s2 in page2.Shapes)
            {
                shapeLookup2[s2.ID] = s2;
            }

            foreach (Shape s1 in page1.Shapes)
            {
                if (!shapeLookup2.TryGetValue(s1.ID, out Shape s2))
                {
                    Console.WriteLine($"Shape ID {s1.ID} present in Diagram1 but missing in Diagram2.");
                    continue;
                }

                // Compare each protection property
                CompareProtectionProperty("LockAspect", s1, s2);
                CompareProtectionProperty("LockBegin", s1, s2);
                CompareProtectionProperty("LockCalcWH", s1, s2);
                CompareProtectionProperty("LockCrop", s1, s2);
                CompareProtectionProperty("LockCustProp", s1, s2);
                CompareProtectionProperty("LockDelete", s1, s2);
                CompareProtectionProperty("LockEnd", s1, s2);
                CompareProtectionProperty("LockFormat", s1, s2);
                CompareProtectionProperty("LockFromGroupFormat", s1, s2);
                CompareProtectionProperty("LockGroup", s1, s2);
                CompareProtectionProperty("LockHeight", s1, s2);
                CompareProtectionProperty("LockMoveX", s1, s2);
                CompareProtectionProperty("LockMoveY", s1, s2);
                CompareProtectionProperty("LockRotate", s1, s2);
                CompareProtectionProperty("LockSelect", s1, s2);
                CompareProtectionProperty("LockTextEdit", s1, s2);
                CompareProtectionProperty("LockThemeColors", s1, s2);
                CompareProtectionProperty("LockThemeEffects", s1, s2);
                CompareProtectionProperty("LockVtxEdit", s1, s2);
                CompareProtectionProperty("LockWidth", s1, s2);
            }
        }
    }

    // Helper to compare a single BOOL property on shape protection
    static void CompareProtectionProperty(string propName, Shape s1, Shape s2)
    {
        // Use reflection to get the property from the Protection object
        var propInfo = typeof(Protection).GetProperty(propName);
        if (propInfo == null)
        {
            // Property name not found; skip
            return;
        }

        var val1Obj = propInfo.GetValue(s1.Protection);
        var val2Obj = propInfo.GetValue(s2.Protection);

        // Each property is a BoolValue; compare its .Value
        var boolVal1 = (BOOL?)val1Obj?.GetType().GetProperty("Value")?.GetValue(val1Obj);
        var boolVal2 = (BOOL?)val2Obj?.GetType().GetProperty("Value")?.GetValue(val2Obj);

        if (boolVal1 != boolVal2)
        {
            Console.WriteLine($"Shape ID {s1.ID} ({s1.NameU}) - {propName} mismatch: Diagram1={boolVal1}, Diagram2={boolVal2}");
        }
    }

    // Helper to compare two BOOL values and report differences
    static void CompareBool(string name, BOOL val1, BOOL val2)
    {
        if (val1 != val2)
        {
            Console.WriteLine($"{name} mismatch: Diagram1={val1}, Diagram2={val2}");
        }
    }
}
