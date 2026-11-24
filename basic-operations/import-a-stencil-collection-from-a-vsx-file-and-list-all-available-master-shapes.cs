using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the VSX stencil file path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the VSX stencil file as a command-line argument.");
            return;
        }

        string stencilPath = args[0];

        // Guard: ensure the stencil file exists
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        try
        {
            // Load the stencil file as a Diagram using the appropriate load format
            using (Diagram stencilDiagram = new Diagram(stencilPath, LoadFileFormat.Vsx))
            {
                // Verify that the stencil contains masters
                if (stencilDiagram.Masters == null || stencilDiagram.Masters.Count == 0)
                {
                    Console.WriteLine("No masters were found in the provided stencil.");
                }
                else
                {
                    Console.WriteLine($"Masters available in stencil \"{Path.GetFileName(stencilPath)}\":");
                    // Iterate over each master and display its name and ID
                    for (int i = 0; i < stencilDiagram.Masters.Count; i++)
                    {
                        Master master = stencilDiagram.Masters[i];
                        Console.WriteLine($"- Name: {master.Name}, ID: {master.ID}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error processing stencil: {ex.Message}");
        }
    }
}