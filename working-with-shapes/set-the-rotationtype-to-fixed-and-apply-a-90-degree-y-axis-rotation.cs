using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                    throw new Exception("The diagram contains no pages.");

                Page page = diagram.Pages[0];

                if (page.Shapes.Count == 0)
                    throw new Exception("The first page contains no shapes.");

                // Retrieve the first shape on the page
                Shape shape = page.Shapes[0];

                // Set the 3‑D rotation type to a fixed (no projection) setting.
                // In Aspose.Diagram this corresponds to RotationTypeValue.None.
                shape.ThreeDFormat.RotationType.Value = RotationTypeValue.None;

                // Apply a 90‑degree rotation around the Y‑axis.
                shape.ThreeDFormat.RotationYAngle.Value = 90;

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }