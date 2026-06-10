using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string filePath = "input.vsdx";

            // Load the Visio diagram using the Diagram(string) constructor (load rule)
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through all masters in the document
                foreach (Master master in diagram.Masters)
                {
                    // Output the master's name and its unique identifier (GUID)
                    Console.WriteLine($"Master Name: {master.Name}, UniqueID: {master.UniqueID}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
