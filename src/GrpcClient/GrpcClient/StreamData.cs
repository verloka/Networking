using System.ComponentModel;

namespace GrpcClient
{
    public class StreamData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        

        private string _value;
        public string Value
        {
            get => _value; 
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
