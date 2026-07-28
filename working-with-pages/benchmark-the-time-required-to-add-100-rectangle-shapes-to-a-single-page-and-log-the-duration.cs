using System;
using System.Diagnostics;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                using (Diagram diagram = new Diagram())
                {
                    // Ensure there is at least one page
                    if (diagram.Pages.Count == 0)
                    {
                        diagram.Pages.Add(new Page());
                    }

                    // Get the first page
                    Page page = diagram.Pages[0];

                    // Start timing
                    Stopwatch sw = Stopwatch.StartNew();

                    // Add 100 rectangle shapes
                    for (int i = 0; i < 100; i++)
                    {
                        // Position shapes in a grid for simplicity
                        double pinX = (i % 10) * 1.5 + 1; // 1 inch spacing
                        double pinY = (i / 10) * 1.5 + 1;
                        double width = 1.0;
                        double height = 0.5;

                        // Add a rectangle shape; returns the shape ID (long)
                        page.AddShape(pinX, pinY, width, height, "Rectangle");
                    }

                    // Stop timing
                    sw.Stop();

                    // Log the duration in milliseconds
                    Console.WriteLine($"Added 100 rectangle shapes in {sw.ElapsedMilliseconds} ms.");
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }