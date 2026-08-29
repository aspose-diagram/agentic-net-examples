using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Define temporary file path
            string tempFile = Path.Combine(Path.GetTempPath(), "ShowPageBreaksTest.vsdx");

            try
            {
                // ---------- Create Diagram ----------
                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a window to the diagram (required for ShowPageBreaks)
                Window window = new Window();
                // Set ShowPageBreaks to true using BOOL enum
                window.ShowPageBreaks = BOOL.True;
                // Add the window to the diagram's window collection
                diagram.Windows.Add(window);

                // ---------- Save Diagram ----------
                // Save the diagram to a VSDX file
                diagram.Save(tempFile, SaveFileFormat.Vsdx);

                // ---------- Load Diagram ----------
                // Load the diagram back from the saved file
                Diagram loadedDiagram = new Diagram(tempFile);

                // Verify that at least one window exists
                if (loadedDiagram.Windows.Count == 0)
                {
                    throw new Exception("No windows found after loading the diagram.");
                }

                // Retrieve the first window
                Window loadedWindow = loadedDiagram.Windows[0];

                // ---------- Verify ShowPageBreaks ----------
                // Check that ShowPageBreaks is still true
                if (loadedWindow.ShowPageBreaks != BOOL.True)
                {
                    throw new Exception("ShowPageBreaks property was not preserved after save and reload.");
                }

                // If we reach this point, the test passed
                Console.WriteLine("Test passed: ShowPageBreaks remains true after saving and reloading.");
            }
            catch (Exception ex)
            {
                // Output any failure messages
                Console.WriteLine($"Test failed: {ex.Message}");
                // Re-throw to indicate failure if needed
                throw;
            }
            finally
            {
                // Clean up temporary file
                if (File.Exists(tempFile))
                {
                    try
                    {
                        File.Delete(tempFile);
                    }
                    catch
                    {
                        // Ignore cleanup errors
                    }
                }
            }
        }
    }