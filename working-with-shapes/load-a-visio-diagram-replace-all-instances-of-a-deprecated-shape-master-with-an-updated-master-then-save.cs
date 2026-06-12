using System.IO;
using System;
using Aspose.Diagram;

class ReplaceMasterExample
{
    static void Main()
    {
        try
        {

            // Paths to the source diagram, the stencil that contains the updated master,
            // and the output diagram.
            string sourceDiagramPath = "input.vsdx";
            string updatedMasterStencilPath = "updatedMasters.vssx";
            string outputDiagramPath = "output.vsdx";

            // Names of the deprecated master and the replacement master.
            string deprecatedMasterName = "OldMaster";
            string replacementMasterName = "NewMaster";

            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(sourceDiagramPath))
            {
                // Add the replacement master from the stencil file to the diagram.
                // The method returns the unique ID of the master inside the diagram.
                int replacementMasterId = diagram.AddMaster(updatedMasterStencilPath, replacementMasterName);

                // Retrieve the Master object that was just added.
                Master replacementMaster = diagram.Masters[replacementMasterId];

                // Iterate through all pages and shapes, replacing the master where needed.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape is based on a master and compare its universal name.
                        if (shape.Master != null && shape.Master.NameU == deprecatedMasterName)
                        {
                            // Replace the master reference with the new master.
                            shape.Master = replacementMaster;
                        }
                    }
                }

                // Save the modified diagram to a new file.
                diagram.Save(outputDiagramPath, SaveFileFormat.Vdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
