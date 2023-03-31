// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainView.cs" company="TODO: Company Name">
//   Copyright (c) 2022 TODO: Company Name
//   Author mmp
// </copyright>
// <summary>
//  If this project is helpful please take a short survey at ->
//  http://ux.mastercam.com/Surveys/APISDKSupport 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows.Forms;

namespace NETHook1
{
    /// <summary> The main view. </summary>
    public partial class MainView : Form
    {
        /// <summary> Initializes a new instance of the <see cref="MainView"/> class. </summary>
        public MainView() => this.InitializeComponent();

        /// <summary> The on ok click. </summary>
        ///
        /// <param name="sender"> The sender of the event. </param>
        /// <param name="e">      The event arg. </param>
        private void OnOkClick(object sender, System.EventArgs e) =>
            MessageBox.Show(Properties.Resources.MessageTile, Properties.Resources.Message);

        /// <summary> The on close click. </summary>
        ///
        /// <param name="sender"> The sender of the event. </param>
        /// <param name="e">      The event arg. </param>
        private void OnCloseClick(object sender, System.EventArgs e) => this.Close();

        private void MainView_Load(object sender, System.EventArgs e)
        {

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            //Insert line geometry to be chained for plunge operation
            //One thing to fix, an extra click is required to bring focus to Mastercam before geometry can be selected

            //Creates point objects
            Mastercam.Math.Point3D selectedPoint = new Mastercam.Math.Point3D();
            Mastercam.Math.Point3D selectedPoint2 = new Mastercam.Math.Point3D();

            //Establishes the point objects from selection

            Mastercam.IO.SelectionManager.AskForPoint("Select point 1", Mastercam.IO.Types.PointMask.Null, ref selectedPoint);
            Mastercam.IO.SelectionManager.AskForPoint("Select point 2", Mastercam.IO.Types.PointMask.Null, ref selectedPoint2);

            DialogResult dialogResult = MessageBox.Show("Is this selection correct?", "Confirmation", MessageBoxButtons.OKCancel);

            if (dialogResult == DialogResult.OK)
            {

                double xs = -(selectedPoint.y + selectedPoint2.y);
                double xOffset = selectedPoint.x;
                double yOffset = -.01;
                double zOffset = selectedPoint.z;
                double toolDia = decimal.ToDouble(toolDiameterInput.Value);



                double pointOne = yOffset - (toolDia * .5);
                double pointTwo = -(xs + .05 + .075) - (toolDia * .5);



                Mastercam.Math.Point3D pt1 = new Mastercam.Math.Point3D(xOffset, pointOne, zOffset);
                Mastercam.Math.Point3D pt2 = new Mastercam.Math.Point3D(xOffset, pointTwo, zOffset);

                Mastercam.Curves.LineGeometry line = new Mastercam.Curves.LineGeometry(pt1, pt2);

                line.Selected.Equals(true);

                line.Commit();

            }

        }

        private void button3_Click(object sender, System.EventArgs e)
        {

            //Imports toolpaths for slitting operation

            //User selects the toolpaths  suing openfiledialog 
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"\\PAI5\SMMC Application Data\Mastercam\Ops\SLITTING FIXTURE\SLITTING";
            string filepath;
            openFileDialog.ShowDialog();

            filepath = openFileDialog.FileName;

            Mastercam.Operations.OperationsManager.ImportOptions importOptions = new Mastercam.Operations.OperationsManager.ImportOptions(filepath,0);

            Mastercam.Operations.OperationsManager.ImportAllOperations(importOptions);
        

        }
    }
}
