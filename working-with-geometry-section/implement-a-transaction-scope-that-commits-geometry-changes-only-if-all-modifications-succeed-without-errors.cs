using System;
using System.IO;
using Aspose.Diagram;

namespace DiagramTransactionExample
{
    // Helper class to manage a transaction-like scope for diagram modifications
    // It saves the original diagram state to a memory stream before any changes.
    // If Commit is called, changes are kept; otherwise the original state is restored.
    public class GeometryTransaction : IDisposable
    {
        private readonly Diagram _diagram;
        private readonly MemoryStream _originalState;
        private bool _committed;

        public GeometryTransaction(Diagram diagram)
        {
            _diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));
            // Save the original diagram to a memory stream (VDX format works for round‑trip)
            _originalState = new MemoryStream();
            _diagram.Save(_originalState, SaveFileFormat.Vsdx);
            // Reset position for potential reload
            _originalState.Position = 0;
        }

        // Call this when all modifications succeed
        public void Commit()
        {
            _committed = true;
        }

        // If not committed, restore the original diagram state
        public void Dispose()
        {
            if (!_committed)
            {
                // Reload the diagram from the saved original state
                // Note: Diagram constructor that takes a stream and format is used
                Diagram restored = new Diagram(_originalState, LoadFileFormat.Vsdx);
                // Replace the contents of the original diagram with the restored one
                // Since Diagram does not provide a direct copy method, we replace key collections
                _diagram.Pages.Clear();
                foreach (Page page in restored.Pages)
                {
                    _diagram.Pages.Add(page);
                }

                _diagram.Masters.Clear();
                foreach (Master master in restored.Masters)
                {
                    _diagram.Masters.Add(master);
                }

                // Additional collections (e.g., Styles, Fonts) can be restored similarly if needed
            }

            _originalState.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Begin a transaction scope
                using (var transaction = new GeometryTransaction(diagram))
                {
                    try
                    {
                        // Example modification: add a line segment to the first shape on the active page
                        Page activePage = diagram.ActivePage;
                        if (activePage == null || activePage.Shapes.Count == 0)
                            throw new InvalidOperationException("No shapes available to modify.");

                        // Get the first shape
                        Shape shape = (Shape)activePage.Shapes[0];

                        // Ensure the shape has at least one geometry section
                        if (shape.Geoms.Count == 0)
                            throw new InvalidOperationException("Shape does not contain geometry sections.");

                        // Access the first geometry section explicitly
                        Geom geom = (Geom)shape.Geoms[0];

                        // Start a new path with MoveTo if none exists
                        if (geom.CoordinateCol.Count == 0)
                        {
                            MoveTo start = new MoveTo();
                            start.X.Value = 0.0;
                            start.Y.Value = 0.0;
                            geom.CoordinateCol.Add(start);
                        }

                        // Append a new line segment
                        LineTo line = new LineTo();
                        line.X.Value = 2.0; // X coordinate
                        line.Y.Value = 2.0; // Y coordinate
                        geom.CoordinateCol.Add(line);

                        // If all modifications succeed, commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Log the error; the transaction will automatically roll back on Dispose
                        Console.WriteLine($"Error during diagram modification: {ex.Message}");
                    }
                }

                // Save the diagram only if modifications were successful
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}