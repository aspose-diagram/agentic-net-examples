using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load the Visio diagram from its original location
            string originalFilePath = @"C:\Diagrams\SampleDiagram.vsdx";
            using (Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram(originalFilePath))
            {
                // Perform any required field operations on the diagram here
                // Example: diagram.Pages[0].Shapes[0].Text = "Updated Text";

                // Save the modified diagram back to the same file, preserving the original format (VSDX)
                diagram.Save(originalFilePath, Aspose.Diagram.SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
