using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Starting value for custom IDs (choose any range that does not clash with existing IDs)
            int customId = 1000;

            // Iterate through all masters in the diagram and assign a custom ID
            foreach (Master master in diagram.Masters)
            {
                master.ID = customId;
                customId++;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
