using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class DiagnosticTool
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (first argument or default)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

            // Load the Visio diagram (load rule)
            Diagram diagram = new Diagram(inputPath);

            // Dictionaries to track user-defined cell names and shapes lacking them
            var userNameToShapes = new Dictionary<string, List<Shape>>(StringComparer.OrdinalIgnoreCase);
            var shapesMissingUsers = new List<Shape>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // If the shape has no user-defined cells, record it as missing
                    if (shape.Users == null || shape.Users.Count == 0)
                    {
                        shapesMissingUsers.Add(shape);
                        continue;
                    }

                    // Record each user-defined cell name and associate it with the shape
                    foreach (User user in shape.Users)
                    {
                        // Prefer the universal name; fallback to the local name
                        string cellName = !string.IsNullOrEmpty(user.NameU) ? user.NameU : user.Name;
                        if (string.IsNullOrEmpty(cellName))
                            continue; // Skip unnamed cells

                        if (!userNameToShapes.ContainsKey(cellName))
                            userNameToShapes[cellName] = new List<Shape>();

                        userNameToShapes[cellName].Add(shape);
                    }
                }
            }

            // Output shapes that are missing user-defined cells
            Console.WriteLine("=== Shapes Missing User-Defined Cells ===");
            foreach (Shape shape in shapesMissingUsers)
            {
                string pageName = shape.Page?.Name ?? "UnknownPage";
                Console.WriteLine($"Page: {pageName}, Shape ID: {shape.ID}, Name: {shape.NameU}");
            }

            // Output duplicate user-defined cell names across shapes
            Console.WriteLine("\n=== Duplicate User-Defined Cell Names ===");
            var duplicates = userNameToShapes.Where(kvp => kvp.Value.Count > 1);
            foreach (var kvp in duplicates)
            {
                Console.WriteLine($"Cell Name: {kvp.Key}");
                foreach (Shape shape in kvp.Value)
                {
                    string pageName = shape.Page?.Name ?? "UnknownPage";
                    Console.WriteLine($"\tPage: {pageName}, Shape ID: {shape.ID}, Name: {shape.NameU}");
                }
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
