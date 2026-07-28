using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vdx");

            // Path to a VST/VSD template that contains a master with the desired background image
            string masterTemplatePath = "BackgroundMaster.vst";
            if (!System.IO.File.Exists(masterTemplatePath))
            {
                Console.Error.WriteLine($"File not found: {masterTemplatePath}");
                return;
            }
            string masterName = "Background";   // Name of the master inside the template

            // Import the master from the template into the current diagram
            // Returns the unique ID of the added master (not used further here)
            int masterId = diagram.AddMaster(masterTemplatePath, masterName);

            // Apply the imported master to every page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Page numbers are 1‑based in Aspose.Diagram
                int pageNumber = i + 1;

                // Add the master shape to the page.
                // PinX and PinY are set to 0 (center of the page). Adjust as needed.
                diagram.AddShape(0.0, 0.0, masterName, pageNumber);
            }

            // Save the modified diagram
            diagram.Save("output.vdx", Aspose.Diagram.SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
