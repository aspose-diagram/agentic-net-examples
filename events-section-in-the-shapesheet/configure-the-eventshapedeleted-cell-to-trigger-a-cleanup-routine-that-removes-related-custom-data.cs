using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Assume we want to configure the first shape on the first page
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(1);

                // Set the shape's deletion event to call a cleanup routine.
                // The Visio formula CALLTHIS invokes a macro named "CleanupDeletedShapes".
                shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"CleanupDeletedShapes\")";

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // This method represents the cleanup routine that would be invoked by the macro.
        // It removes custom properties (Props) and user-defined cells (Users) from shapes
        // that have been marked as deleted (shape.Del == BOOL.True).
        static void CleanupDeletedShapes(Diagram diagram)
        {
            foreach (Page page in diagram.Pages)
            {
                // Collect shape IDs to avoid modifying the collection while iterating
                var shapeIds = new System.Collections.Generic.List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    shapeIds.Add(shape.ID);
                }

                foreach (long id in shapeIds)
                {
                    Shape shape = page.Shapes.GetShape(id);
                    if (shape.Del == BOOL.True)
                    {
                        // Remove all custom properties
                        shape.Props.Clear();

                        // Remove all user-defined cells
                        shape.Users.Clear();
                    }
                }
            }
        }
    }