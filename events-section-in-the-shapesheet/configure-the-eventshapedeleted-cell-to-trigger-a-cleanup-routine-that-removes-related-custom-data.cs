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
                foreach (Shape shape in page.Shapes)
                {
                    // ------------------------------------------------------------
                    // 1. Configure the (closest) event cell to represent a delete event.
                    //    Visio does not expose an explicit EventShapeDeleted cell via Aspose.Diagram,
                    //    so we use EventDrop as the nearest substitute.
                    // ------------------------------------------------------------
                    shape.Event.EventDrop.Ufe.F = "\"CleanupRoutine()\"";

                    // ------------------------------------------------------------
                    // 2. Perform cleanup for shapes that are already marked as deleted.
                    //    The deletion flag is the BOOL enumeration; compare with BOOL.True.
                    // ------------------------------------------------------------
                    if (shape.Del == BOOL.True)
                    {
                        // Collect custom properties that belong to this shape.
                        var propsToRemove = new System.Collections.Generic.List<Prop>();
                        foreach (Prop prop in shape.Props)
                        {
                            propsToRemove.Add(prop);
                        }

                        // Remove each collected property.
                        foreach (Prop prop in propsToRemove)
                        {
                            shape.Props.Remove(prop);
                        }

                        // Optionally, also remove user‑defined cells if needed.
                        // (Demonstrated here for completeness.)
                        var usersToRemove = new System.Collections.Generic.List<User>();
                        foreach (User user in shape.Users)
                        {
                            usersToRemove.Add(user);
                        }
                        foreach (User user in usersToRemove)
                        {
                            shape.Users.Remove(user);
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}