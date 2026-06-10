using System;
using System.Text.Json;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (replace with actual path)
                const string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                int lockedWidthCount = 0;
                int lockedHeightCount = 0;
                int lockedRotateCount = 0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check width lock
                        if (shape.Protection.LockWidth.Value == BOOL.True)
                            lockedWidthCount++;

                        // Check height lock
                        if (shape.Protection.LockHeight.Value == BOOL.True)
                            lockedHeightCount++;

                        // Check rotation lock
                        if (shape.Protection.LockRotate.Value == BOOL.True)
                            lockedRotateCount++;
                    }
                }

                // Prepare JSON summary
                var summary = new
                {
                    LockedWidth = lockedWidthCount,
                    LockedHeight = lockedHeightCount,
                    LockedRotate = lockedRotateCount
                };

                string json = JsonSerializer.Serialize(summary);
                Console.WriteLine(json);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }