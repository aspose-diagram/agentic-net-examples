using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Lock horizontal position (X)
                        shape.Protection.LockMoveX.Value = BOOL.True;
                        // Lock vertical position (Y)
                        shape.Protection.LockMoveY.Value = BOOL.True;
                        // Lock width
                        shape.Protection.LockWidth.Value = BOOL.True;
                        // Lock height
                        shape.Protection.LockHeight.Value = BOOL.True;
                        // Lock rotation
                        shape.Protection.LockRotate.Value = BOOL.True;
                    }
                }

                // Save the modified diagram (replace with desired output path)
                string outputPath = "output_protected.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }