using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Ensure the target master is present in the diagram.
             // If the master is defined in another template, add it to the current diagram.
             // Example: add master named "NewMaster" from a template file.
            int newMasterId = diagram.AddMaster("template.vst", "NewMaster");

            // Retrieve the shape whose master you want to replace.
            // Here we assume the shape's ID is known (e.g., 5).
            Aspose.Diagram.Shape shape = diagram.Pages[0].Shapes.GetShape(5);

            // Assign the new master to the shape.
            // The Masters collection is indexed by the master’s unique ID.
            shape.Master = diagram.Masters[newMasterId];

            // Save the modified diagram.
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
