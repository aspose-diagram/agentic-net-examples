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
            string visioFilePath = "input.vsdx";

            // Load the Visio diagram using the built‑in constructor (load rule)
            using (Diagram diagram = new Diagram(visioFilePath))
            {
                // Iterate through all master shapes in the document
                foreach (Master master in diagram.Masters)
                {
                    // Output the master name and its unique identifier (GUID)
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
