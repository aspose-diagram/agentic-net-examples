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

            // Load the original Visio diagram
            Diagram diagram = new Diagram("input.vdx");

            // Add a master shape from a template (preserves original master definitions)
            // The master name must exist in the template file.
            int masterId = diagram.AddMaster("template.vdx", "Rectangle");

            // Add a shape based on the newly added master to the first page
            // Parameters: PinX, PinY, master name, and page index (0 = first page)
            diagram.AddShape(2.0, 2.0, "Rectangle", 0);

            // Configure save options – using VDX format and auto‑fit page to content
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx)
            {
                AutoFitPageToDrawingContent = true
            };

            // Save the modified diagram to a new file while keeping all master definitions
            diagram.Save("output.vdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
