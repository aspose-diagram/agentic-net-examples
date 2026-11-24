using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);
            Console.WriteLine($"Loaded diagram from '{inputPath}'.");

            // Iterate through all pages.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    Console.WriteLine($"Processing Shape ID: {shape.ID}");

                    // ---------- Add a new field ----------
                    Aspose.Diagram.Field newField = new Aspose.Diagram.Field();
                    // Set the field's value.
                    newField.Value.Val = "NewFieldValue";
                    // Insert the field into the shape.
                    shape.Fields.Add(newField);
                    Console.WriteLine($"Added field (IX={newField.IX}) to Shape ID {shape.ID}.");

                    // ---------- Modify an existing field ----------
                    if (shape.Fields.Count > 0)
                    {
                        // Access the first field in the collection.
                        Aspose.Diagram.Field firstField = shape.Fields[0];
                        string oldValue = firstField.Value.Val;
                        // Update the field's value.
                        firstField.Value.Val = "ModifiedValue";
                        Console.WriteLine($"Modified field IX={firstField.IX} of Shape ID {shape.ID} from '{oldValue}' to '{firstField.Value.Val}'.");
                    }
                    else
                    {
                        Console.WriteLine($"Shape ID {shape.ID} has no fields to modify.");
                    }

                    // ---------- Remove the field that was added ----------
                    shape.Fields.Remove(newField);
                    Console.WriteLine($"Removed field (IX={newField.IX}) from Shape ID {shape.ID}.");
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}