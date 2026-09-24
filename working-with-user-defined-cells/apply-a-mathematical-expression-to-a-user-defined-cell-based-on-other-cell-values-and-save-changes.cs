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
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Look for user-defined cells named "Value1" and "Value2"
                        User? userValue1 = null;
                        User? userValue2 = null;

                        foreach (User user in shape.Users)
                        {
                            if (user.Name.Equals("Value1", StringComparison.OrdinalIgnoreCase))
                                userValue1 = user;
                            else if (user.Name.Equals("Value2", StringComparison.OrdinalIgnoreCase))
                                userValue2 = user;
                        }

                        // If both source cells are present, compute the expression
                        if (userValue1 != null && userValue2 != null)
                        {
                            // Try to parse the numeric values
                            if (double.TryParse(userValue1.Value.Val, out double val1) &&
                                double.TryParse(userValue2.Value.Val, out double val2))
                            {
                                // Example expression: (Value1 + Value2) * 2
                                double result = (val1 + val2) * 2.0;

                                // Find existing "Result" cell or create a new one
                                User? resultCell = null;
                                foreach (User user in shape.Users)
                                {
                                    if (user.Name.Equals("Result", StringComparison.OrdinalIgnoreCase))
                                    {
                                        resultCell = user;
                                        break;
                                    }
                                }

                                if (resultCell == null)
                                {
                                    // Create a new user-defined cell named "Result"
                                    resultCell = new User();
                                    resultCell.Name = "Result";
                                    shape.Users.Add(resultCell);
                                }

                                // Assign the computed value as a string
                                resultCell.Value.Val = result.ToString();

                                Console.WriteLine($"Shape ID {shape.ID}: Computed Result = {result}");
                            }
                            else
                            {
                                Console.WriteLine($"Shape ID {shape.ID}: Unable to parse numeric values.");
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }