using System.IO;
using System;
using Aspose.Diagram;

class ReplaceMasterShapes
{
    static void Main()
    {
        try
        {

            // Paths to the source Visio file, the stencil/template containing the new master,
            // and the output file.
            string sourceVisioPath = "input.vsdx";
            string masterTemplatePath = "template.vst";
            string outputVisioPath = "output.vsdx";

            // Names of the master to be replaced and the master that will replace it.
            string oldMasterName = "OldMaster";
            string newMasterName = "NewMaster";

            // Load the Visio diagram.
            Diagram diagram = new Diagram(sourceVisioPath);

            // Ensure the new master exists in the diagram.
            // AddMaster returns the unique ID of the master within the diagram.
            int newMasterId = diagram.AddMaster(masterTemplatePath, newMasterName);

            // Iterate through all pages and their shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape is an instance of a master and matches the old master name.
                    if (shape.Master != null && 
                        (shape.Master.Name == oldMasterName || shape.Master.NameU == oldMasterName))
                    {
                        // Replace the master reference with the new master.
                        shape.Master = diagram.Masters[newMasterId];
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputVisioPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
