using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination diagrams
            string sourcePath = "source.vsdx";
            string destPath = "cloned.vsdx";

            // Load the original diagram
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Create a new empty diagram
            Diagram clonedDiagram = new Diagram();

            // Merge the source diagram into the new diagram (pages, masters, etc.)
            clonedDiagram.Combine(sourceDiagram);

            // Preserve custom document properties
            // Remove any existing custom properties in the target diagram
            clonedDiagram.DocumentProps.CustomProps.Clear();

            // Copy each custom property from the source diagram
            foreach (CustomProp prop in sourceDiagram.DocumentProps.CustomProps)
            {
                CustomProp newProp = new CustomProp();
                newProp.Name = prop.Name;
                newProp.PropType = prop.PropType;
                newProp.CustomValue.ValueString = prop.CustomValue.ValueString;
                clonedDiagram.DocumentProps.CustomProps.Add(newProp);
            }

            // Save the cloned diagram with all content and custom properties intact
            clonedDiagram.Save(destPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
