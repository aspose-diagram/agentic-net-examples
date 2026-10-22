using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <stencil.vsx>");
            return;
        }

        string stencilPath = args[0];

        // Guard: ensure the VSX file exists.
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Error: File not found - {stencilPath}");
            return;
        }

        try
        {
            // Load the stencil (VSX) file into a Diagram object.
            Diagram stencilDiagram = new Diagram(stencilPath, LoadFileFormat.Vsx);

            // Check if any masters are present.
            if (stencilDiagram.Masters == null || stencilDiagram.Masters.Count == 0)
            {
                Console.WriteLine("No masters found in the stencil.");
                return;
            }

            // List all master shapes available in the stencil.
            Console.WriteLine($"Masters found in stencil \"{Path.GetFileName(stencilPath)}\":");
            foreach (Master master in stencilDiagram.Masters)
            {
                // Output master ID and Name.
                Console.WriteLine($"- ID: {master.ID}, Name: {master.Name}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"An error occurred while processing the stencil: {ex.Message}");
        }
    }
}