using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Collect locked protection properties for logging
                    var lockedProperties = new System.Collections.Generic.List<string>();

                    if (shape.Protection.LockMoveX.Value == BOOL.True) lockedProperties.Add("LockMoveX");
                    if (shape.Protection.LockMoveY.Value == BOOL.True) lockedProperties.Add("LockMoveY");
                    if (shape.Protection.LockWidth.Value == BOOL.True) lockedProperties.Add("LockWidth");
                    if (shape.Protection.LockHeight.Value == BOOL.True) lockedProperties.Add("LockHeight");
                    if (shape.Protection.LockRotate.Value == BOOL.True) lockedProperties.Add("LockRotate");
                    if (shape.Protection.LockAspect.Value == BOOL.True) lockedProperties.Add("LockAspect");
                    if (shape.Protection.LockDelete.Value == BOOL.True) lockedProperties.Add("LockDelete");
                    if (shape.Protection.LockSelect.Value == BOOL.True) lockedProperties.Add("LockSelect");
                    if (shape.Protection.LockTextEdit.Value == BOOL.True) lockedProperties.Add("LockTextEdit");

                    // If any lock is present, log the attempt and skip modification
                    if (lockedProperties.Count > 0)
                    {
                        Console.WriteLine($"[Locked] Attempt to modify Shape ID {shape.ID} on Page \"{page.Name}\" blocked. Locked properties: {string.Join(", ", lockedProperties)}");
                        continue;
                    }

                    // No locks detected – perform a sample modification (move shape by 1 inch)
                    try
                    {
                        shape.XForm.PinX.Value += 1.0; // Move right
                        shape.XForm.PinY.Value += 1.0; // Move up
                        Console.WriteLine($"[Modified] Shape ID {shape.ID} on Page \"{page.Name}\" moved by 1 inch.");
                    }
                    catch (Exception ex)
                    {
                        // Log any unexpected errors during modification
                        Console.WriteLine($"[Error] Failed to modify Shape ID {shape.ID} on Page \"{page.Name}\": {ex.Message}");
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to \"{outputPath}\".");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
