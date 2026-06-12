using System.IO;
using System;
using Aspose.Diagram;

class ApplyThemeWithErrorHandling
{
    static void Main()
    {
        try
        {

            // Load an existing diagram (the load rule will handle the actual loading)
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve a shape – here we take the first shape on the first page
            Shape shape = diagram.Pages[0].Shapes[0];

            try
            {
                // Attempt to apply a preset theme style matrix to the shape.
                // If the shape lacks the necessary style data, an exception will be thrown.
                shape.SetPresetThemeStyleMatrics(
                    PresetStyleMatricsValue.Style1,
                    PresetColorMatricsValue.Color1);
            }
            catch (Exception ex)
            {
                // Handle the error gracefully – log or inform the user.
                Console.WriteLine($"Error applying theme to shape: {ex.Message}");
            }

            // Save the modified diagram (the save rule will handle the actual saving)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
