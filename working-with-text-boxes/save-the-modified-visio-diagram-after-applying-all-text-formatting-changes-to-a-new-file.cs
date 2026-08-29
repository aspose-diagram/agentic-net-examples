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

            // Load the existing Visio diagram
            string inputFile = "input.vsdx";
            Diagram diagram = new Diagram(inputFile);

            // -------------------------------------------------
            // Apply text formatting changes here.
            // Example (placeholder):
            // foreach (Shape shape in diagram.Pages[0].Shapes)
            // {
            //     // shape.Text = "New Text";
            //     // shape.Characters.CharProps[...]=...;
            // }
            // -------------------------------------------------

            // Save the modified diagram to a new file
            string outputFile = "output.vsdx";
            diagram.Save(outputFile, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
