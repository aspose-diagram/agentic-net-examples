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

            Diagram diagram = null;
            try
            {
                // Load an existing Visio diagram from a file
                diagram = new Diagram("input.vsdx");

                // ----- Place any diagram processing logic here -----
                // Example: modify pages, shapes, etc.
                // --------------------------------------------------

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
            finally
            {
                // Ensure the Diagram object is properly disposed even if an exception occurs
                if (diagram != null)
                {
                    diagram.Dispose();
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
