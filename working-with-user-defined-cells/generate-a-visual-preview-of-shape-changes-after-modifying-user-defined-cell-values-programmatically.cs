using System.IO;
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

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Look for a user-defined cell named "MyCustomValue"
                    bool cellFound = false;
                    foreach (User userCell in shape.Users)
                    {
                        if (userCell.Name == "MyCustomValue")
                        {
                            // Update the existing cell value
                            userCell.Value.Val = "1234";
                            userCell.Prompt.Value = "Updated value";
                            cellFound = true;
                            break;
                        }
                    }

                    // If the cell does not exist, create it
                    if (!cellFound)
                    {
                        User newUser = new User();
                        newUser.Name = "MyCustomValue";
                        newUser.Value.Val = "1234";
                        newUser.Prompt.Value = "Initial value";
                        shape.Users.Add(newUser);
                    }

                    // Apply changes to the shape
                    shape.RefreshData();
                }
            }

            // Configure image export options (PNG format)
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.PageIndex = 0; // Export the first page

            // Save the preview image
            string outputPath = "preview.png";
            diagram.Save(outputPath, imgOptions);

            Console.WriteLine($"Preview image saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
