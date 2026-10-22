using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths as arguments: old version and new version.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <oldDiagramPath> <newDiagramPath>");
            return;
        }

        string oldPath = args[0];
        string newPath = args[1];

        // Guard against missing files.
        if (!File.Exists(oldPath))
        {
            Console.Error.WriteLine($"File not found: {oldPath}");
            return;
        }
        if (!File.Exists(newPath))
        {
            Console.Error.WriteLine($"File not found: {newPath}");
            return;
        }

        try
        {
            // Load the old diagram inside a using block as required.
            using (Diagram oldDiagram = new Diagram(oldPath))
            {
                // Load the new diagram inside a using block as required.
                using (Diagram newDiagram = new Diagram(newPath))
                {
                    // Build a lookup of shapes from the new diagram by their ID.
                    Dictionary<long, Shape> newShapesById = new Dictionary<long, Shape>();
                    foreach (Page newPage in newDiagram.Pages)
                    {
                        foreach (Shape newShape in newPage.Shapes)
                        {
                            // Store each shape using its unique ID.
                            newShapesById[newShape.ID] = newShape;
                        }
                    }

                    // Iterate over all pages and shapes in the old diagram.
                    foreach (Page oldPage in oldDiagram.Pages)
                    {
                        foreach (Shape oldShape in oldPage.Shapes)
                        {
                            // Process only 1-D connector shapes (OneD is a native bool).
                            if (oldShape.OneD)
                            {
                                // Try to find the corresponding shape in the new diagram.
                                if (newShapesById.TryGetValue(oldShape.ID, out Shape matchingNewShape))
                                {
                                    // Retrieve line jump style values from both shapes.
                                    ConLineJumpStyleValue oldJumpStyle = oldShape.Layout.ConLineJumpStyle.Value;
                                    ConLineJumpStyleValue newJumpStyle = matchingNewShape.Layout.ConLineJumpStyle.Value;

                                    // Compare the styles and report differences.
                                    if (oldJumpStyle != newJumpStyle)
                                    {
                                        Console.WriteLine($"Shape ID {oldShape.ID} on page {oldPage.ID} has different line jump style:");
                                        Console.WriteLine($"  Old style: {oldJumpStyle}");
                                        Console.WriteLine($"  New style: {newJumpStyle}");
                                    }
                                }
                                else
                                {
                                    // Shape missing in the new diagram.
                                    Console.WriteLine($"Shape ID {oldShape.ID} on page {oldPage.ID} is missing in the new diagram.");
                                }
                            }
                        }
                    }

                    // Optionally, report shapes that exist only in the new diagram.
                    foreach (KeyValuePair<long, Shape> kvp in newShapesById)
                    {
                        long shapeId = kvp.Key;
                        Shape newShape = kvp.Value;

                        // Process only 1-D connector shapes.
                        if (newShape.OneD)
                        {
                            // Check if this shape was not present in the old diagram.
                            bool existsInOld = false;
                            foreach (Page oldPage in oldDiagram.Pages)
                            {
                                foreach (Shape oldShape in oldPage.Shapes)
                                {
                                    if (oldShape.ID == shapeId)
                                    {
                                        existsInOld = true;
                                        break;
                                    }
                                }
                                if (existsInOld) break;
                            }

                            if (!existsInOld)
                            {
                                // Page reference may be null for orphan shapes; use 0 as fallback.
                                long pageId = newShape.Page?.ID ?? 0;
                                Console.WriteLine($"New connector shape ID {shapeId} on page {pageId} appears only in the new diagram.");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }
}