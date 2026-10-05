using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
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
            // Load the stencil (VSX) file as a Diagram instance.
            Diagram stencilDiagram = new Diagram(stencilPath, LoadFileFormat.Vsx);

            // Retrieve the Masters collection from the loaded stencil.
            MasterCollection masters = stencilDiagram.Masters;

            // If there are no masters, inform the user.
            if (masters == null || masters.Count == 0)
            {
                Console.WriteLine("No masters found in the stencil.");
                return;
            }

            // List all master shapes available in the stencil.
            Console.WriteLine($"Masters found in stencil \"{Path.GetFileName(stencilPath)}\":");
            foreach (Master master in masters)
            {
                // Output master ID and name.
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