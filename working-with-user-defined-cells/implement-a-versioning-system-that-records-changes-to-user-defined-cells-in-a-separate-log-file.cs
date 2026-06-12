using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        // Path to the Visio file to process
        private const string DiagramPath = "input.vsdx";
        // Path to the output Visio file after modifications
        private const string OutputDiagramPath = "output.vsdx";
        // Path to the log file that records user-defined cell changes
        private const string LogFilePath = "cell_changes.log";

        static void Main()
        {
            try
            {

                // Load the diagram from file
                Diagram diagram = new Diagram(DiagramPath);

                // Prepare a log writer (append mode)
                using (StreamWriter logWriter = new StreamWriter(LogFilePath, append: true))
                {
                    // Dictionary to store original values of user-defined cells
                    // Key: Tuple of Shape ID and User cell Name
                    Dictionary<(long shapeId, string userName), string> originalValues = new();

                    // First pass: capture current values of all user-defined cells
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            foreach (User userCell in shape.Users)
                            {
                                var key = (shape.ID, userCell.Name);
                                originalValues[key] = userCell.Value.Val;
                            }
                        }
                    }

                    // Example modification: for each shape, if a user cell named "Version" exists, increment its numeric value
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            foreach (User userCell in shape.Users)
                            {
                                if (userCell.Name.Equals("Version", StringComparison.OrdinalIgnoreCase))
                                {
                                    string oldValue = userCell.Value.Val;
                                    // Try to parse as integer, otherwise default to 0
                                    if (!int.TryParse(oldValue, out int versionNumber))
                                        versionNumber = 0;
                                    versionNumber++; // Increment version
                                    string newValue = versionNumber.ToString();
                                    userCell.Value.Val = newValue;

                                    // Log the change
                                    string logEntry = $"Shape ID {shape.ID}, User Cell '{userCell.Name}': '{oldValue}' -> '{newValue}' at {DateTime.Now:u}";
                                    logWriter.WriteLine(logEntry);
                                }
                            }
                        }
                    }

                    // Additional example: add a new user-defined cell to the first shape on the first page
                    if (diagram.Pages.Count > 0 && diagram.Pages[0].Shapes.Count > 0)
                    {
                        Shape firstShape = diagram.Pages[0].Shapes[0];
                        User newUserCell = new User
                        {
                            Name = "LastModified",
                            Value = { Val = DateTime.Now.ToString("o") },
                            Prompt = { Value = "Timestamp of last modification" }
                        };
                        firstShape.Users.Add(newUserCell);

                        // Log the addition
                        string additionLog = $"Shape ID {firstShape.ID}, Added User Cell '{newUserCell.Name}' with value '{newUserCell.Value.Val}' at {DateTime.Now:u}";
                        logWriter.WriteLine(additionLog);
                    }
                }

                // Save the modified diagram
                diagram.Save(OutputDiagramPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }