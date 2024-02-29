/* *******************************************************************************************
 * Application.cs
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
 * ******************************************************************************************/

using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;

// ***************************
//         APPLICATION
// ***************************

namespace RebarComments
{
    // ----------------------
    //    add-in interface
    // ----------------------
    /// <summary>
    /// Реализация Revit add-in interface IExternalApplication
    /// </summary>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class Application : IExternalApplication
    {
        // --- On Shutdown ---
        /// <summary>
        /// Реализация Shutdown event
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }


        // --- On Startup ---
        /// <summary>
        /// Реализация OnStartup event
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel = RibbonPanel(application);
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            // -----  RebarCommentsAdd (command) -----
            if (panel.AddItem(new PushButtonData("Rebars\n Comments", "Rebars Comments", thisAssemblyPath, "RebarComments.RebarCommentsAdd"))
                is PushButton button1)
            {
                button1.ToolTip = "For the “Structural Rebar” family category, assign to the \"Comments\" parameter.";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "Register.png"));
                BitmapImage bitmapImage = new BitmapImage(uri);
                button1.LargeImage = bitmapImage;
            }

            // ----- CommandTest (command) -----
            if (panel.AddItem(new PushButtonData("Empty\n Test", "Empty Test", thisAssemblyPath, "RebarComments.CommandTest"))
                is PushButton button2)
            {
                button2.ToolTip = "Empty Test...";
                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "StrcturalWall.png"));
                BitmapImage bitmapImage = new BitmapImage(uri);
                button2.LargeImage = bitmapImage;
            }
            

            return Result.Succeeded;    

        }

        // ----------------------------------
        //           Ribbon Panel
        // ----------------------------------
        /// <summary>
        /// Function that creates RibbonPanel
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public RibbonPanel RibbonPanel(UIControlledApplication a)
        {
            string tab = "Rebars";

            RibbonPanel ribbonPanel = null;

            try
            {
                a.CreateRibbonTab(tab);
            }   
            catch (Exception ex) 
            {
               Debug.WriteLine(ex.Message);
            }

            try
            {
                RibbonPanel panel = a.CreateRibbonPanel(tab, "Rebars");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels.Where(p => p.Name == "Rebars"))
            {
                ribbonPanel = p;
            }

            return ribbonPanel;


        } // ---  RibbonPanel ---


    } //  --- Application : IExternalApplication ---

} // --- Class myFirstPlugin ---
