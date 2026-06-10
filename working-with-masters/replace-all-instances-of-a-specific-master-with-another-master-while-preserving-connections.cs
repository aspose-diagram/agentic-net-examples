using System.IO;
using System;
using Aspose.Diagram;

class ReplaceMasterExample
{
    static void Main()
    {
        try
        {

            // Paths to the source diagram and the stencil/template that contains the replacement master
            string sourceDiagramPath = "input.vdx";
            string templatePath = "template.vst";

            // Names of the master to be replaced and the master that will replace it
            string oldMasterName = "OldMaster";
            string newMasterName = "NewMaster";

            // Load the existing diagram (lifecycle rule: load)
            Diagram diagram = new Diagram(sourceDiagramPath);

            // Add the replacement master from the template file (lifecycle rule: AddMaster)
            int newMasterId = diagram.AddMaster(templatePath, newMasterName);
            Master newMaster = diagram.Masters.GetMaster(newMasterId);

            // Retrieve the master that needs to be replaced
            Master oldMaster = diagram.Masters.GetMasterByName(oldMasterName);
            if (oldMaster == null)
            {
                Console.WriteLine($"Master '{oldMasterName}' not found.");
                return;
            }

            // Iterate through all pages and shapes, swapping the master reference while keeping connections intact
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape is an instance of the old master
                    if (shape.Master != null && shape.Master.ID == oldMaster.ID)
                    {
                        // Replace the master; shape.Connects (connections) remain unchanged
                        shape.Master = newMaster;
                    }
                }
            }

            // Remove the old master from the diagram's master collection (lifecycle rule: Remove)
            diagram.Masters.Remove(oldMaster);

            // Save the modified diagram (lifecycle rule: save)
            diagram.Save("output.vdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
