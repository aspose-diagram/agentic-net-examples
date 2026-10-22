using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // List to hold shapes that have no user-defined cells
                var shapesMissingUserCells = new List<(string PageName, long ShapeId, string ShapeName)>();

                // Map to track user-defined cell names and the shapes that use them
                var userCellNameMap = new Dictionary<string, List<(string PageName, long ShapeId, string ShapeName)>>();

                // Manual page counter because Page does not have an Index property
                int pageCounter = 0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Determine a readable page name
                    string pageName = !string.IsNullOrEmpty(page.NameU) ? page.NameU : $"Page_{pageCounter}";
                    pageCounter++;

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Resolve shape name (fallback to ID if missing)
                        string shapeName = !string.IsNullOrEmpty(shape.NameU) ? shape.NameU : $"Shape_{shape.ID}";
                        long shapeId = shape.ID;

                        // Check for missing user-defined cells
                        if (shape.Users == null || shape.Users.Count == 0)
                        {
                            shapesMissingUserCells.Add((pageName, shapeId, shapeName));
                        }
                        else
                        {
                            // Process each user-defined cell
                            foreach (User userCell in shape.Users)
                            {
                                // Prefer universal name, fallback to local name
                                string cellName = !string.IsNullOrEmpty(userCell.NameU) ? userCell.NameU : userCell.Name;

                                if (string.IsNullOrEmpty(cellName))
                                    continue; // Skip unnamed cells

                                if (!userCellNameMap.ContainsKey(cellName))
                                {
                                    userCellNameMap[cellName] = new List<(string, long, string)>();
                                }

                                userCellNameMap[cellName].Add((pageName, shapeId, shapeName));
                            }
                        }
                    }
                }

                // Output shapes missing user-defined cells
                Console.WriteLine("=== Shapes Missing User-Defined Cells ===");
                if (shapesMissingUserCells.Count == 0)
                {
                    Console.WriteLine("None");
                }
                else
                {
                    foreach (var entry in shapesMissingUserCells)
                    {
                        Console.WriteLine($"Page: {entry.PageName}, Shape ID: {entry.ShapeId}, Shape Name: {entry.ShapeName}");
                    }
                }

                // Output duplicate user-defined cell names
                Console.WriteLine();
                Console.WriteLine("=== Duplicate User-Defined Cell Names ===");
                bool anyDuplicates = false;
                foreach (var kvp in userCellNameMap)
                {
                    if (kvp.Value.Count > 1)
                    {
                        anyDuplicates = true;
                        Console.WriteLine($"Cell Name: {kvp.Key}");
                        foreach (var shapeInfo in kvp.Value)
                        {
                            Console.WriteLine($"\tPage: {shapeInfo.PageName}, Shape ID: {shapeInfo.ShapeId}, Shape Name: {shapeInfo.ShapeName}");
                        }
                    }
                }

                if (!anyDuplicates)
                {
                    Console.WriteLine("None");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}