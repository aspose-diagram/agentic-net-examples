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
                // Path for the modified Visio file
                string outputPath = "output.vsdx";
                // Name of the shape to locate (use the Name property)
                string targetShapeName = "TargetShape";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Flag to indicate whether the shape was found
                bool shapeFound = false;

                // Iterate through all pages and shapes to locate the target shape by its Name property
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Name == targetShapeName)
                        {
                            // Create a new field
                            Field field = new Field();

                            // Set the field's value (text to be inserted)
                            field.Value.Val = "Inserted Value";

                            // Add the field to the shape's Fields collection
                            shape.Fields.Add(field);

                            shapeFound = true;
                            break; // Exit inner loop once the shape is processed
                        }
                    }

                    if (shapeFound)
                        break; // Exit outer loop if the shape has been found
                }

                // If the shape was not found, raise an exception
                if (!shapeFound)
                    throw new Exception($"Shape with Name \"{targetShapeName}\" was not found in the diagram.");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }