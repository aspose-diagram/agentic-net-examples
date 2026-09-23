using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Configure the deletion event to call a cleanup routine.
                    // Visio does not expose a dedicated EventShapeDeleted cell, so we use EventDrop as a placeholder.
                    shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"CleanupRoutine\")";

                    // If the shape is already marked as deleted, perform immediate cleanup.
                    if (shape.Del == BOOL.True)
                    {
                        CleanupCustomData(shape);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Cleanup routine that removes custom properties and user‑defined cells from a shape.
    static void CleanupCustomData(Shape shape)
    {
        // Remove all custom properties (Props)
        if (shape.Props != null)
        {
            var propsToRemove = new List<Prop>();
            foreach (Prop prop in shape.Props)
            {
                propsToRemove.Add(prop);
            }
            foreach (Prop prop in propsToRemove)
            {
                shape.Props.Remove(prop);
            }
        }

        // Remove all user‑defined cells (Users)
        if (shape.Users != null)
        {
            var usersToRemove = new List<User>();
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
