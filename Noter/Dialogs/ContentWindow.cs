using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Noter.Patterns;

namespace Noter.Dialogs
{
    /// <summary>
    /// Extends from Window. Facilitates work with window
    /// </summary>
    public class ContentWindow : Window
    {
        /// <summary>
        /// Create grid and add content to it
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public ContentWindow(string title, UserControl content)
        {
            this.Title = title;
            Grid grid = new Grid();
            grid.Children.Add(content);
            this.Content = grid;
            if (content is IClose) ((IClose)content).Close += ContentWindow_Close;

        }

        /// <summary>
        /// Set window size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Set icon to window by picture name. Use VisualizationDesignResourceDictionary
        /// </summary>
        /// <param name="name"></param>
        public void SetIcon(string name)
        {
            if (Application.Current.Resources.Contains(name))
                this.Icon = (BitmapImage)Application.Current.Resources[name];
        }

        private void ContentWindow_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((IClose)sender).Close -= ContentWindow_Close;
            this.DialogResult = !e.Cancel;
            this.Close();
        }
    }
}
