/* *******************************************************************************************
 * CommandTest.cs
 * 
 * Rebar Comments (API REVIT 2023) 
 * Application (add-ins)
 * -------------------------------------------------------------------------------------------
 * Visual Studio 2022 
 * C# | .NET Framework 4.8
 * -------------------------------------------------------------------------------------------
 * Revit API docs: 
 * https://www.revitapidocs.com/2023/
 * 
 * Writing sgiman @ 2024 
 * *******************************************************************************************/

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Windows.Forms;

namespace RebarComments
{
    // -------------------------
    //          EMPTY
    // -------------------------
    /// <summary>
    /// Реализация revit add-in IExternalCommand interface
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class CommandTest : IExternalCommand
    {

        // **************************
        //           MAIN
        //       (Enter Point)
        // **************************
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            MessageBox.Show("EMPTY");

            // -------------------------
            //   Code goes here ....
            // -------------------------

            // Get application and document objects
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            // Grab Level
            FilteredElementCollector colLevels =
                new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.INVALID)
                .OfClass(typeof(Level));

            Element firstLevel = colLevels.FirstElement();

            return Result.Succeeded;

        } // ---  Execute (main) ---


    } // --- CommandTest : IExternalCommand ---


} // --- namespace RebarComments ---
