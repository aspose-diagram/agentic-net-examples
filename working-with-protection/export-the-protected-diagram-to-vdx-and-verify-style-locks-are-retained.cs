using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportProtectedDiagram
{
    static void Main()
    {
        try
        {

            // Load the protected Visio diagram from file
            // (uses the Diagram(string) constructor – lifecycle rule)
            var diagram = new Diagram("protected_input.vsdx");

            // Verify that style locks (Protection settings) are retained
            Console.WriteLine("Style sheet protection flags:");
            foreach (StyleSheet styleSheet in diagram.StyleSheets)
            {
                var protection = styleSheet.Protection;
                // Example: check a few common lock flags
                Console.WriteLine($"StyleSheet: {styleSheet.NameU}");
                Console.WriteLine($"  LockFormat: {protection.LockFormat}");
                Console.WriteLine($"  LockMoveX: {protection.LockMoveX}");
                Console.WriteLine($"  LockMoveY: {protection.LockMoveY}");
                Console.WriteLine($"  LockRotate: {protection.LockRotate}");
                Console.WriteLine($"  LockTextEdit: {protection.LockTextEdit}");
            }

            // Save the diagram to VDX format while preserving all settings
            var saveOptions = new DiagramSaveOptions
            {
                SaveFormat = SaveFileFormat.Vdx
            };
            diagram.Save("exported_output.vdx", saveOptions);

            // Clean up
            diagram.Dispose();
            Console.WriteLine("Diagram exported to VDX with style locks retained.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
