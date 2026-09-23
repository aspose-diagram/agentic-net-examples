using System.IO;
using System;
using Aspose.Diagram;

class ListMasterShapes
{
    static void Main()
    {
        try
        {

            // Load the Visio file
            // (Assumes the file "input.vsdx" is in the same directory as the executable)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all master shapes in the document
            foreach (Master master in diagram.Masters)
            {
                // Each master has a unique identifier (ID) and a name (NameU)
                Console.WriteLine($"Master ID: {master.ID}, Name: {master.NameU}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
