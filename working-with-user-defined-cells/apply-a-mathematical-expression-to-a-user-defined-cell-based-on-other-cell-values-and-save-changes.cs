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
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Find the source user-defined cell (e.g., "SourceValue")
                        User sourceUser = null;
                        foreach (User user in shape.Users)
                        {
                            if (user.Name == "SourceValue")
                            {
                                sourceUser = user;
                                break;
                            }
                        }

                        // If the source cell is not present, continue to next shape
                        if (sourceUser == null)
                            continue;

                        // Try to parse the source value as double
                        if (!double.TryParse(sourceUser.Value.Val, out double sourceNumber))
                            continue; // Invalid number, skip

                        // Compute the new value (example expression: double the source)
                        double result = sourceNumber * 2.0;

                        // Find or create the target user-defined cell (e.g., "ResultValue")
                        User targetUser = null;
                        foreach (User user in shape.Users)
                        {
                            if (user.Name == "ResultValue")
                            {
                                targetUser = user;
                                break;
                            }
                        }

                        if (targetUser == null)
                        {
                            // Create a new user-defined cell if it does not exist
                            targetUser = new User();
                            targetUser.Name = "ResultValue";
                            shape.Users.Add(targetUser);
                        }

                        // Assign the computed result back to the target cell
                        targetUser.Value.Val = result.ToString();

                        // Optional: you can also set a prompt or universal name if needed
                        // targetUser.Prompt.Value = "Computed result";
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