using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Load the diagram and add a timestamp custom property to each shape.
        try
        {
            // Diagram does not implement IDisposable; instantiate directly.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over every page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over every shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Create a new custom property (Prop) for the timestamp.
                    Prop timestampProp = new Prop();

                    // Set the internal name of the property.
                    timestampProp.Name = "Timestamp";

                    // Set the display label of the property.
                    // Use .Value for Str2Value (Label) as required by the API.
                    timestampProp.Label.Value = "Timestamp";

                    // Assign the current timestamp in ISO‑8601 format.
                    timestampProp.Value.Val = DateTime.Now.ToString("o");

                    // Add the custom property to the shape's property collection.
                    shape.Props.Add(timestampProp);
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with timestamps to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
