using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Name of the shape to modify
                string targetShapeName = "TargetShape";

                // Flag to indicate if the shape was found
                bool shapeFound = false;

                // Iterate through all pages and shapes to locate the shape by its Name property
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Name == targetShapeName)
                        {
                            // Create a new field
                            Field field = new Field();

                            // Set the field's value (plain text)
                            field.Value.Val = "Inserted Field Value";

                            // Optional: clear any existing format strings
                            field.Format.Val = "";
                            field.Format.Ufev.F = "";
                            field.Format.Ufev.Unit = MeasureConst.Undefined;

                            // Add the field to the shape's Fields collection
                            shape.Fields.Add(field);

                            shapeFound = true;
                            break; // Exit inner loop once the shape is processed
                        }
                    }

                    if (shapeFound)
                        break; // Exit outer loop if shape has been found
                }

                if (!shapeFound)
                {
                    Console.WriteLine($"Shape with Name \"{targetShapeName}\" was not found.");
                    return;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Field inserted and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }