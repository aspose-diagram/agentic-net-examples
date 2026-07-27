using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSX stencil file using the appropriate load format
            var stencilDiagram = new Diagram("StencilFile.vsx", LoadFileFormat.Vsx);

            // Iterate through all masters (shapes) in the stencil and output their names
            foreach (Master master in stencilDiagram.Masters)
            {
                Console.WriteLine($"Master Name: {master.Name}, Universal Name: {master.NameU}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
