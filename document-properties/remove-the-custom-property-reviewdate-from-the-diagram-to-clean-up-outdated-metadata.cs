using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // If no output path is provided, create one by appending "_cleaned" before the extension.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_cleaned" + Path.GetExtension(inputPath));

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the collection of custom document properties.
                CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

                // Find the custom property named "ReviewDate".
                CustomProp? propToRemove = null;
                for (int i = 0; i < customProps.Count; i++)
                {
                    CustomProp prop = customProps[i];
                    if (string.Equals(prop.Name, "ReviewDate", StringComparison.OrdinalIgnoreCase))
                    {
                        propToRemove = prop;
                        break;
                    }
                }

                // Remove the property if it was found.
                if (propToRemove != null)
                {
                    customProps.Remove(propToRemove);
                    Console.WriteLine("Custom property 'ReviewDate' removed.");
                }
                else
                {
                    Console.WriteLine("Custom property 'ReviewDate' not found; no changes made.");
                }

                // Save the updated diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}