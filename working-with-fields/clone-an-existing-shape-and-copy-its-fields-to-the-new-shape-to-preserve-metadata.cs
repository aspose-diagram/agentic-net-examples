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
                string inputPath = "source.vsdx";
                // Path to the output Visio file
                string outputPath = "cloned_output.vsdx";

                // Load the existing diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Find the shape to clone (for example, the shape with ID 1)
                // Adjust the ID as needed for your scenario
                long originalShapeId = 1;
                Shape originalShape = page.Shapes.GetShape(originalShapeId);

                // Retrieve master name from the original shape
                string masterName = originalShape.Master?.Name;
                if (string.IsNullOrEmpty(masterName))
                {
                    throw new Exception("Original shape does not have an associated master.");
                }

                // Use the same position as the original shape for the cloned shape
                double pinX = originalShape.XForm.PinX.Value;
                double pinY = originalShape.XForm.PinY.Value;

                // Add a new shape using the same master; the fourth parameter isCalculate must be a bool
                long newShapeId = page.AddShape(pinX + 1.0, pinY + 1.0, masterName, false); // offset slightly to avoid overlap
                Shape clonedShape = page.Shapes.GetShape(newShapeId);

                // Copy all fields (metadata) from the original shape to the cloned shape
                foreach (Field originalField in originalShape.Fields)
                {
                    Field newField = new Field();

                    // Copy field value
                    newField.Value.Val = originalField.Value.Val;

                    // Copy field format
                    newField.Format.Val = originalField.Format.Val;

                    // Copy field type
                    newField.Type.Value = originalField.Type.Value;

                    // Copy field calendar (if any)
                    newField.Calendar.Value = originalField.Calendar.Value;

                    // Copy deletion flag
                    newField.Del = originalField.Del;

                    // Add the new field to the cloned shape
                    clonedShape.Fields.Add(newField);
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