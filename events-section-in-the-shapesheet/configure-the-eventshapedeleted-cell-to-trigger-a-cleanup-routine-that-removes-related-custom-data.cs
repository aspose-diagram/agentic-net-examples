using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public class Program
{
    // Entry point
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Configure the EventShapeDeleted (using EventXFMod as a placeholder) for each shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Set the event formula to call a cleanup routine (Visio macro placeholder)
                    shape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"CleanupRoutine\")";
                }
            }

            // Perform the cleanup of shapes that have been marked as deleted
            CleanupDeletedShapes(diagram);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Removes custom data from shapes that are flagged as deleted (shape.Del == BOOL.True)
    private static void CleanupDeletedShapes(Diagram diagram)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.True)
                {
                    // Clear user‑defined cells (custom data)
                    shape.Users.Clear();

                    // Optionally clear the generic data fields
                    shape.Data1 = string.Empty;
                    shape.Data2 = string.Empty;
                    shape.Data3 = string.Empty;
                }
            }
        }
    }
}
