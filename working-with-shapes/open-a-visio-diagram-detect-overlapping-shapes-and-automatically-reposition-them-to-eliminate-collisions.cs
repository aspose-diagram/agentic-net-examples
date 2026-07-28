using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        // Simple structure to hold shape bounds for overlap checking
        private struct ShapeBounds
        {
            public long Id;
            public double Left;
            public double Right;
            public double Bottom;
            public double Top;
        }

        static void Main(string[] args)
        {
            // Input and output file paths (provide via command line or prompt)
            string inputPath;
            string outputPath;

            if (args.Length >= 2)
            {
                inputPath = args[0];
                outputPath = args[1];
            }
            else
            {
                Console.Write("Enter path to the Visio file to process: ");
                inputPath = Console.ReadLine();

                Console.Write("Enter path for the output Visio file: ");
                outputPath = Console.ReadLine();
            }

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // List to keep track of already positioned shapes on this page
                List<ShapeBounds> placedShapes = new List<ShapeBounds>();

                // Iterate over all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve current geometry
                    double pinX = shape.XForm.PinX.Value;
                    double pinY = shape.XForm.PinY.Value;
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Compute bounding box
                    double left = pinX - width / 2.0;
                    double right = pinX + width / 2.0;
                    double bottom = pinY - height / 2.0;
                    double top = pinY + height / 2.0;

                    // Resolve overlaps with previously placed shapes
                    bool overlapFound;
                    const double offset = 0.5; // inches to shift when overlap occurs

                    do
                    {
                        overlapFound = false;
                        foreach (ShapeBounds other in placedShapes)
                        {
                            bool isOverlapping =
                                left < other.Right && right > other.Left &&
                                bottom < other.Top && top > other.Bottom;

                            if (isOverlapping)
                            {
                                // Move the shape to the right by the offset
                                shape.Move(offset, 0);
                                // Update bounding box after move
                                pinX = shape.XForm.PinX.Value;
                                left = pinX - width / 2.0;
                                right = pinX + width / 2.0;
                                // Mark that we need to re‑check against all placed shapes
                                overlapFound = true;
                                break;
                            }
                        }
                    } while (overlapFound);

                    // Add the (now non‑overlapping) shape to the list
                    placedShapes.Add(new ShapeBounds
                    {
                        Id = shape.ID,
                        Left = left,
                        Right = right,
                        Bottom = bottom,
                        Top = top
                    });
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Processing complete. Diagram saved to: " + outputPath);
        }
    }