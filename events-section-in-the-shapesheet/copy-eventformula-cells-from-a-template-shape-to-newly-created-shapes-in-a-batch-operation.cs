using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string templatePath = "template.vsdx";
        if (!System.IO.File.Exists(templatePath))
        {
            Console.Error.WriteLine($"File not found: {templatePath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the template file exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        try
        {
            // Load the source diagram containing the template shape
            Diagram diagram = new Diagram(templatePath);

            // Assume the first page contains the template shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Guard: ensure the page has at least one shape to use as a template
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes to use as a template.");
                return;
            }

            // Retrieve the template shape (first shape on the page)
            Shape templateShape = page.Shapes[0];

            // Store event formulas from the template shape
            string eventDropFormula = templateShape.Event.EventDrop.Ufe.F;
            string eventDblClickFormula = templateShape.Event.EventDblClick.Ufe.F;
            string eventXFModFormula = templateShape.Event.EventXFMod.Ufe.F;
            string eventMultiDropFormula = templateShape.Event.EventMultiDrop.Ufe.F;
            string theTextFormula = templateShape.Event.TheText.Ufe.F;

            // Determine the master name to use for new shapes (use the same master as the template)
            string masterName = templateShape.Master != null ? templateShape.Master.Name : "Rectangle";

            // Batch create new shapes and copy event formulas
            int numberOfNewShapes = 5; // Example: create 5 new shapes
            double startX = 2.0; // Starting X coordinate
            double startY = 2.0; // Starting Y coordinate
            double offsetX = 2.0; // Horizontal offset between shapes

            for (int i = 0; i < numberOfNewShapes; i++)
            {
                // Calculate position for the new shape
                double pinX = startX + i * offsetX;
                double pinY = startY;

                // Add a new shape to the page using the determined master
                long newShapeId = page.AddShape(pinX, pinY, masterName);

                // Retrieve the newly added shape instance
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Copy event formulas from the template shape to the new shape
                newShape.Event.EventDrop.Ufe.F = eventDropFormula;
                newShape.Event.EventDblClick.Ufe.F = eventDblClickFormula;
                newShape.Event.EventXFMod.Ufe.F = eventXFModFormula;
                newShape.Event.EventMultiDrop.Ufe.F = eventMultiDropFormula;
                newShape.Event.TheText.Ufe.F = theTextFormula;
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}