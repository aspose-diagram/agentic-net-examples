using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page to work with
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page
            Page page = diagram.Pages[0];

            // Add a simple rectangle shape to the page
            // Parameters: pinX, pinY (position), master name, isCalculate flag
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the newly added shape object
            Shape shape = page.Shapes.GetShape(shapeId);

            // Prepare XML data to store in the custom property
            string xmlData = "<Root><Item id=\"1\">Value</Item></Root>";

            // Create a new custom property (Prop) for the shape
            Prop prop = new Prop();
            prop.Name = "XmlData";                     // Internal name of the property
            prop.Label.Value = "XML Data";             // Display label
            prop.Value.Val = xmlData;                  // Store the XML string
            prop.Type.Value = TypePropValue.String;   // Define the property type as string

            // Add the custom property to the shape's property collection
            shape.Props.Add(prop);

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}