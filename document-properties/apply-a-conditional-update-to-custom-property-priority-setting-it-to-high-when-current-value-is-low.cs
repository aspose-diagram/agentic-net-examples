using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Access document custom properties.
            CustomPropCollection customProps = diagram.DocumentProps.CustomProps;

            // Flag to indicate whether an update was performed.
            bool updated = false;

            // Iterate through custom properties to find "Priority".
            foreach (CustomProp prop in customProps)
            {
                // Check property name.
                if (prop.Name == "Priority")
                {
                    // Compare current value; use CustomValue.ValueString as per API.
                    if (prop.CustomValue != null && prop.CustomValue.ValueString == "Low")
                    {
                        // Update the value to "High".
                        prop.CustomValue.ValueString = "High";
                        updated = true;
                        Console.WriteLine("Custom property 'Priority' updated from 'Low' to 'High'.");
                    }
                    else
                    {
                        Console.WriteLine("Custom property 'Priority' does not have the value 'Low'; no change applied.");
                    }
                    // Since property names are unique, break after handling.
                    break;
                }
            }

            if (!updated)
            {
                Console.WriteLine("No matching custom property 'Priority' with value 'Low' was found.");
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}