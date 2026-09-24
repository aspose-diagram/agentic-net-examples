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

                // Path for the exported PDF
                string outputPath = "output.pdf";

                // Define the required user‑defined cell names
                string[] requiredUserCells = { "Cost", "Owner", "Status" };

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Validate each shape on every page
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        foreach (string cellName in requiredUserCells)
                        {
                            bool cellExists = false;

                            // Iterate the Users collection to find the cell
                            foreach (User userCell in shape.Users)
                            {
                                // Compare by the universal name (NameU) or the local name (Name)
                                if (userCell.NameU == cellName || userCell.Name == cellName)
                                {
                                    cellExists = true;
                                    break;
                                }
                            }

                            if (!cellExists)
                            {
                                // Report missing cell and abort the export
                                throw new Exception($"Shape ID {shape.ID} on page '{page.Name}' is missing required user‑defined cell '{cellName}'.");
                            }
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                pdfOptions.ExportHiddenPage = false; // Export only visible pages

                // Export the diagram to PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram exported successfully to PDF.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }