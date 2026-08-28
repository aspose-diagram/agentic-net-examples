using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output PDF file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <input.vsdx> <output.pdf>");
            return;
        }

        // Assign input and output paths.
        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the minimum required Aspose.Diagram version (major.minor.build.revision).
        // Adjust this string to the version you need.
        string requiredVersionString = "23.10.0.0";

        // Retrieve the actual library version from the assembly.
        Version actualVersion = typeof(Diagram).Assembly.GetName().Version;
        Version requiredVersion = new Version(requiredVersionString);

        // Compare versions; abort if the library is older than required.
        if (actualVersion < requiredVersion)
        {
            Console.Error.WriteLine($"Aspose.Diagram version {actualVersion} is older than required {requiredVersion}. Aborting conversion.");
            return;
        }

        // Proceed with conversion inside a try/catch to handle Aspose-specific errors.
        try
        {
            // Load the Visio diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram as PDF to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Pdf);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}