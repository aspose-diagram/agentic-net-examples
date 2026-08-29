using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Draw a rectangle shape on the page
        // Parameters: pinX, pinY, width, height (all in inches)
        long rectId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

        // Retrieve the shape object from the returned ID
        Shape rectShape = page.Shapes.GetShape(rectId);

        // Create first user‑defined cell
        User customInt = new User();
        customInt.Name = "CustomInt";               // Row name
        customInt.Value.Val = "123";                // Cell value as string
        customInt.Prompt.Value = "An integer value"; // Optional description

        // Create second user‑defined cell
        User customString = new User();
        customString.Name = "CustomString";
        customString.Value.Val = "SampleText";
        customString.Prompt.Value = "A string value";

        // Add the user‑defined cells to the shape's Users collection
        rectShape.Users.Add(customInt);
        rectShape.Users.Add(customString);

        // Save the diagram as a VSDX template file
        string outputPath = "TemplateDiagram.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
