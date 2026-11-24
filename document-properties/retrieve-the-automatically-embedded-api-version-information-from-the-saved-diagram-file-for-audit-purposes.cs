using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the automatically embedded API version information
                string version = diagram.Version;

                // Retrieve additional audit-related metadata from document properties
                string buildCreated = diagram.DocumentProps.BuildNumberCreated.ToString();
                string buildEdited = diagram.DocumentProps.BuildNumberEdited.ToString();

                // TimeCreated is a DateTime; convert to string explicitly
                DateTime timeCreatedDt = diagram.DocumentProps.TimeCreated;
                string timeCreated = timeCreatedDt.ToString();

                // Output the retrieved information to the console
                Console.WriteLine($"Diagram Version: {version}");
                Console.WriteLine($"Build Number Created: {buildCreated}");
                Console.WriteLine($"Build Number Edited: {buildEdited}");
                Console.WriteLine($"Time Created: {timeCreated}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to standard error
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}