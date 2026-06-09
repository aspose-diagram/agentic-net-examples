using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramTransactionDemo
{
    // Handles geometry modifications as a transaction.
    // If Commit is not called, the diagram is rolled back to its original state.
    class GeometryTransaction : IDisposable
    {
        private readonly Diagram _diagram;
        private readonly MemoryStream _backupStream;
        private bool _committed = false;

        public GeometryTransaction(Diagram diagram)
        {
            _diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));

            // Preserve the original diagram state in a memory stream.
            _backupStream = new MemoryStream();
            _diagram.Save(_backupStream, SaveFileFormat.Vsdx);
            _backupStream.Position = 0;
        }

        // Call this when all modifications succeed.
        public void Commit()
        {
            _committed = true;
        }

        public void Dispose()
        {
            if (!_committed)
            {
                // Roll back: reload the diagram from the backup stream.
                _backupStream.Position = 0;
                Diagram restored = new Diagram(_backupStream, LoadFileFormat.Vsdx);

                // Replace the contents of the original diagram with the restored one.
                // Clear current pages and copy pages from restored diagram.
                _diagram.Pages.Clear();
                foreach (Page page in restored.Pages)
                {
                    _diagram.Pages.Add(page);
                }

                // Similarly, copy masters, styles, etc., if needed.
                // For this example, pages are sufficient.
            }

            _backupStream.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing diagram.
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

                // Begin a geometry transaction.
                using (var tx = new GeometryTransaction(diagram))
                {
                    try
                    {
                        // Example modification: add a line segment to the first shape on the first page.
                        if (diagram.Pages.Count > 0 && diagram.Pages[0].Shapes.Count > 0)
                        {
                            Shape shape = diagram.Pages[0].Shapes[0];

                            // Ensure the shape has at least one geometry section.
                            if (shape.Geoms.Count > 0)
                            {
                                // Retrieve the first geometry.
                                Geom geom = (Geom)shape.Geoms[0];

                                // Create a new line segment.
                                LineTo line = new LineTo();
                                line.X.Value = 2.0; // X coordinate
                                line.Y.Value = 2.0; // Y coordinate

                                // Append the new segment.
                                geom.CoordinateCol.Add(line);
                            }
                            else
                            {
                                throw new InvalidOperationException("Shape does not contain any geometry sections.");
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("Diagram does not contain any pages or shapes.");
                        }

                        // All modifications succeeded; commit the transaction.
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Any exception will cause the transaction to roll back automatically.
                        Console.WriteLine($"Error during geometry modification: {ex.Message}");
                        // No need to rethrow; the using block will dispose and roll back.
                    }
                }

                // Save the diagram after successful transaction.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}