using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

namespace MockAutoSpaceTest
{
    // Simple mock of AutoSpaceOptions (mirrors Aspose.Diagram.AutoSpaceOptions)
    public class MockAutoSpaceOptions
    {
        public double HorizontalSpacing { get; set; }
        public double VerticalSpacing { get; set; }

        public MockAutoSpaceOptions()
        {
            HorizontalSpacing = 1.0;
            VerticalSpacing = 1.0;
        }
    }

    // Mock shape with minimal properties required for spacing
    public class MockShape
    {
        public double PinX { get; set; }   // X coordinate of shape center
        public double PinY { get; set; }   // Y coordinate of shape center
        public double Width { get; set; }
        public double Height { get; set; }

        public MockShape(double pinX, double pinY, double width, double height)
        {
            PinX = pinX;
            PinY = pinY;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"Shape[PinX={PinX}, PinY={PinY}, Width={Width}, Height={Height}]";
        }
    }

    // Mock page that holds a collection of shapes and provides an AutoSpace method
    public class MockPage
    {
        public List<MockShape> Shapes { get; } = new List<MockShape>();

        // Adds a shape to the page and returns its index (simulating Aspose behavior)
        public int AddShape(MockShape shape)
        {
            Shapes.Add(shape);
            return Shapes.Count - 1;
        }

        // Mock implementation of AutoSpaceShapes.
        // Arranges the provided shapes in a simple grid based on the spacing options.
        public void AutoSpaceShapes(List<MockShape> targetShapes, MockAutoSpaceOptions options)
        {
            if (targetShapes == null) throw new ArgumentNullException(nameof(targetShapes));
            if (options == null) throw new ArgumentNullException(nameof(options));

            // Determine grid dimensions (simple square layout)
            int count = targetShapes.Count;
            int columns = (int)Math.Ceiling(Math.Sqrt(count));
            int rows = (int)Math.Ceiling((double)count / columns);

            double startX = 0.0;
            double startY = 0.0;

            int index = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    if (index >= count) break;

                    MockShape shape = targetShapes[index];
                    // Position shape based on its size and the spacing options
                    shape.PinX = startX + c * (shape.Width + options.HorizontalSpacing);
                    shape.PinY = startY + r * (shape.Height + options.VerticalSpacing);
                    index++;
                }
            }
        }
    }

    // Mock diagram containing pages
    public class MockDiagram
    {
        public List<MockPage> Pages { get; } = new List<MockPage>();

        public MockPage AddPage()
        {
            var page = new MockPage();
            Pages.Add(page);
            return page;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // No external files are required for this mock test, but we keep the guard pattern for consistency.
            // If a future extension adds a file path argument, the guard will protect against missing files.
            if (args.Length > 0 && !File.Exists(args[0]))
            {
                Console.Error.WriteLine($"Input file not found: {args[0]}");
                return;
            }

            try
            {
                // Create a mock diagram and a single page.
                var diagram = new MockDiagram();
                MockPage page = diagram.AddPage();

                // Add a few shapes with arbitrary sizes.
                page.AddShape(new MockShape(0, 0, 2, 1)); // Shape 0
                page.AddShape(new MockShape(0, 0, 1.5, 1.5)); // Shape 1
                page.AddShape(new MockShape(0, 0, 2.5, 2)); // Shape 2
                page.AddShape(new MockShape(0, 0, 1, 1)); // Shape 3

                // Prepare auto-space options.
                var options = new MockAutoSpaceOptions
                {
                    HorizontalSpacing = 0.5,
                    VerticalSpacing = 0.75
                };

                // Perform auto-spacing on all shapes in the page.
                page.AutoSpaceShapes(page.Shapes, options);

                // Output the new positions of the shapes for verification.
                Console.WriteLine("Auto-spaced shape positions:");
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Console.WriteLine($"Shape {i}: {page.Shapes[i]}");
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected errors.
                Console.Error.WriteLine($"Error during mock auto-spacing test: {ex.Message}");
            }
        }
    }
}