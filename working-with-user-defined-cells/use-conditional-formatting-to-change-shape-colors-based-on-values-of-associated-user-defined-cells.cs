using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (can be passed as command‑line arguments)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Look for a user‑defined cell named "Value"
                        User userCell = null;
                        foreach (User user in shape.Users)
                        {
                            if (user.Name == "Value")
                            {
                                userCell = user;
                                break;
                            }
                        }

                        // If the user cell exists, evaluate its value and apply color
                        if (userCell != null)
                        {
                            double numericValue;
                            // Try to parse the cell value as a double
                            if (double.TryParse(userCell.Value.Val, out numericValue))
                            {
                                // Example rule: values > 100 -> red, otherwise -> green
                                if (numericValue > 100)
                                {
                                    // Set fill foreground color to red
                                    shape.Fill.FillForegnd.Value = "#FF0000";
                                }
                                else
                                {
                                    // Set fill foreground color to green
                                    shape.Fill.FillForegnd.Value = "#00FF00";
                                }
                            }
                            else
                            {
                                // If parsing fails, you could apply a default color (optional)
                                shape.Fill.FillForegnd.Value = "#CCCCCC"; // light gray
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }