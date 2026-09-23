using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Temporary backup file to enable rollback
                string backupPath = Path.Combine(Path.GetTempPath(), "diagram_backup.vsdx");

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Save a backup copy before making any changes
                diagram.Save(backupPath, SaveFileFormat.Vsdx);

                try
                {
                    // Example geometry update: move the first shape on the first page
                    Page page = diagram.Pages[0];
                    // Retrieve shape with ID 1 (adjust as needed)
                    Shape shape = page.Shapes.GetShape(1);
                    // Apply translation
                    shape.XForm.PinX.Value += 1.0; // move right by 1 inch
                    shape.XForm.PinY.Value += 1.0; // move up by 1 inch

                    // Additional geometry modifications can be placed here
                    // ...

                    Console.WriteLine("Geometry updates applied successfully.");
                }
                catch (Exception ex)
                {
                    // Rollback to the backup diagram if any error occurs
                    diagram = new Diagram(backupPath);
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    Console.WriteLine("Diagram has been rolled back to its original state.");
                }
                finally
                {
                    // Save the final diagram (whether updated or rolled back)
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

                    // Clean up the temporary backup file
                    if (File.Exists(backupPath))
                    {
                        File.Delete(backupPath);
                    }

                    Console.WriteLine("Diagram saved to output.vsdx.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }