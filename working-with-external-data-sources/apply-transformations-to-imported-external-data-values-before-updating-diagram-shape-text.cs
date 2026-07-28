using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the updated Visio file
                string outputPath = "output.vsdx";

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Refresh external data connections to ensure latest data is loaded
                    diagram.Refresh();

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Example: If the shape has a Data1 value that can be parsed as a number,
                            // apply a transformation (e.g., multiply by 2) and update the shape text.
                            if (!string.IsNullOrWhiteSpace(shape.Data1))
                            {
                                if (double.TryParse(shape.Data1, out double originalValue))
                                {
                                    // Transformation logic: double the value
                                    double transformedValue = originalValue * 2;

                                    // Update the Data1 field with the transformed value
                                    shape.Data1 = transformedValue.ToString();

                                    // Update the shape's displayed text to reflect the new value
                                    shape.Text.Value.Clear();
                                    shape.Text.Value.Add(new Txt($"Value: {transformedValue}"));
                                }
                            }

                            // Example: If the shape has a Data2 value that represents a date,
                            // convert it to a different format before updating the text.
                            if (!string.IsNullOrWhiteSpace(shape.Data2))
                            {
                                if (DateTime.TryParse(shape.Data2, out DateTime dateValue))
                                {
                                    // Transformation: format date as "yyyy-MM-dd"
                                    string formattedDate = dateValue.ToString("yyyy-MM-dd");

                                    // Update the Data2 field
                                    shape.Data2 = formattedDate;

                                    // Update the shape's displayed text
                                    shape.Text.Value.Clear();
                                    shape.Text.Value.Add(new Txt($"Date: {formattedDate}"));
                                }
                            }
                        }
                    }

                    // Save the updated diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    // Simple error handling
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }