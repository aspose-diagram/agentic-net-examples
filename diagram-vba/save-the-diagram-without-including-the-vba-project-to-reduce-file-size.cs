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

            // Path to the source Visio file
            string inputFile = "input.vsdx";

            // Path for the output file without VBA project
            string outputFile = "output_without_vba.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputFile);

            // Remove any VBA macros to reduce file size
            diagram.RemoveMacro();

            // Save the diagram in the same format (VSDX) without the VBA project
            diagram.Save(outputFile, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
