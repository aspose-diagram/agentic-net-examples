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
                // Path where the modified Visio file will be saved
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Example: remove a custom property named "UnusedField" from a shape with a specific ID
                long targetShapeId = 1; // replace with the actual shape ID
                string propertyNameToRemove = "UnusedField";

                // Retrieve the page that contains the shape (assumes first page)
                Page page = diagram.Pages[0];

                // Get the shape by its ID
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape != null)
                {
                    RemovePropByName(shape, propertyNameToRemove);
                }
                else
                {
                    Console.WriteLine($"Shape with ID {targetShapeId} not found.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Removes a custom property (Prop) from the specified shape by its name.
        /// </summary>
        /// <param name="shape">The shape from which to remove the property.</param>
        /// <param name="propName">The name of the property to remove.</param>
        static void RemovePropByName(Shape shape, string propName)
        {
            if (shape.Props == null)
                return;

            // Collect matching properties
            var toRemove = new System.Collections.Generic.List<Prop>();
            foreach (Prop prop in shape.Props)
            {
                if (prop.Name == propName)
                {
                    toRemove.Add(prop);
                }
            }

            // Remove the collected properties
            foreach (Prop prop in toRemove)
            {
                shape.Props.Remove(prop);
                Console.WriteLine($"Removed property '{propName}' from shape ID {shape.ID}.");
            }
        }
    }