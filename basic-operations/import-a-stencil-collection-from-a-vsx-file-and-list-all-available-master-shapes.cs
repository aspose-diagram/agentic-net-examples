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
            string stencilPath = @"C:\Stencils\MyStencil.vsx";

            // Load the stencil as a diagram to access its masters
            Diagram stencilDiagram = new Diagram(stencilPath);

            // Create an empty diagram that will receive the masters
            Diagram diagram = new Diagram();

            // Import each master from the stencil into the new diagram
            foreach (Master master in stencilDiagram.Masters)
            {
                // Add the master by specifying the stencil file path and the master name
                diagram.AddMaster(stencilPath, master.Name);
            }

            // List all masters now available in the diagram
            Console.WriteLine("Masters imported from the stencil:");
            foreach (Master master in diagram.Masters)
            {
                Console.WriteLine($"- {master.Name}");
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
