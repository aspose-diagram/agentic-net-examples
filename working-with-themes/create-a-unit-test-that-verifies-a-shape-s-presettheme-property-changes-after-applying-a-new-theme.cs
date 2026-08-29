using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram (contains a default page)
        Diagram diagram;
        try
        {
            diagram = new Diagram(); // default constructor creates a blank VSDX diagram
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create diagram: {ex.Message}");
            return;
        }

        // Get the first page (index 0) to work with
        Page page = diagram.Pages[0];

        // Add a rectangle shape to the page; master name "Rectangle" exists in the default stencil
        long shapeId;
        try
        {
            shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to add shape: {ex.Message}");
            return;
        }

        // Retrieve the Shape object using the returned ID
        Shape shape = page.Shapes.GetShape(shapeId);
        if (shape == null)
        {
            Console.Error.WriteLine("Shape retrieval failed.");
            return;
        }

        // Record the initial fill foreground color (write‑only theme changes will affect this)
        string initialFill = shape.Fill.FillForegnd.Value;

        // Apply the first preset theme (Bubble) with Variant1
        try
        {
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to apply first theme: {ex.Message}");
            return;
        }

        // Capture fill color after first theme application
        string fillAfterFirstTheme = shape.Fill.FillForegnd.Value;

        // Apply a different variant of the same theme (Variant2)
        try
        {
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to apply second theme variant: {ex.Message}");
            return;
        }

        // Capture fill color after second variant application
        string fillAfterSecondTheme = shape.Fill.FillForegnd.Value;

        // Verify that the fill color changed after applying the first theme
        if (initialFill == fillAfterFirstTheme)
        {
            throw new Exception("PresetTheme did not modify the shape's fill color after first application.");
        }

        // Verify that the fill color changed again after applying the second variant
        if (fillAfterFirstTheme == fillAfterSecondTheme)
        {
            throw new Exception("PresetThemeVariant did not modify the shape's fill color after second application.");
        }

        // If we reach this point, the test succeeded
        Console.WriteLine("Shape PresetTheme property change verified successfully.");
    }
}