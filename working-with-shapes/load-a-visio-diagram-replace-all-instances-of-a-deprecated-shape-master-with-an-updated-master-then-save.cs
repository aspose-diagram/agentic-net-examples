using System.IO;
using System;
using Aspose.Diagram;

class ReplaceMasterExample
{
    static void Main()
    {
        try
        {

            // Paths to the source diagram, the stencil containing the updated master, and the output file
            string sourceDiagramPath = "input.vsdx";
            string updatedStencilPath = "updatedStencil.vssx";
            string outputDiagramPath = "output.vsdx";

            // Names of the deprecated master and the updated master within the stencil
            string deprecatedMasterName = "OldMaster";
            string updatedMasterName = "NewMaster";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(sourceDiagramPath);

            // Add the updated master to the diagram from the stencil file
            // The method returns the unique ID of the added master in the diagram's Masters collection
            int newMasterId = diagram.AddMaster(updatedStencilPath, updatedMasterName);

            // Retrieve the newly added master using its ID
            Master newMaster = diagram.Masters[newMasterId];

            // Iterate through all pages and shapes, replacing the deprecated master with the new one
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape is based on a master and if that master matches the deprecated name
                    if (shape.Master != null && shape.Master.NameU == deprecatedMasterName)
                    {
                        // Replace the master reference with the updated master
                        shape.Master = newMaster;
                    }
                }
            }

            // Save the modified diagram to the specified output file (VDX format)
            diagram.Save(outputDiagramPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
