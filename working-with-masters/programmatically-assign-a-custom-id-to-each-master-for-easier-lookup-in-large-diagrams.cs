using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Assign a custom ID to each master (e.g., "CustomMasterID_1", "CustomMasterID_2", ...)
            int masterIndex = 1;
            foreach (Master master in diagram.Masters)
            {
                // Use the NameU property to store a custom identifier
                master.NameU = $"CustomMasterID_{masterIndex}";
                masterIndex++;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
