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
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Example: attempt to move the shape and change its line color
                            TryModifyShape(shape);
                        }
                    }

                    // Save the diagram after processing
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Attempts to modify a shape while respecting its protection settings.
        /// Logs any attempt to change a locked attribute.
        /// </summary>
        /// <param name="shape">The shape to modify.</param>
        private static void TryModifyShape(Shape shape)
        {
            // Attempt to move the shape (change PinX and PinY)
            bool canMoveX = shape.Protection.LockMoveX.Value != BOOL.True;
            bool canMoveY = shape.Protection.LockMoveY.Value != BOOL.True;

            if (canMoveX && canMoveY)
            {
                // Move the shape by adding an offset of 0.5 inches
                shape.XForm.PinX.Value += 0.5;
                shape.XForm.PinY.Value += 0.5;
                Console.WriteLine($"Shape ID {shape.ID} moved.");
            }
            else
            {
                if (!canMoveX)
                {
                    Console.WriteLine($"Attempted to modify PinX of shape ID {shape.ID}, but LockMoveX is enabled.");
                }
                if (!canMoveY)
                {
                    Console.WriteLine($"Attempted to modify PinY of shape ID {shape.ID}, but LockMoveY is enabled.");
                }
            }

            // Attempt to resize the shape (change Width and Height)
            bool canResizeWidth = shape.Protection.LockWidth.Value != BOOL.True;
            bool canResizeHeight = shape.Protection.LockHeight.Value != BOOL.True;

            if (canResizeWidth && canResizeHeight)
            {
                shape.XForm.Width.Value += 0.2;   // increase width by 0.2 inches
                shape.XForm.Height.Value += 0.2; // increase height by 0.2 inches
                Console.WriteLine($"Shape ID {shape.ID} resized.");
            }
            else
            {
                if (!canResizeWidth)
                {
                    Console.WriteLine($"Attempted to modify Width of shape ID {shape.ID}, but LockWidth is enabled.");
                }
                if (!canResizeHeight)
                {
                    Console.WriteLine($"Attempted to modify Height of shape ID {shape.ID}, but LockHeight is enabled.");
                }
            }

            // Attempt to change line color (no specific lock, but we still log the action)
            try
            {
                shape.Line.LineColor.Value = "#FF0000"; // set line color to red
                Console.WriteLine($"Line color of shape ID {shape.ID} set to red.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to set line color for shape ID {shape.ID}: {ex.Message}");
            }
        }
    }