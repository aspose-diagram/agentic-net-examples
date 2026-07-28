using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new diagram (empty)
            Diagram diagram = new Diagram();

            // Ensure there is at least one window; create one if none exist
            if (diagram.Windows.Count == 0)
            {
                var newWindow = new Window();
                ConfigureWindow(newWindow);
                diagram.Windows.Add(newWindow);
            }
            else
            {
                // Configure the first existing window
                ConfigureWindow(diagram.Windows[0]);
            }

            // Save the diagram (optional demonstration)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }

        /// <summary>
        /// Configures a <see cref="Window"/> instance by setting its properties.
        /// Each property is accompanied by a comment that explains its purpose.
        /// </summary>
        /// <param name="window">The <see cref="Window"/> object to configure.</param>
        static void ConfigureWindow(Window window)
        {
            // Unique identifier of the window within its parent collection.
            window.ID = 1;

            // Determines the kind of UI window (Drawing, Stencil, Sheet, Icon).
            window.WindowType = WindowTypeValue.Drawing;

            // Controls the visual state of the window (Maximized, Minimized).
            window.WindowState = WindowStateValue.Maximized;

            // Height of the window rectangle (in inches).
            window.WindowHeight = 800;

            // Width of the window rectangle (in inches).
            window.WindowWidth = 1200;

            // Left coordinate of the window rectangle (in inches).
            window.WindowLeft = 100;

            // Top coordinate of the window rectangle (in inches).
            window.WindowTop = 50;

            // Shows or hides the grid in the drawing window.
            window.ShowGrid = BOOL.True;

            // Shows or hides the guide lines in the drawing window.
            window.ShowGuides = BOOL.True;

            // Shows or hides the rulers in the drawing window.
            window.ShowRulers = BOOL.True;

            // Shows or hides page break indicators in the window.
            window.ShowPageBreaks = BOOL.False;

            // Enables or disables the dynamic grid feature for the window.
            window.DynamicGridEnabled = BOOL.True;

            // Shows or hides connection points for shapes in the window.
            window.ShowConnectionPoints = BOOL.False;

            // Identifier of the container (Page, Sheet, or Master) that hosts this window.
            // Relevant when ContainerType is set. Here we leave it at the default (0).
            window.Container = 0;

            // Specifies the type of container (Document, Page, Master).
            // Not required for a simple drawing window; left at default.

            // Path to the document displayed in this window (used for stencil windows).
            // Not required for drawing windows; commented out.
            // window.Document = "C:\\Stencils\\MyStencil.vssx";

            // Master ID if this window displays a master.
            // Not required for drawing windows; commented out.
            // window.Master = 0;

            // Page ID if this window displays a page.
            // Not required for drawing windows; commented out.
            // window.Page = 0;

            // Parent window ID when this window is a stencil contained within another window.
            // Not required for top‑level drawing windows; commented out.
            // window.ParentWindow = 0;

            // Read‑only flag for stencil windows that are not document stencils.
            // Not required for drawing windows; commented out.
            // window.ReadOnly = BOOL.False;

            // Sheet ID if the container is a sheet.
            // Not required for drawing windows; commented out.
            // window.Sheet = 0;

            // Group identifier for merged stencil windows.
            // Not required for drawing windows; commented out.
            // window.StencilGroup = 0;

            // Position of this stencil within its group.
            // Not required for drawing windows; commented out.
            // window.StencilGroupPos = 0;

            // Fraction of total width allocated to the page tab control (0.0‑1.0).
            window.TabSplitterPos = 0.5;

            // Center point of the view in X coordinate (optional).
            // window.ViewCenterX = 0.0;

            // Center point of the view in Y coordinate (optional).
            // window.ViewCenterY = 0.0;

            // Scale factor of the view (optional, 1.0 = 100%).
            // window.ViewScale = 1.0;
        }
    }