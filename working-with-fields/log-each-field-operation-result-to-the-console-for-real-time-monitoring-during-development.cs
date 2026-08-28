using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Define input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
                Console.WriteLine($"Diagram loaded from '{inputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Iterate through each page
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                Console.WriteLine($"Processing Page ID: {page.ID}, Name: {page.NameU}");

                // Iterate through each shape on the page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    long shapeId = shape.ID;
                    Console.WriteLine($"  Shape ID: {shapeId}, NameU: {shape.NameU}");

                    // Log existing fields
                    if (shape.Fields != null && shape.Fields.Count > 0)
                    {
                        Console.WriteLine($"    Existing Fields Count: {shape.Fields.Count}");
                        foreach (Aspose.Diagram.Field field in shape.Fields)
                        {
                            // Field index
                            int ix = field.IX;
                            // Field value (string)
                            string value = field.Value != null ? field.Value.Val : "<null>";
                            Console.WriteLine($"      Field IX: {ix}, Value: '{value}'");
                        }
                    }
                    else
                    {
                        Console.WriteLine("    No existing fields.");
                    }

                    // Add a new field to the shape
                    Aspose.Diagram.Field newField = new Aspose.Diagram.Field();
                    // Set a simple value for demonstration
                    if (newField.Value != null)
                    {
                        newField.Value.Val = "NewFieldValue";
                    }
                    shape.Fields.Add(newField);
                    Console.WriteLine("    Added new field with value 'NewFieldValue'.");

                    // Update the first field if it exists
                    if (shape.Fields.Count > 0)
                    {
                        Aspose.Diagram.Field firstField = shape.Fields[0];
                        if (firstField.Value != null)
                        {
                            firstField.Value.Val = "UpdatedValue";
                            Console.WriteLine($"    Updated first field (IX={firstField.IX}) to 'UpdatedValue'.");
                        }
                    }

                    // Remove the field we just added (last field)
                    int lastIndex = shape.Fields.Count - 1;
                    if (lastIndex >= 0)
                    {
                        Aspose.Diagram.Field fieldToRemove = shape.Fields[lastIndex];
                        shape.Fields.Remove(fieldToRemove);
                        Console.WriteLine($"    Removed field at index {lastIndex} (IX={fieldToRemove.IX}).");
                    }
                }
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }