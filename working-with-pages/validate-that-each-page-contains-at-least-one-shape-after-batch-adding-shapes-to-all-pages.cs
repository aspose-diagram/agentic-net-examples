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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the master shape to be added (e.g., a built‑in rectangle)
            string masterName = "Rectangle";

            // Add a shape to every page in the document
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Add the shape at a fixed position (PinX, PinY). Width/Height are default.
                diagram.Pages[pageIndex].AddShape(1.0, 1.0, masterName);
            }

            // Validate that each page now contains at least one shape
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];
                if (page.Shapes.Count == 0)
                {
                    // Throw an exception if a page is found without shapes
                    throw new InvalidOperationException($"Page \"{page.Name}\" does not contain any shapes.");
                }
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
