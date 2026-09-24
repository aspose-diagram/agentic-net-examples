using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Look for a user‑defined cell named "Status"
                        foreach (User userCell in shape.Users)
                        {
                            if (userCell.Name == "Status")
                            {
                                string cellValue = userCell.Value.Val?.Trim();

                                // Apply conditional color based on the cell value
                                if (cellValue == "1")
                                {
                                    // Green fill for status = 1
                                    shape.Fill.FillForegnd.Value = "#00FF00";
                                }
                                else
                                {
                                    // Red fill for any other status
                                    shape.Fill.FillForegnd.Value = "#FF0000";
                                }

                                // No need to check other user cells for this shape
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }