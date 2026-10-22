using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access the collection of custom document properties.
            CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

            // Locate the custom property named "ReviewDate".
            CustomProp propToRemove = null;
            foreach (CustomProp prop in customProps)
            {
                if (prop.Name == "ReviewDate")
                {
                    propToRemove = prop;
                    break;
                }
            }

            // Remove the property if it was found.
            if (propToRemove != null)
            {
                customProps.Remove(propToRemove);
                Console.WriteLine("Custom property 'ReviewDate' has been removed.");
            }
            else
            {
                Console.WriteLine("Custom property 'ReviewDate' was not found; no removal performed.");
            }

            // Save the updated diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources.
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}