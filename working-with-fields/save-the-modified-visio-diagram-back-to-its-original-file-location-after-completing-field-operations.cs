using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Fields collection before accessing.
                        if (shape.Fields != null && shape.Fields.Count > 0)
                        {
                            // Update each existing field's value.
                            foreach (Field field in shape.Fields)
                            {
                                // Assign a new value to the field.
                                field.Value.Val = "Updated";
                            }

                            // Example removal: remove the first field if more than one exists.
                            if (shape.Fields.Count > 1)
                            {
                                // Remove field at index 0 (first field).
                                shape.Fields.RemoveAt(0);
                            }

                            // Example insertion: add a new custom field.
                            // Create a new Field instance and set minimal required properties.
                            Field newField = new Field();
                            // Set the field index (IX) to the next available position.
                            newField.IX = shape.Fields.Count + 1;
                            // Set the field value.
                            newField.Value.Val = "NewField";
                            // Set the field type to Undefined (safe default).
                            newField.Type.Value = TypeFieldValue.Undefined;
                            // Add the new field to the shape's Fields collection.
                            shape.Fields.Add(newField);
                        }
                    }
                }

                // Save the modified diagram back to its original location.
                // The diagram is saved in VSDX format regardless of original format.
                diagram.Save(inputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}