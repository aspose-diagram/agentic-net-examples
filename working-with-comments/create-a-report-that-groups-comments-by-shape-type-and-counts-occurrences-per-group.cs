using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <program> <visio-file-path>");
            return;
        }

        string visioPath = args[0];
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        Diagram diagram;
        try
        {
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        var commentCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        try
        {
            foreach (Page page in diagram.Pages)
            {
                if (page.PageSheet?.Annotations == null) continue;

                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    int shapeId = annotation.ShapeID;
                    Shape shape = null;
                    try
                    {
                        shape = page.Shapes.GetShape(shapeId);
                    }
                    catch
                    {
                        // Skip if shape cannot be retrieved
                        continue;
                    }

                    string key;
                    if (shape != null && shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                    {
                        key = shape.Master.Name;
                    }
                    else if (shape != null)
                    {
                        key = shape.Type.ToString();
                    }
                    else
                    {
                        key = "Unknown";
                    }

                    if (commentCounts.ContainsKey(key))
                        commentCounts[key]++;
                    else
                        commentCounts[key] = 1;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing comments: {ex.Message}");
            return;
        }

        Console.WriteLine("Comments grouped by shape type:");
        foreach (var kvp in commentCounts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}