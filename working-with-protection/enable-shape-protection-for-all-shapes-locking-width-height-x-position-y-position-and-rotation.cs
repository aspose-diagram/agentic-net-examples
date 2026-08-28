using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Apply protection to lock position, size, and rotation
                        shape.Protection.LockMoveX.Value = BOOL.True;   // Lock X‑position
                        shape.Protection.LockMoveY.Value = BOOL.True;   // Lock Y‑position
                        shape.Protection.LockWidth.Value = BOOL.True;   // Lock width
                        shape.Protection.LockHeight.Value = BOOL.True;  // Lock height
                        shape.Protection.LockRotate.Value = BOOL.True;  // Lock rotation
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }