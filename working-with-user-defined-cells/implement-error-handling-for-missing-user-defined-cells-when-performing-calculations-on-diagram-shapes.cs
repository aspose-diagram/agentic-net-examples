using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            Diagram diagram = null;

            try
            {
                // Load the diagram from file
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading diagram: {ex.Message}");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the Users collection is available
                    if (shape.Users == null)
                        continue;

                    // Attempt to find a user-defined cell named "CustomValue"
                    User customUser = null;
                    foreach (User user in shape.Users)
                    {
                        if (string.Equals(user.Name, "CustomValue", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(user.NameU, "CustomValue", StringComparison.OrdinalIgnoreCase))
                        {
                            customUser = user;
                            break;
                        }
                    }

                    if (customUser == null)
                    {
                        // Missing user-defined cell – log and continue
                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' does not contain 'CustomValue' cell.");
                        continue;
                    }

                    // Parse the cell value safely
                    if (!double.TryParse(customUser.Value.Val, out double customValue))
                    {
                        Console.WriteLine($"Invalid numeric value in 'CustomValue' for shape ID {shape.ID}: '{customUser.Value.Val}'.");
                        continue;
                    }

                    // Perform a sample calculation (e.g., double the value)
                    double resultValue = customValue * 2;

                    // Store the result in a user-defined cell named "Result"
                    User resultUser = null;
                    foreach (User user in shape.Users)
                    {
                        if (string.Equals(user.Name, "Result", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(user.NameU, "Result", StringComparison.OrdinalIgnoreCase))
                        {
                            resultUser = user;
                            break;
                        }
                    }

                    if (resultUser == null)
                    {
                        // Create the cell if it does not exist
                        resultUser = new User
                        {
                            Name = "Result",
                            Value = { Val = resultValue.ToString() }
                        };
                        shape.Users.Add(resultUser);
                    }
                    else
                    {
                        // Update existing cell
                        resultUser.Value.Val = resultValue.ToString();
                    }

                    Console.WriteLine($"Shape ID {shape.ID}: CustomValue={customValue}, Result={resultValue}");
                }
            }

            try
            {
                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving diagram: {ex.Message}");
            }
        }
    }