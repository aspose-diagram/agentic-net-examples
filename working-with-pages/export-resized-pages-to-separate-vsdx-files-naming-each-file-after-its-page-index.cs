using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the source diagram
                Diagram sourceDiagram = new Diagram(sourcePath);

                int pageCount = sourceDiagram.Pages.Count;

                for (int i = 0; i < pageCount; i++)
                {
                    // Create a new empty diagram for the current page
                    Diagram pageDiagram = new Diagram();

                    // Copy all masters from the source diagram
                    foreach (Master master in sourceDiagram.Masters)
                    {
                        // Add master by name from the source diagram
                        pageDiagram.AddMaster(sourceDiagram, master.Name);
                    }

                    // Retrieve the source page
                    Page srcPage = sourceDiagram.Pages[i];

                    // Create a new page and copy its PageSheet (size, properties, etc.)
                    Page newPage = new Page();
                    newPage.Name = srcPage.Name;
                    newPage.NameU = srcPage.NameU;
                    newPage.PageSheet.Copy(srcPage.PageSheet);

                    // Add the new page to the diagram
                    pageDiagram.Pages.Add(newPage);

                    // Copy shapes that have an associated master
                    foreach (Shape shape in srcPage.Shapes)
                    {
                        if (shape.Master != null)
                        {
                            // Add the shape to the new diagram using the same master
                            pageDiagram.AddShape(shape, shape.Master.Name, pageDiagram.Pages.Count - 1);
                        }
                    }

                    // Build output file name using the page index
                    string outputFileName = $"Page_{i}{Path.GetExtension(sourcePath)}";

                    // Save the single‑page diagram as VSDX
                    pageDiagram.Save(outputFileName, SaveFileFormat.Vsdx);

                    // Clean up the per‑page diagram
                    pageDiagram.Dispose();
                }

                // Clean up the source diagram
                sourceDiagram.Dispose();

                Console.WriteLine("Export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }