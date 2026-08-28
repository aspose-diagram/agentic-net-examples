using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the original diagram file
        string sourceFile = "original.vsdx";

        // Path for the new file to preserve the original
        string newFile = "updated.vsdx";

        // Load the diagram from the original file
        using (Diagram diagram = new Diagram(sourceFile))
        {
            // TODO: Apply any updates to the diagram here

            // Save the updated diagram to a new file
            diagram.Save(newFile, SaveFileFormat.Vsdx);
        }
    }
}
