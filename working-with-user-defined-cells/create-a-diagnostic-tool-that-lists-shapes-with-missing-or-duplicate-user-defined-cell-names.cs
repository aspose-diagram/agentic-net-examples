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
            Console.Error.WriteLine("Usage: DiagnosticTool <diagram-file-path>");
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
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Lists to collect diagnostic information
            var shapesWithNoUserCells = new List<(Page page, Shape shape)>();
            var userCellNameMap = new Dictionary<string, List<(Page page, Shape shape)>>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check for missing user-defined cells
                    if (shape.Users == null || shape.Users.Count == 0)
                    {
                        shapesWithNoUserCells.Add((page, shape));
                    }

                    // Process each user-defined cell
                    if (shape.Users != null)
                    {
                        foreach (User user in shape.Users)
                        {
                            // Use NameU if available, otherwise fallback to Name
                            string cellName = !string.IsNullOrEmpty(user.NameU) ? user.NameU : user.Name;

                            if (string.IsNullOrEmpty(cellName))
                                continue; // Skip unnamed cells

                            if (!userCellNameMap.ContainsKey(cellName))
                            {
                                userCellNameMap[cellName] = new List<(Page, Shape)>();
                            }
                            userCellNameMap[cellName].Add((page, shape));
                        }
                    }
                }
            }

            // Output shapes with missing user-defined cells
            Console.WriteLine("=== Shapes Missing User-Defined Cells ===");
            if (shapesWithNoUserCells.Count == 0)
            {
                Console.WriteLine("None");
            }
            else
            {
                foreach (var entry in shapesWithNoUserCells)
                {
                    Console.WriteLine($"Page: {entry.page.Name}, Shape ID: {entry.shape.ID}, Shape NameU: {entry.shape.NameU}");
                }
            }

            // Detect duplicate user-defined cell names across the diagram
            Console.WriteLine();
            Console.WriteLine("=== Duplicate User-Defined Cell Names ===");
            bool anyDuplicates = false;
            foreach (var kvp in userCellNameMap)
            {
                if (kvp.Value.Count > 1)
                {
                    anyDuplicates = true;
                    Console.WriteLine($"Cell Name: {kvp.Key}");
                    foreach (var loc in kvp.Value)
                    {
                        Console.WriteLine($"  Page: {loc.page.Name}, Shape ID: {loc.shape.ID}, Shape NameU: {loc.shape.NameU}");
                    }
                }
            }

            if (!anyDuplicates)
            {
                Console.WriteLine("None");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}