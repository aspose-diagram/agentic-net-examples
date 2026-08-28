using System.IO;
using System;
using Aspose.Diagram;

using Aspose.Diagram.Saving; // for SaveFileFormat if needed

class ImportMastersExample
{
    static void Main()
    {
        try
        {

            // Paths to the target Visio document and the XML file containing master definitions
            string targetVisioPath = "target.vsdx";
            string mastersXmlPath = "masters.xml";

            // Load the target Visio diagram
            Diagram targetDiagram = new Diagram(targetVisioPath);

            // Load the XML file that holds master definitions.
            // Assuming the XML follows the VDX (Visio XML) format.
            Diagram masterSourceDiagram = new Diagram(mastersXmlPath, LoadFileFormat.Vdx);

            // Iterate through each master in the source diagram and add it to the target diagram
            foreach (Master srcMaster in masterSourceDiagram.Masters)
            {
                // AddMaster copies the master by its Name (or NameU) from the source diagram
                // The method returns the unique ID of the added master in the target diagram.
                targetDiagram.AddMaster(masterSourceDiagram, srcMaster.Name);
            }

            // Save the updated diagram to a new file
            targetDiagram.Save("updated_output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
