// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationView.xaml.cs" company="DexTech">
//   2014
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FH.Views
{
    using FH.ViewModels;

    /// <summary>
    /// Interaction logic for "MainWindow.xaml"
    /// </summary>
    public partial class ApplicationView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationView"/> class.
        /// </summary>
        public ApplicationView()
        {
            this.DataContext = new ApplicationViewModel();
            this.InitializeComponent();
        }

        // TODO : Bring Button in focus
   }
}
