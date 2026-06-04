using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSX stencil file
            string stencilPath = @"C:\Stencils\example.vsx";

            // Load the stencil using the VSX format
            Diagram stencil = new Diagram(stencilPath, LoadFileFormat.Vsx);

            // List all master shapes contained in the stencil
            foreach (Master master in stencil.Masters)
            {
                Console.WriteLine($"Master Name: {master.Name}, Universal Name: {master.NameU}");
            }

            // Clean up resources
            stencil.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
