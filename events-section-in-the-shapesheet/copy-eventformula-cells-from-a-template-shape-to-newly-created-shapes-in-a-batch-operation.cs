using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the template diagram that contains the source shape with Event formulas.
        string templatePath = "template.vsdx";
        if (!System.IO.File.Exists(templatePath))
        {
            Console.Error.WriteLine($"File not found: {templatePath}");
            return;
        }
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Path where the resulting diagram will be saved.
        string outputPath = "output.vsdx";

        try
        {
            // Load the template diagram.
            Diagram templateDiagram = new Diagram(templatePath);

            // Validate that the template has at least one page and one shape.
            if (templateDiagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Template diagram contains no pages.");
                return;
            }

            if (templateDiagram.Pages[0].Shapes.Count == 0)
            {
                Console.Error.WriteLine("Template diagram's first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the first page as the source for Event formulas.
            Shape templateShape = templateDiagram.Pages[0].Shapes[0];

            // Store the Event formulas from the template shape.
            string eventDropFormula = templateShape.Event.EventDrop.Ufe.F;
            string eventDblClickFormula = templateShape.Event.EventDblClick.Ufe.F;
            string eventXFModFormula = templateShape.Event.EventXFMod.Ufe.F;
            string eventMultiDropFormula = templateShape.Event.EventMultiDrop.Ufe.F;
            string theTextFormula = templateShape.Event.TheText.Ufe.F;
            string theDataFormula = templateShape.Event.TheData.Ufe.F;

            // Determine the master name of the template shape (used when creating new shapes).
            string masterName = templateShape.Master != null ? templateShape.Master.Name : "Rectangle";

            // Create a new empty diagram.
            Diagram newDiagram = new Diagram();

            // Add a new page to the diagram.
            Page newPage = new Page();
            newDiagram.Pages.Add(newPage);

            // Parameters for batch shape creation.
            int shapeCount = 5;               // Number of shapes to create.
            double startX = 1.0;              // Starting X coordinate.
            double startY = 1.0;              // Starting Y coordinate.
            double offsetX = 2.0;             // Horizontal offset between shapes.
            double shapeWidth = 1.0;          // Width of each new shape.
            double shapeHeight = 1.0;         // Height of each new shape.

            // Loop to create shapes and copy Event formulas.
            for (int i = 0; i < shapeCount; i++)
            {
                // Calculate position for the new shape.
                double pinX = startX + i * offsetX;
                double pinY = startY;

                // Add a new shape using the identified master.
                // The overload returns a long shape ID.
                long shapeId = newPage.AddShape(pinX, pinY, shapeWidth, shapeHeight, masterName, false);

                // Retrieve the shape instance using the returned ID.
                Shape newShape = newPage.Shapes.GetShape(shapeId);

                // Copy each Event formula from the template shape to the new shape.
                newShape.Event.EventDrop.Ufe.F = eventDropFormula;
                newShape.Event.EventDblClick.Ufe.F = eventDblClickFormula;
                newShape.Event.EventXFMod.Ufe.F = eventXFModFormula;
                newShape.Event.EventMultiDrop.Ufe.F = eventMultiDropFormula;
                newShape.Event.TheText.Ufe.F = theTextFormula;
                newShape.Event.TheData.Ufe.F = theDataFormula;
            }

            // Save the newly created diagram.
            newDiagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}