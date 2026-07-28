using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve user-defined cells "A" and "B"
                        double valueA = 0;
                        double valueB = 0;
                        bool hasA = false;
                        bool hasB = false;

                        foreach (User user in shape.Users)
                        {
                            if (user.Name.Equals("A", StringComparison.OrdinalIgnoreCase))
                            {
                                hasA = double.TryParse(user.Value.Val, out valueA);
                            }
                            else if (user.Name.Equals("B", StringComparison.OrdinalIgnoreCase))
                            {
                                hasB = double.TryParse(user.Value.Val, out valueB);
                            }
                        }

                        // If both cells exist and contain numeric values, compute the result
                        if (hasA && hasB)
                        {
                            double result = valueA + valueB; // Example expression: A + B

                            // Check if a "Result" user-defined cell already exists
                            User resultUser = null;
                            foreach (User user in shape.Users)
                            {
                                if (user.Name.Equals("Result", StringComparison.OrdinalIgnoreCase))
                                {
                                    resultUser = user;
                                    break;
                                }
                            }

                            if (resultUser != null)
                            {
                                // Update existing cell
                                resultUser.Value.Val = result.ToString();
                            }
                            else
                            {
                                // Create a new user-defined cell named "Result"
                                User newUser = new User
                                {
                                    Name = "Result",
                                    NameU = "Result",
                                    Prompt = { Value = "Computed Result" },
                                    Value = { Val = result.ToString() }
                                };
                                shape.Users.Add(newUser);
                            }
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }