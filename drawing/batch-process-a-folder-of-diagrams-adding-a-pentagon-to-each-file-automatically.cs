using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio diagram files (VSDX format)
            string inputFolder = @"C:\Diagrams";

            // Get all VSDX files in the folder
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Process each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Define pentagon vertices (closed polygon, repeat first point at end)
                        // Coordinates are in inches; adjust as needed.
                        double[] pentagonPoints = new double[]
                        {
                            5.0, 3.0,   // Point 1
                            7.0, 4.0,   // Point 2
                            6.5, 6.0,   // Point 3
                            3.5, 6.0,   // Point 4
                            3.0, 4.0,   // Point 5
                            5.0, 3.0    // Close back to Point 1
                        };

                        // Draw the pentagon shape on the current page
                        long shapeId = page.DrawPolyline(pentagonPoints);

                        // Retrieve the shape object (cast ID to int as required)
                        Shape pentagonShape = page.Shapes.GetShape((int)shapeId);

                        // Optional: set line color and fill color
                        pentagonShape.Line.LineColor.Value = "#FF0000"; // Red outline
                        pentagonShape.Fill.FillForegnd.Value = "#FFFF00"; // Yellow fill

                        // Add a label to the pentagon
                        pentagonShape.Text.Value.Clear();
                        pentagonShape.Text.Value.Add(new Txt("Pentagon"));
                    }

                    // Save the modified diagram (overwrite original file)
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }