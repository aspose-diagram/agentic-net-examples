using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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

                // Define the page index and shape ID to work with
                int pageIndex = 0;               // first page
                long shapeId = 1;                // example shape ID

                // Retrieve the page and shape
                Page page = diagram.Pages[pageIndex];
                Shape shape = page.Shapes.GetShape(shapeId);

                // The value we want to ensure exists as a field
                string targetFieldValue = "MyCustomField";

                // Check if the field already exists on the shape
                bool fieldExists = false;
                foreach (Field field in shape.Fields)
                {
                    if (field.Value.Val == targetFieldValue)
                    {
                        fieldExists = true;
                        break;
                    }
                }

                // Add the field only if it does not already exist
                if (!fieldExists)
                {
                    Field newField = new Field();
                    newField.Value.Val = targetFieldValue;
                    shape.Fields.Add(newField);
                    Console.WriteLine($"Field '{targetFieldValue}' added to shape ID {shapeId}.");
                }
                else
                {
                    Console.WriteLine($"Field '{targetFieldValue}' already exists on shape ID {shapeId}.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }