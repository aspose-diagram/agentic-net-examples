using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Define output path (same directory, new file name).
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "output_modified.vsdx");

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);
            Console.WriteLine($"Loaded diagram \"{inputPath}\" successfully.");

            // Iterate through each page.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // ---------- Add a new field ----------
                    Field newField = new Field();
                    newField.Value.Val = "InitialValue";                     // Set initial value.
                    newField.Type.Value = TypeFieldValue.Undefined;         // Set type (using enum directly).
                    newField.Del = BOOL.False;                              // Not marked for deletion.

                    // Add the field to the shape.
                    shape.Fields.Add(newField);
                    Console.WriteLine($"[Add] Shape ID {shape.ID}: Added field IX {newField.IX} with value \"{newField.Value.Val}\".");

                    // ---------- Modify the first existing field (if any) ----------
                    if (shape.Fields.Count > 0)
                    {
                        // Retrieve the first field (could be the one just added or an existing one).
                        Field existingField = shape.Fields[0];
                        string oldValue = existingField.Value.Val;

                        // Update the field's value.
                        existingField.Value.Val = "ModifiedValue";
                        Console.WriteLine($"[Modify] Shape ID {shape.ID}: Field IX {existingField.IX} value changed from \"{oldValue}\" to \"{existingField.Value.Val}\".");
                    }
                    else
                    {
                        Console.WriteLine($"[Modify] Shape ID {shape.ID}: No fields available to modify.");
                    }

                    // ---------- Remove the field we just added ----------
                    // Attempt to remove the specific field instance.
                    bool removed = false;
                    // Ensure the field still exists in the collection before removal.
                    foreach (Field f in shape.Fields)
                    {
                        if (f == newField)
                        {
                            shape.Fields.Remove(newField);
                            removed = true;
                            Console.WriteLine($"[Remove] Shape ID {shape.ID}: Removed field IX {newField.IX}.");
                            break;
                        }
                    }

                    if (!removed)
                    {
                        Console.WriteLine($"[Remove] Shape ID {shape.ID}: Field to remove not found.");
                    }
                }
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}