using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSX stencil file using the appropriate load format.
            // The Diagram constructor with (string, LoadFileFormat) follows the provided lifecycle rule.
            var stencil = new Diagram("StencilFile.vsx", LoadFileFormat.Vsx);

            // Iterate through all masters (shapes) defined in the stencil and list their names.
            foreach (Master master in stencil.Masters)
            {
                // Output the master name and its universal name.
                Console.WriteLine($"Master Name: {master.Name}, Universal Name: {master.NameU}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
