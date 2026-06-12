using System.IO;
using System;
using Aspose.Diagram;

class AssignDefaultMaster
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = @"C:\Visio\input.vsdx";

            // Output Visio file path
            string outputPath = @"C:\Visio\output.vsdx";

            // Path to a stencil/template that contains the default master (e.g., Basic_U.vssx)
            string stencilPath = @"C:\Visio\Stencils\Basic_U.vssx";

            // Name of the default master to assign (must exist in the stencil)
            string defaultMasterName = "Rectangle";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Try to locate the default master in the current document
            int defaultMasterId = -1;
            foreach (Master m in diagram.Masters)
            {
                if (string.Equals(m.Name, defaultMasterName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(m.NameU, defaultMasterName, StringComparison.OrdinalIgnoreCase))
                {
                    defaultMasterId = m.ID;
                    break;
                }
            }

            // If the master is not present, add it from the stencil file
            if (defaultMasterId == -1)
            {
                // AddMaster returns the ID of the newly added master
                defaultMasterId = diagram.AddMaster(stencilPath, defaultMasterName);
            }

            // Retrieve the Master object to assign
            Master defaultMaster = diagram.Masters[defaultMasterId];

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Shapes without an assigned master have Master == null
                    if (shape.Master == null)
                    {
                        // Assign the default master
                        shape.Master = defaultMaster;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
