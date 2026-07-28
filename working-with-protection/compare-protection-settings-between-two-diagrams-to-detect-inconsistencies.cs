using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths: first diagram, second diagram
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: CompareProtection <DiagramPath1> <DiagramPath2>");
            return;
        }

        string path1 = args[0];
        string path2 = args[1];

        // Load the two diagrams
        Diagram diagram1 = new Diagram(path1);
        Diagram diagram2 = new Diagram(path2);

        Console.WriteLine("=== Global Document Protection Comparison ===");
        CompareDocumentProtection(diagram1, diagram2);

        Console.WriteLine("\n=== Shape-Level Protection Comparison ===");
        CompareShapeProtectionAcrossDiagrams(diagram1, diagram2);
    }

    static void CompareDocumentProtection(Diagram d1, Diagram d2)
    {
        var ds1 = d1.DocumentSettings;
        var ds2 = d2.DocumentSettings;

        CompareBoolProperty("ProtectBkgnds", ds1.ProtectBkgnds, ds2.ProtectBkgnds);
        CompareBoolProperty("ProtectMasters", ds1.ProtectMasters, ds2.ProtectMasters);
        CompareBoolProperty("ProtectShapes", ds1.ProtectShapes, ds2.ProtectShapes);
        CompareBoolProperty("ProtectStyles", ds1.ProtectStyles, ds2.ProtectStyles);
    }

    static void CompareBoolProperty(string name, BOOL val1, BOOL val2)
    {
        if (val1 != val2)
        {
            Console.WriteLine($"Mismatch in {name}: Diagram1={val1}, Diagram2={val2}");
        }
    }

    static void CompareShapeProtectionAcrossDiagrams(Diagram d1, Diagram d2)
    {
        // Assume pages are in the same order and have the same count
        if (d1.Pages.Count != d2.Pages.Count)
        {
            Console.WriteLine($"Page count differs: Diagram1={d1.Pages.Count}, Diagram2={d2.Pages.Count}");
            return;
        }

        for (int pageIndex = 0; pageIndex < d1.Pages.Count; pageIndex++)
        {
            Page page1 = d1.Pages[pageIndex];
            Page page2 = d2.Pages[pageIndex];

            // Build a lookup for shapes in diagram2 by ID for quick access
            var shapeMap2 = new System.Collections.Generic.Dictionary<long, Shape>();
            foreach (Shape s2 in page2.Shapes)
            {
                shapeMap2[s2.ID] = s2;
            }

            foreach (Shape s1 in page1.Shapes)
            {
                if (!shapeMap2.TryGetValue(s1.ID, out Shape s2))
                {
                    Console.WriteLine($"Shape ID {s1.ID} exists in Diagram1 page '{page1.Name}' but not in Diagram2.");
                    continue;
                }

                CompareShapeProtection(s1, s2, page1.Name, s1.ID);
            }
        }
    }

    static void CompareShapeProtection(Shape s1, Shape s2, string pageName, long shapeId)
    {
        // Helper to compare individual lock properties
        void Check(string propName, BOOL val1, BOOL val2)
        {
            if (val1 != val2)
            {
                Console.WriteLine($"Page '{pageName}', Shape ID {shapeId}: Mismatch in {propName} - Diagram1={val1}, Diagram2={val2}");
            }
        }

        // Compare a selection of common protection flags
        Check("LockMoveX", s1.Protection.LockMoveX.Value, s2.Protection.LockMoveX.Value);
        Check("LockMoveY", s1.Protection.LockMoveY.Value, s2.Protection.LockMoveY.Value);
        Check("LockWidth", s1.Protection.LockWidth.Value, s2.Protection.LockWidth.Value);
        Check("LockHeight", s1.Protection.LockHeight.Value, s2.Protection.LockHeight.Value);
        Check("LockRotate", s1.Protection.LockRotate.Value, s2.Protection.LockRotate.Value);
        Check("LockVtxEdit", s1.Protection.LockVtxEdit.Value, s2.Protection.LockVtxEdit.Value);
        Check("LockAspect", s1.Protection.LockAspect.Value, s2.Protection.LockAspect.Value);
        Check("LockSelect", s1.Protection.LockSelect.Value, s2.Protection.LockSelect.Value);
        Check("LockTextEdit", s1.Protection.LockTextEdit.Value, s2.Protection.LockTextEdit.Value);
        Check("LockDelete", s1.Protection.LockDelete.Value, s2.Protection.LockDelete.Value);
        // Add more checks as needed following the same pattern
    }
}
