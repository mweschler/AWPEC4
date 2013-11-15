using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EC4
{
    [Serializable]
    class TextDoc : INotifyPropertyChanged
    {
        private string m_text = "";

        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
     
        public string Text
        {
            get { return m_text; }
            set {
                m_text = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }

    }
}
