using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class DiagramProtectionComparer
{
    // Compare two Protection objects property by property
    static bool ProtectionsEqual(Protection p1, Protection p2)
    {
        if (p1 == null || p2 == null) return p1 == p2;

        return
            p1.LockAspect == p2.LockAspect &&
            p1.LockBegin == p2.LockBegin &&
            p1.LockCalcWH == p2.LockCalcWH &&
            p1.LockCrop == p2.LockCrop &&
            p1.LockCustProp == p2.LockCustProp &&
            p1.LockDelete == p2.LockDelete &&
            p1.LockEnd == p2.LockEnd &&
            p1.LockFormat == p2.LockFormat &&
            p1.LockFromGroupFormat == p2.LockFromGroupFormat &&
            p1.LockGroup == p2.LockGroup &&
            p1.LockHeight == p2.LockHeight &&
            p1.LockMoveX == p2.LockMoveX &&
            p1.LockMoveY == p2.LockMoveY &&
            p1.LockRotate == p2.LockRotate &&
            p1.LockSelect == p2.LockSelect &&
            p1.LockTextEdit == p2.LockTextEdit &&
            p1.LockThemeColors == p2.LockThemeColors &&
            p1.LockThemeEffects == p2.LockThemeEffects &&
            p1.LockVtxEdit == p2.LockVtxEdit &&
            p1.LockWidth == p2.LockWidth;
    }

    // Build a lookup of shape IDs to their Protection settings for a diagram
    static Dictionary<long, Protection> BuildProtectionMap(Diagram diagram)
    {
        var map = new Dictionary<long, Protection>();
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Use Shape ID as unique key
                map[shape.ID] = shape.Protection;
            }
        }
        return map;
    }

    static void Main()
    {
        try
        {

            // Load the two diagrams (lifecycle rule: use provided constructor)
            var diagramPath1 = "DiagramA.vsdx";
            var diagramPath2 = "DiagramB.vsdx";

            using (var diagram1 = new Diagram(diagramPath1))
            using (var diagram2 = new Diagram(diagramPath2))
            {
                // Build protection dictionaries
                var protectionMap1 = BuildProtectionMap(diagram1);
                var protectionMap2 = BuildProtectionMap(diagram2);

                // Compare shapes present in both diagrams
                foreach (var kvp in protectionMap1)
                {
                    var shapeId = kvp.Key;
                    var prot1 = kvp.Value;

                    if (protectionMap2.TryGetValue(shapeId, out var prot2))
                    {
                        if (!ProtectionsEqual(prot1, prot2))
                        {
                            Console.WriteLine($"Inconsistent protection for Shape ID {shapeId}:");
                            Console.WriteLine($"  Diagram A - LockAspect: {prot1.LockAspect}, LockDelete: {prot1.LockDelete}, LockMoveX: {prot1.LockMoveX}");
                            Console.WriteLine($"  Diagram B - LockAspect: {prot2.LockAspect}, LockDelete: {prot2.LockDelete}, LockMoveX: {prot2.LockMoveX}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Shape ID {shapeId} exists in Diagram A but not in Diagram B.");
                    }
                }

                // Detect shapes that exist only in Diagram B
                foreach (var shapeId in protectionMap2.Keys)
                {
                    if (!protectionMap1.ContainsKey(shapeId))
                    {
                        Console.WriteLine($"Shape ID {shapeId} exists in Diagram B but not in Diagram A.");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
