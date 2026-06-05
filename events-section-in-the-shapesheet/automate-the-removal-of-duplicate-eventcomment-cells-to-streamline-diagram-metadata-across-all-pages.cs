using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the input Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output_cleaned.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Dictionary to track unique EventComment values
                var seenComments = new System.Collections.Generic.Dictionary<string, User>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Collect users named "EventComment" for processing
                        var usersToCheck = new System.Collections.Generic.List<User>();
                        foreach (User user in shape.Users)
                        {
                            if (string.Equals(user.Name, "EventComment", StringComparison.OrdinalIgnoreCase))
                            {
                                usersToCheck.Add(user);
                            }
                        }

                        // Process each EventComment user cell
                        foreach (User user in usersToCheck)
                        {
                            string commentValue = user.Value?.Val ?? string.Empty;

                            if (seenComments.ContainsKey(commentValue))
                            {
                                // Duplicate found – remove this user cell from the shape
                                shape.Users.Remove(user);
                            }
                            else
                            {
                                // First occurrence – remember it
                                seenComments[commentValue] = user;
                            }
                        }
                    }
                }

                // Save the cleaned diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }