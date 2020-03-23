using System.ComponentModel;

namespace Noter.ModelViews
{
    /// <summary>
    /// Base object for view models 
    /// Implements INotifyPropertyChanged interface and Invoke method
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Invoke property change
        /// </summary>
        /// <param name="property"></param>
        protected void NotifiyPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Event for invoking propery change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
