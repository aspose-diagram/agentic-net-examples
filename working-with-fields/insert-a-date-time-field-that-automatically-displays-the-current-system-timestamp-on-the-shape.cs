using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a rectangle shape to the active page
                // Parameters: PinX, PinY, master name ("Rectangle")
                long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the concrete Shape object using the returned ID
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Create a new field that will display the current system date/time
                // The formula "NOW()" returns the current timestamp in Visio
                Field dateTimeField = new Field();
                dateTimeField.Value.Val = "";                 // Display value (empty, will be filled by formula)
                dateTimeField.Value.Ufev.F = "NOW()";         // Set the formula
                dateTimeField.Value.Ufev.Unit = MeasureConst.Undefined; // No specific unit

                // Add the field to the shape's Fields collection
                shape.Fields.Add(dateTimeField);

                // Save the diagram to a VSDX file
                diagram.Save("DateTimeFieldDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }