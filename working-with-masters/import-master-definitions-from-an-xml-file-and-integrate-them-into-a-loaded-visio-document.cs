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

            // Path to the existing Visio document that will receive the masters
            string targetDiagramPath = "target.vsdx";

            // Path to the XML file that contains master definitions (saved as a Visio diagram)
            string masterDefinitionsPath = "masters.xml";

            // Path where the updated diagram will be saved
            string outputDiagramPath = "output.vsdx";

            // Load the target diagram
            Diagram targetDiagram = new Diagram(targetDiagramPath);

            // Load the diagram that holds the master definitions (XML format is supported)
            Diagram masterDiagram = new Diagram(masterDefinitionsPath);

            // Iterate through each master in the source diagram
            foreach (Master sourceMaster in masterDiagram.Masters)
            {
                // Check if a master with the same name already exists in the target diagram
                if (!targetDiagram.Masters.IsExist(sourceMaster.Name))
                {
                    // Import the master from the source diagram into the target diagram
                    // Overload that accepts a source Diagram instance and the master name
                    targetDiagram.AddMaster(masterDiagram, sourceMaster.Name);
                }
            }

            // Save the updated diagram with the newly imported masters
            targetDiagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
