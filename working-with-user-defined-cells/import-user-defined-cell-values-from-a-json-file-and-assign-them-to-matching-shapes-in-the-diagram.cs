using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and JSON file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramUserCellImporter <VisioFilePath> <JsonFilePath>");
                return;
            }

            string visioPath = args[0];
            string jsonPath = args[1];
            string outputPath = Path.Combine(
                Path.GetDirectoryName(visioPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(visioPath) + "_Updated.vsdx");

            // Load the diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(visioPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load Visio file: {ex.Message}");
                return;
            }

            // Read and parse JSON
            // Expected format:
            // {
            //   "ShapeNameU1": { "UserCellName1": "Value1", "UserCellName2": "Value2" },
            //   "ShapeNameU2": { "UserCellNameA": "ValueA" }
            // }
            Dictionary<string, Dictionary<string, string>> shapeUserData;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                shapeUserData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonContent);
                if (shapeUserData == null)
                {
                    Console.WriteLine("JSON file is empty or not in expected format.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read or parse JSON file: {ex.Message}");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Match shape by its universal name (NameU)
                    if (shape.NameU != null && shapeUserData.TryGetValue(shape.NameU, out var userCells))
                    {
                        foreach (KeyValuePair<string, string> cellKvp in userCells)
                        {
                            string userName = cellKvp.Key;
                            string userValue = cellKvp.Value;

                            // Search for existing user-defined cell
                            User existingUser = null;
                            foreach (User u in shape.Users)
                            {
                                if (u.Name == userName)
                                {
                                    existingUser = u;
                                    break;
                                }
                            }

                            if (existingUser != null)
                            {
                                // Update existing cell value
                                existingUser.Value.Val = userValue;
                            }
                            else
                            {
                                // Create a new user-defined cell
                                User newUser = new User();
                                newUser.Name = userName;
                                newUser.Value.Val = userValue;
                                shape.Users.Add(newUser);
                            }
                        }
                    }
                }
            }

            // Save the updated diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save diagram: {ex.Message}");
            }
        }
    }