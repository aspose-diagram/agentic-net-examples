using System.IO;
using System;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Ensure a file path is provided
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to the Visio file as a command‑line argument.");
                return;
            }

            string filePath = args[0];

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Retrieve the embedded API version from the diagram
            string embeddedVersion = diagram.Version;

            // Retrieve the current Aspose.Diagram library version from the assembly
            Version assemblyVersion = typeof(Diagram).Assembly.GetName().Version;
            string libraryVersion = assemblyVersion != null ? assemblyVersion.ToString() : "unknown";

            Console.WriteLine($"Embedded diagram version: {embeddedVersion}");
            Console.WriteLine($"Current Aspose.Diagram library version: {libraryVersion}");

            // Compare versions and report mismatch
            if (!string.Equals(embeddedVersion, libraryVersion, StringComparison.Ordinal))
            {
                string message = "Version mismatch detected between diagram and library.";
                Console.WriteLine(message);
                throw new Exception(message);
            }
            else
            {
                Console.WriteLine("Version check passed. Diagram version matches library version.");
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
