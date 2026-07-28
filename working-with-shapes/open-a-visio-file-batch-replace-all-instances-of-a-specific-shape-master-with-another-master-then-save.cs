using System.IO;
using System;
using Aspose.Diagram;

class ReplaceMasterExample
{
    static void Main()
    {
        try
        {

            // Input Visio file
            string inputPath = "input.vsdx";

            // Output Visio file
            string outputPath = "output.vsdx";

            // Name of the master to be replaced (as it appears in the source diagram)
            string oldMasterName = "OldMaster";

            // Stencil (or Visio file) that contains the replacement master
            string replacementStencilPath = "newMasters.vssx";

            // Name of the master that will replace the old one
            string newMasterName = "NewMaster";

            // Load the source diagram (uses the provided load rule)
            Diagram diagram = new Diagram(inputPath);

            // Ensure the replacement master is present in the diagram.
            // This uses the AddMaster(string, string) method (provided rule).
            int newMasterId = diagram.AddMaster(replacementStencilPath, newMasterName);

            // Iterate through all pages and shapes, swapping the master where needed.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape is an instance of the old master.
                    if (shape.Master != null &&
                        (string.Equals(shape.Master.Name, oldMasterName, StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(shape.Master.NameU, oldMasterName, StringComparison.OrdinalIgnoreCase)))
                    {
                        // Replace the master reference with the new master.
                        shape.Master = diagram.Masters[newMasterId];
                    }
                }
            }

            // Save the modified diagram (uses the provided save rule).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
