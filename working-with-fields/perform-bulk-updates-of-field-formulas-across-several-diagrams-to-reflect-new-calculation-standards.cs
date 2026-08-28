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

                // Folder containing source Visio files
                string inputFolder = @"C:\Visio\Input";
                // Folder where updated files will be saved
                string outputFolder = @"C:\Visio\Output";

                // Ensure output directory exists
                if (!Directory.Exists(outputFolder))
                {
                    Directory.CreateDirectory(outputFolder);
                }

                // Get all Visio files (VSDX) in the input folder
                string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

                foreach (string filePath in diagramFiles)
                {
                    try
                    {
                        // Load the diagram
                        Diagram diagram = new Diagram(filePath);

                        // Iterate through each page
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through each shape on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Iterate through each field (text-insertion field) of the shape
                                foreach (Field field in shape.Fields)
                                {
                                    // Update the formula to the new standard
                                    // Example: set formula to calculate area (Width * Height)
                                    field.Value.Ufev.F = "Width*Height";

                                    // Optionally clear unit and format if not needed
                                    field.Value.Ufev.Unit = MeasureConst.Undefined;
                                    field.Format.Val = "";
                                    field.Format.Ufev.F = "";
                                }
                            }
                        }

                        // Save the updated diagram to the output folder
                        string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    }
                    catch (Exception ex)
                    {
                        // Log any errors for the current file
                        Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                    }
                }

                Console.WriteLine("Bulk field formula update completed.");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }