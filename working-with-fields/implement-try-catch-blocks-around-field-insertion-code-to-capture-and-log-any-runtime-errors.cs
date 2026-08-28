using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram instance
                Diagram diagram = new Diagram();

                // Ensure there is at least one page (the default constructor creates one)
                var page = diagram.Pages[0];

                // Add a rectangle shape to the diagram; AddShape returns the shape ID (long)
                long shapeId = diagram.AddShape(1.0, 1.0, "Rectangle", 0);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Attempt to insert a text field into the shape
                try
                {
                    // Create a new field object
                    Field field = new Field();

                    // Set the field's displayed value
                    field.Value.Val = "Sample Text";

                    // Add the field to the shape's field collection
                    shape.Fields.Add(field);

                    Console.WriteLine("Field inserted successfully.");
                }
                catch (Exception ex)
                {
                    // Log any runtime errors that occur during field insertion
                    Console.WriteLine($"Error inserting field: {ex.Message}");
                }

                // Optional: save the diagram to verify changes (commented out as not required)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }