using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                // Collect shape IDs first because we may modify the collection while iterating.
                var shapeIds = new System.Collections.Generic.List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    shapeIds.Add(shape.ID);
                }

                foreach (long shapeId in shapeIds)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape == null)
                        continue;

                    // Check if the shape is marked as deleted.
                    if (shape.Del == BOOL.True)
                    {
                        // ------------------------------------------------------------
                        // Cleanup routine: remove all custom properties associated with
                        // the deleted shape. This mimics the intended behavior of an
                        // EventShapeDeleted cell, which is not available in the API.
                        // ------------------------------------------------------------
                        var propsToRemove = new System.Collections.Generic.List<Prop>();
                        foreach (Prop prop in shape.Props)
                        {
                            propsToRemove.Add(prop);
                        }

                        foreach (Prop prop in propsToRemove)
                        {
                            shape.Props.Remove(prop);
                        }

                        // Optionally, remove user‑defined cells as part of cleanup.
                        var usersToRemove = new System.Collections.Generic.List<User>();
                        foreach (User user in shape.Users)
                        {
                            usersToRemove.Add(user);
                        }

                        foreach (User user in usersToRemove)
                        {
                            shape.Users.Remove(user);
                        }

                        // Since the shape is already flagged as deleted, we do not
                        // attempt to physically remove it from the page collection.
                        // The cleanup ensures no orphaned custom data remains.
                    }
                    else
                    {
                        // For shapes that are not deleted, we can set a placeholder
                        // formula in an existing event cell (e.g., EventDrop) to
                        // illustrate where a deletion‑triggered formula would go.
                        // Note: EventShapeDeleted does not exist; using EventDrop as
                        // the closest substitute.
                        shape.Event.EventDrop.Ufe.F = "\"\""; // No‑op formula.
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}