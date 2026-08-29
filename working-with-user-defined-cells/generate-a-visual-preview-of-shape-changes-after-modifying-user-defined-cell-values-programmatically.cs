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
                    // Look for a user-defined cell named "CustomValue"
                    bool found = false;
                    foreach (User user in shape.Users)
                    {
                        if (user.Name == "CustomValue")
                        {
                            // Update existing cell value
                            user.Value.Val = "12345";
                            found = true;
                            break;
                        }
                    }

                    // If the cell does not exist, create it
                    if (!found)
                    {
                        User newUser = new User();
                        newUser.Name = "CustomValue";
                        newUser.Value.Val = "12345";
                        shape.Users.Add(newUser);
                    }
                }
            }

            // Save a visual preview of the modified diagram as a PNG image
            string outputPath = "preview.png";
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Preview image saved to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
