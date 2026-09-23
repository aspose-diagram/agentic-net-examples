using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            Diagram diagram = null;
            try
            {
                // Load an existing diagram
                diagram = new Diagram("input.vsdx");

                // TODO: add processing logic here (e.g., modify shapes, pages, etc.)

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
            finally
            {
                // Ensure the Diagram object is disposed even if an exception occurs
                if (diagram != null)
                    diagram.Dispose();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
