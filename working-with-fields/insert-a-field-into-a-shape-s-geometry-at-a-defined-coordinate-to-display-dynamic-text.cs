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

                // Use the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape at coordinates (2,2)
                // The AddShape method returns the shape ID (long)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the concrete Shape object using the ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Ensure the shape is positioned at the desired location
                shape.XForm.PinX.Value = 2.0;
                shape.XForm.PinY.Value = 2.0;

                // Create a new text field that will display the current page number dynamically
                Field pageNumberField = new Field();

                // Set the field's formula to the Visio function that returns the page number
                // The formula is stored in the Ufev.F property of the field's Value object
                pageNumberField.Value.Ufev.F = "PageNum";

                // Optionally set a placeholder value (not displayed, but required by the API)
                pageNumberField.Value.Val = "";

                // Add the field to the shape's Fields collection
                shape.Fields.Add(pageNumberField);

                // Save the diagram to a VSDX file
                diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram created and field inserted successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }