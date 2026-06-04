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

            // Path to the source Visio file (can be VSD, VSDX, VDX, etc.)
            string sourcePath = "input.vsdx";

            // Path where the cleaned file will be saved
            string targetPath = "output.vsdx";

            // Load the diagram from the source file
            Diagram diagram = new Diagram(sourcePath);

            // Remove any VBA macros or projects to reduce file size
            diagram.RemoveMacro();

            // Save the diagram using the desired format (e.g., VSDX)
            diagram.Save(targetPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
