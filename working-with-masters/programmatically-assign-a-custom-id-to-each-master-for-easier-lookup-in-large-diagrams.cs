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
            Diagram diagram = new Diagram("input.vsdx");

            // Assign a custom integer ID to each master for easier lookup
            int customId = 1000; // starting point for custom IDs
            foreach (Master master in diagram.Masters)
            {
                master.ID = customId++;               // set custom ID
                master.BaseID = Guid.NewGuid();       // optional: give each master a unique GUID
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
