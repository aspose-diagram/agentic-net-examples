using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page of the diagram
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.Del == BOOL.False) // ensure the shape is not marked as deleted
                    {
                        targetShape = shp;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception("No suitable shape found on the page.");
                }

                // Ensure the shape has at least one field to modify
                if (targetShape.Fields.Count == 0)
                {
                    throw new Exception("The selected shape does not contain any fields.");
                }

                // Access the first field (you can select a different index as required)
                Field field = targetShape.Fields[0];

                // Set the formula to calculate the area (Width * Height)
                field.Value.Ufev.F = "Width*Height";

                // Use an undefined unit for the formula result
                field.Value.Ufev.Unit = MeasureConst.Undefined;

                // Optional: clear any existing format strings
                field.Format.Val = "";
                field.Format.Ufev.F = "";
                field.Format.Ufev.Unit = MeasureConst.Undefined;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }