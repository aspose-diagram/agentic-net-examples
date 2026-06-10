using System;
using System.IO;
using Aspose.Diagram;

class ImportMastersExample
{
    static void Main()
    {
        try
        {

            // Paths to the target Visio document and the XML file containing master definitions
            string targetVisioPath = @"C:\Visio\TargetDiagram.vsdx";
            string mastersXmlPath = @"C:\Visio\MasterDefinitions.vdx";

            // Load the existing Visio document (target)
            Diagram targetDiagram = new Diagram(targetVisioPath);

            // Load the XML file that holds master definitions.
            // The file is a Visio VDX (XML) stencil/template, so specify the format explicitly.
            Diagram masterSourceDiagram = new Diagram(mastersXmlPath, LoadFileFormat.Vdx);

            // Iterate through each master in the source diagram and add it to the target diagram.
            foreach (Master srcMaster in masterSourceDiagram.Masters)
            {
                // AddMaster copies the master by its Name (or NameU) from the source diagram.
                // The method returns the unique ID of the added master in the target diagram.
                int addedMasterId = targetDiagram.AddMaster(masterSourceDiagram, srcMaster.Name);
                // Optional: you can use addedMasterId for further processing if needed.
            }

            // Save the updated Visio document.
            // Save as VSDX (Visio 2013+ format). Adjust the format if a different output is required.
            targetDiagram.Save(@"C:\Visio\TargetDiagram_WithMasters.vsdx", SaveFileFormat.Vsdx);

            // Clean up resources
            targetDiagram.Dispose();
            masterSourceDiagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
