using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the Visio diagram and the CSV spreadsheet
                string diagramPath = "input.vsdx";
                string csvPath = "data.csv";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Read CSV file
                List<string> headers = new List<string>();
                List<string[]> rows = new List<string[]>();

                using (StreamReader reader = new StreamReader(csvPath))
                {
                    bool isFirstLine = true;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        string[] parts = line.Split(',');

                        if (isFirstLine)
                        {
                            // Store header names
                            foreach (string header in parts)
                                headers.Add(header.Trim());
                            isFirstLine = false;
                        }
                        else
                        {
                            rows.Add(parts);
                        }
                    }
                }

                // Expect a column named "ShapeName" that identifies the target shape
                int shapeNameIndex = headers.IndexOf("ShapeName");
                if (shapeNameIndex == -1)
                    throw new Exception("CSV must contain a 'ShapeName' column.");

                // Process each data row
                foreach (string[] row in rows)
                {
                    if (row.Length != headers.Count)
                        continue; // Skip malformed rows

                    string targetShapeName = row[shapeNameIndex];

                    // Find the shape with matching universal name (NameU)
                    Shape targetShape = null;
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (shape.NameU != null && shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                            {
                                targetShape = shape;
                                break;
                            }
                        }
                        if (targetShape != null)
                            break;
                    }

                    if (targetShape == null)
                    {
                        Console.WriteLine($"Shape '{targetShapeName}' not found. Skipping row.");
                        continue;
                    }

                    // Map each column (except ShapeName) to a user-defined cell
                    for (int i = 0; i < headers.Count; i++)
                    {
                        if (i == shapeNameIndex)
                            continue; // Skip the identifier column

                        string userCellName = headers[i];
                        string cellValue = row[i];

                        // Search for existing user-defined cell
                        User existingUser = null;
                        foreach (User user in targetShape.Users)
                        {
                            if (user.Name != null && user.Name.Equals(userCellName, StringComparison.OrdinalIgnoreCase))
                            {
                                existingUser = user;
                                break;
                            }
                        }

                        if (existingUser != null)
                        {
                            // Update existing cell value
                            existingUser.Value.Val = cellValue;
                        }
                        else
                        {
                            // Create a new user-defined cell
                            User newUser = new User();
                            newUser.Name = userCellName;
                            newUser.Value.Val = cellValue;
                            targetShape.Users.Add(newUser);
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Csv);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }