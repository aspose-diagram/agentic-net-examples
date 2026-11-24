using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Add a new rectangle shape to the first page
            // Parameters: page index (1‑based), master ID, master name, and shape ID (0 for auto‑assign)
            long shapeId = diagram.AddShape(1, 1, "Rectangle", 0);
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // ----- Begin field insertion with error handling -----
            try
            {
                // Create a new text field
                Field field = new Field();

                // Set the field's displayed value
                field.Value.Val = "Inserted Field Text";

                // Optionally, set format or type here if needed
                // field.Format.Val = "";
                // field.Type.Value = TypeFieldValue.Undefined; // example

                // Insert the field into the shape
                shape.Fields.Add(field);

                Console.WriteLine("Field inserted successfully.");
            }
            catch (Exception ex)
            {
                // Log any runtime errors that occur during field insertion
                Console.Error.WriteLine($"Error inserting field: {ex.Message}");
            }
            // ----- End field insertion -----

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors that occur while processing the diagram
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}