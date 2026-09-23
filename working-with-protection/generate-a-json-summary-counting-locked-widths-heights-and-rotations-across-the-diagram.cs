using System;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Counters for locked properties
                int lockedWidthCount = 0;
                int lockedHeightCount = 0;
                int lockedRotationCount = 0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check width lock
                        if (shape.Protection.LockWidth.Value == BOOL.True)
                            lockedWidthCount++;

                        // Check height lock
                        if (shape.Protection.LockHeight.Value == BOOL.True)
                            lockedHeightCount++;

                        // Check rotation lock
                        if (shape.Protection.LockRotate.Value == BOOL.True)
                            lockedRotationCount++;
                    }
                }

                // Prepare summary object
                var summary = new
                {
                    LockedWidthCount = lockedWidthCount,
                    LockedHeightCount = lockedHeightCount,
                    LockedRotationCount = lockedRotationCount
                };

                // Serialize to JSON
                string json = JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true });

                // Output the JSON summary
                Console.WriteLine(json);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }