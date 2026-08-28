using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the original protected diagram file
            string sourceFile = "protected.vsdx";

            // Path where the copy will be saved
            string destinationFile = "protected_copy.vsdx";

            // Load the diagram from the source file
            Diagram diagram = new Diagram(sourceFile);

            // Save the diagram to a new file using the same format.
            // This preserves all original metadata (properties, settings, etc.).
            diagram.Save(destinationFile, SaveFileFormat.Vsdx);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
