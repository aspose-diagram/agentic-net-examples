using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        try
                        {
                            // Attempt to retrieve required user-defined cells
                            // Example: expecting cells named "Length" and "Width"
                            string lengthCellName = "Length";
                            string widthCellName = "Width";

                            // Find the user-defined cell for Length
                            User lengthUser = null;
                            foreach (User user in shape.Users)
                            {
                                if (string.Equals(user.Name, lengthCellName, StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(user.NameU, lengthCellName, StringComparison.OrdinalIgnoreCase))
                                {
                                    lengthUser = user;
                                    break;
                                }
                            }

                            // Find the user-defined cell for Width
                            User widthUser = null;
                            foreach (User user in shape.Users)
                            {
                                if (string.Equals(user.Name, widthCellName, StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(user.NameU, widthCellName, StringComparison.OrdinalIgnoreCase))
                                {
                                    widthUser = user;
                                    break;
                                }
                            }

                            // Validate presence of both cells
                            if (lengthUser == null)
                            {
                                Console.WriteLine($"Shape ID {shape.ID} is missing required user-defined cell '{lengthCellName}'. Skipping calculation.");
                                continue; // Skip this shape
                            }

                            if (widthUser == null)
                            {
                                Console.WriteLine($"Shape ID {shape.ID} is missing required user-defined cell '{widthCellName}'. Skipping calculation.");
                                continue; // Skip this shape
                            }

                            // Parse the cell values (they are stored as strings)
                            if (!double.TryParse(lengthUser.Value.Val, out double lengthValue))
                            {
                                Console.WriteLine($"Shape ID {shape.ID} has invalid numeric value in '{lengthCellName}': '{lengthUser.Value.Val}'. Skipping.");
                                continue;
                            }

                            if (!double.TryParse(widthUser.Value.Val, out double widthValue))
                            {
                                Console.WriteLine($"Shape ID {shape.ID} has invalid numeric value in '{widthCellName}': '{widthUser.Value.Val}'. Skipping.");
                                continue;
                            }

                            // Perform the calculation (e.g., area)
                            double area = lengthValue * widthValue;

                            // Output the result
                            Console.WriteLine($"Shape ID {shape.ID}: Length={lengthValue}, Width={widthValue}, Area={area}");
                        }
                        catch (Exception ex)
                        {
                            // General exception handling for unexpected errors
                            Console.WriteLine($"Error processing shape ID {shape.ID}: {ex.Message}");
                        }
                    }
                }

                // Optionally, save the diagram after any modifications (none in this example)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }