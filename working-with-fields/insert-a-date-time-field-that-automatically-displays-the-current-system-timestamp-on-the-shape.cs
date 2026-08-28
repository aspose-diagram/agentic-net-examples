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

                // Use the first page (a default page is created automatically)
                Page page = diagram.Pages[0];

                // Add a rectangle shape at position (2,2) inches
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the concrete Shape object
                Shape shape = page.Shapes.GetShape(shapeId);

                // Create a new field that will display the current system timestamp
                Field dateTimeField = new Field();

                // Set the formula to the Visio NOW() function
                dateTimeField.Value.Ufev.F = "Now()";

                // Use undefined unit (required by the API)
                dateTimeField.Value.Ufev.Unit = MeasureConst.Undefined;

                // Optionally clear any format string
                dateTimeField.Format.Val = "";

                // Add the field to the shape's Fields collection
                shape.Fields.Add(dateTimeField);

                // Save the diagram to a VSDX file
                diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }