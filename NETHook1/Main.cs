// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="TODO: Company Name">
//   Copyright (c) 2022 TODO: Company Name
// </copyright>
// <summary>
//  If this project is helpful please take a short survey at ->
//  http://ux.mastercam.com/Surveys/APISDKSupport 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mastercam.App;
using Mastercam.App.Exceptions;
using Mastercam.App.Types;
using Mastercam.IO;
using Mastercam.IO.Types;
using Mastercam.Support.UI;
using NETHook1.Properties;
using NETHook1.Services;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace NETHook1
{
    /// <summary> Describes the main class. </summary>
    public class Main : NetHook3App
    {
        #region Public Override Methods

        /// <summary> Initialize anything we need for the NET-Hook here. </summary>
        ///
        /// <param name="param"> System parameter. </param>
        ///
        /// <returns> A <c>MCamReturn</c> return type representing the outcome of your NetHook application. </returns>
        public override MCamReturn Init(int param)
        {
            // Wire up handler for any global exceptions not handled by the app
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                this.HandleUnhandledException(args.ExceptionObject as Exception);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, args) => this.HandleUnhandledException(args.Exception);

            if (Properties.Settings.Default.FirstTimeRunning)
            {
                var msg = ResourceReaderService.GetString("FirstTimeRunning");
                var assembly = Assembly.GetExecutingAssembly().FullName;
                EventManager.LogEvent(MessageSeverityType.InformationalMessage, assembly, msg.IsSuccess ? msg.Value : msg.Error);

                Properties.Settings.Default.FirstTimeRunning = false;
                Properties.Settings.Default.Save();
            }

            return base.Init(param);
        }

        /// <summary> The main entry point for your NETHook1. </summary>
        ///
        /// <param name="param"> System parameter. </param>
        ///
        /// <returns> A <c>MCamReturn</c> return type representing the outcome of your NetHook application. </returns>
        public override MCamReturn Run(int param)
        {
            // Create our view
            var winView = new MainView { TopLevel = true };

            // Set the dialog as modeless to Mastercam, always on top
            var handle = Control.FromHandle(MastercamWindow.GetHandle().Handle);
            _ = new ModelessDialogTabsHandler(winView);

            winView.StartPosition = FormStartPosition.CenterScreen;
            winView.Show(handle);

            
            
            //MessageBox.Show("Hello from MCamReturn Run");

            return MCamReturn.NoErrors;

        }

        #endregion

        #region Public User Defined Methods

        /// <summary> The custom user function entry point for your NETHook1. </summary>
        ///
        /// <param name="param"> System parameter. </param>
        ///
        /// <returns> A <c>MCamReturn</c> return type representing the outcome of your NetHook application. </returns>
        public MCamReturn RunUserDefined(int param)
        {
            // read project resource strings

            var userMessage = ResourceReaderService.GetString("This is the userMessage Var");
            var title = ResourceReaderService.GetString("This is the title Var");


            DialogManager.OK(
                userMessage.IsSuccess ? userMessage.Value : userMessage.Error,
                title.IsSuccess ? title.Value : title.Error);
            return MCamReturn.NoErrors;
        }
        #endregion

        #region Private Methods

        /// <summary> Log exceptions and show a message. </summary>
        ///
        /// <param name="e"> The exception. </param>
        private void HandleUnhandledException(Exception e)
        {
            // Show the user
            DialogManager.Exception(new MastercamException(e.Message, e.InnerException));

            // Write to the event log
            var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
            var assembly = Assembly.GetExecutingAssembly().FullName;
            EventManager.LogEvent(MessageSeverityType.ErrorMessage, assembly, msg);
        }

        #endregion
    }
}
