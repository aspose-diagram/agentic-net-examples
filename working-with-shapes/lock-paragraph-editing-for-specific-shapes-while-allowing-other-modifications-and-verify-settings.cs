using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the input and output Visio files.
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram.
            Page page = diagram.Pages[0];

            // Define the name of the shape(s) whose paragraph editing should be locked.
            string targetShapeNameU = "MyShape";

            // Flag to track whether any target shape was found.
            bool shapeFound = false;

            // Iterate through all shapes on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Check if this shape matches the target name (case‑sensitive).
                if (shape.NameU == targetShapeNameU)
                {
                    shapeFound = true;

                    // Lock paragraph (text) editing for this shape.
                    shape.Protection.LockTextEdit.Value = BOOL.True;

                    // Verify that the lock was applied.
                    if (shape.Protection.LockTextEdit.Value != BOOL.True)
                    {
                        throw new Exception($"Failed to lock text editing for shape ID {shape.ID}.");
                    }

                    // Ensure other protection flags remain unchanged (example: LockFormat should stay false).
                    if (shape.Protection.LockFormat.Value != BOOL.False)
                    {
                        throw new Exception($"Unexpected LockFormat state for shape ID {shape.ID}.");
                    }

                    Console.WriteLine($"Shape ID {shape.ID} ('{shape.NameU}') locked for paragraph editing.");
                }
            }

            if (!shapeFound)
            {
                Console.WriteLine($"No shape with NameU '{targetShapeNameU}' was found on page '{page.Name}'.");
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
