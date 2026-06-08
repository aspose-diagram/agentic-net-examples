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
            string originalFile = "input.vsdx";
            Diagram diagram = new Diagram(originalFile);

            // TODO: Apply any modifications to the diagram here
            // e.g., diagram.Pages[0].Shapes.Add(...);

            // Save the updated diagram to a new file to keep the original unchanged
            string newFile = "output.vsdx";
            diagram.Save(newFile, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
