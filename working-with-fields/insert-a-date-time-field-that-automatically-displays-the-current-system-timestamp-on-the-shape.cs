using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a rectangle shape to the active page at position (2,2) inches
                long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the concrete Shape object using the returned ID
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Create a new field that will display the current system date/time
                Field dateTimeField = new Field();

                // Assign a Visio formula that returns the current date and time.
                // The formula is evaluated at render time, so the shape always shows the latest timestamp.
                dateTimeField.Value.Ufev.F = "NOW()";

                // Optionally clear any static value; the formula will provide the content.
                dateTimeField.Value.Val = "";

                // Add the field to the shape's Fields collection
                shape.Fields.Add(dateTimeField);

                // Save the diagram to a VSDX file
                diagram.Save("DateTimeShape.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }