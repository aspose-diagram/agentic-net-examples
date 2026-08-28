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

            // Configure the font cache size for Aspose.Diagram.
            // The library checks the environment variable "Aspose.Diagram.FontCacheSize"
            // to determine how many font objects it should keep in memory.
            // Setting it to a higher value (e.g., 500) can improve performance
            // when processing large diagrams that use many different fonts.
            Environment.SetEnvironmentVariable("Aspose.Diagram.FontCacheSize", "500");

            // Load an existing diagram (using the standard load pattern).
            Diagram diagram = new Diagram("input.vsdx");

            // ... perform any diagram processing here ...

            // Save the diagram (using the standard save pattern).
            diagram.Save("output.vsdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
