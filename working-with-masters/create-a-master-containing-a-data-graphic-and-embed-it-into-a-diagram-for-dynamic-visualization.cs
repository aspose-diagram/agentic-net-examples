using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the template diagram that already contains a master with a data graphic.
            // The template file (e.g., .vsdx) must have a master named "DataGraphicMaster".
            string templatePath = "DataGraphicTemplate.vsdx";
            Diagram templateDiagram = new Diagram(templatePath);

            // Create a new, empty diagram.
            Diagram diagram = new Diagram();

            // Add the master from the template into the new diagram by its name.
            // The method returns the unique ID of the added master in the target diagram.
            string masterName = "DataGraphicMaster";
            int masterId = diagram.AddMaster(templateDiagram, masterName);

            // Place an instance of the master on the active page.
            // PinX and PinY define the position (in the document's units).
            double pinX = 5.0;
            double pinY = 5.0;
            diagram.AddShape(pinX, pinY, masterName, masterId);

            // Save the resulting diagram to a file.
            string outputPath = "ResultDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
