using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram (or load an existing one)
        Diagram diagram = new Diagram();

        // Example: add a simple rectangle shape so the HTML has content
        // (optional, can be omitted if not needed)
        // long shapeId = diagram.AddShape(2.0, 2.0, 4.0, 2.0, "Rectangle", 0);
        // Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
        // shape.Text.Value.Add(new Txt("Sample Shape"));

        // Configure HTML export options
        HTMLSaveOptions htmlOptions = new HTMLSaveOptions
        {
            ExportHiddenPage = false
        };

        // Pre‑create the output file stream
        using (FileStream fileStream = new FileStream("output.html", FileMode.Create, FileAccess.Write))
        {
            // Save the diagram as HTML directly to the stream
            diagram.Save(fileStream, htmlOptions);
        }

        Console.WriteLine("Diagram has been saved to output.html");
    }
}
