using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Get the first shape on the first page
                Shape shape = page.Shapes[0];

                // Insert a new text field into the shape with error handling
                try
                {
                    // Create a new field
                    Field field = new Field();

                    // Set the field type (Undefined is a safe default)
                    field.Type.Value = TypeFieldValue.Undefined;

                    // Set the field value (the visible text)
                    field.Value.Val = "Sample Text";

                    // Add the field to the shape's field collection
                    shape.Fields.Add(field);

                    Console.WriteLine("Field inserted successfully.");
                }
                catch (Exception ex)
                {
                    // Log any runtime errors that occur during field insertion
                    Console.WriteLine($"Error inserting field: {ex.Message}");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }