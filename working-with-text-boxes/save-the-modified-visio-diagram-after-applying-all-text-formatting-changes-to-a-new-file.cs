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

            // Load the existing Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // -------------------------------------------------
            // Apply text formatting changes to the diagram here
            // Example (placeholder):
            // foreach (Page page in diagram.Pages)
            // {
            //     foreach (Shape shape in page.Shapes)
            //     {
            //         // Modify shape text formatting as needed
            //         // shape.Text = "New Text";
            //         // shape.Characters.CharProps["Font"] = "Arial";
            //     }
            // }
            // -------------------------------------------------

            // Save the modified diagram to a new file (VDX format)
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
