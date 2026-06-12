using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a blank page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Draw a simple rectangle shape on the page
            // Parameters: PinX, PinY, Width, Height
            long shapeId = page.DrawRectangle(2.0, 2.0, 3.0, 1.5);
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Create first user‑defined cell
            User customCell1 = new User();
            customCell1.Name = "CustomField1";               // Row name
            customCell1.Value.Val = "DefaultValue1";         // Cell value
            customCell1.Prompt.Value = "First custom field"; // Optional description
            shape.Users.Add(customCell1);

            // Create second user‑defined cell
            User customCell2 = new User();
            customCell2.Name = "CustomField2";
            customCell2.Value.Val = "12345";
            customCell2.Prompt.Value = "Second custom field (numeric)";
            shape.Users.Add(customCell2);

            // Save the diagram as a VSDX template file
            diagram.Save("TemplateDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }