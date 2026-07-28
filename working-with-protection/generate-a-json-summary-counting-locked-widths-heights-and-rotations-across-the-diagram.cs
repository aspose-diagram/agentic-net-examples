using System;
using System.Text.Json;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (provide via command line or default)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                int lockedWidthCount = 0;
                int lockedHeightCount = 0;
                int lockedRotateCount = 0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Protection cell collection exists
                        if (shape.Protection != null)
                        {
                            if (shape.Protection.LockWidth != null && shape.Protection.LockWidth.Value == BOOL.True)
                                lockedWidthCount++;

                            if (shape.Protection.LockHeight != null && shape.Protection.LockHeight.Value == BOOL.True)
                                lockedHeightCount++;

                            if (shape.Protection.LockRotate != null && shape.Protection.LockRotate.Value == BOOL.True)
                                lockedRotateCount++;
                        }
                    }
                }

                // Prepare summary object
                var summary = new
                {
                    LockedWidth = lockedWidthCount,
                    LockedHeight = lockedHeightCount,
                    LockedRotate = lockedRotateCount
                };

                // Serialize to JSON and output
                string json = JsonSerializer.Serialize(summary);
                Console.WriteLine(json);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }