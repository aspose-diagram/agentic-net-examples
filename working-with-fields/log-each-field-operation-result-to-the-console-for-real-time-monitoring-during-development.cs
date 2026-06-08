using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("No pages found in the diagram.");
                    return;
                }

                // Work with the first page
                Aspose.Diagram.Page page = diagram.Pages[0];

                // Ensure there is at least one shape; if not, create a rectangle shape
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("No shapes found on the first page. Adding a rectangle shape.");
                    long rectId = page.DrawRectangle(1.0, 1.0, 3.0, 2.0);
                    // Retrieve the newly created shape
                    Aspose.Diagram.Shape rectShape = page.Shapes.GetShape(rectId);
                    // Continue processing with the newly created shape
                    ProcessShapeFields(rectShape);
                }
                else
                {
                    // Process each shape on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        ProcessShapeFields(shape);
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Handles field operations and logs each step
        private static void ProcessShapeFields(Aspose.Diagram.Shape shape)
        {
            Console.WriteLine($"Processing Shape ID: {shape.ID}, Name: {shape.NameU}");

            // Log existing fields
            if (shape.Fields != null && shape.Fields.Count > 0)
            {
                Console.WriteLine($"  Existing fields count: {shape.Fields.Count}");
                for (int i = 0; i < shape.Fields.Count; i++)
                {
                    Aspose.Diagram.Field existingField = shape.Fields[i];
                    Console.WriteLine($"    Field[{i}] Value: {existingField.Value.Val}");
                }
            }
            else
            {
                Console.WriteLine("  No existing fields.");
            }

            // Add a new field
            Aspose.Diagram.Field newField = new Field();
            newField.Value.Val = "NewFieldValue";
            shape.Fields.Add(newField);
            Console.WriteLine("  Added new field with value 'NewFieldValue'.");

            // Verify addition
            Console.WriteLine($"  Fields count after addition: {shape.Fields.Count}");

            // Update the first field if it exists
            if (shape.Fields.Count > 0)
            {
                Aspose.Diagram.Field firstField = shape.Fields[0];
                string oldValue = firstField.Value.Val;
                firstField.Value.Val = "UpdatedValue";
                Console.WriteLine($"  Updated first field from '{oldValue}' to '{firstField.Value.Val}'.");
            }

            // Remove the field we just added (by reference)
            shape.Fields.Remove(newField);
            Console.WriteLine("  Removed the newly added field.");

            // Verify removal
            Console.WriteLine($"  Fields count after removal: {shape.Fields.Count}");
        }
    }