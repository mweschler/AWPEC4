using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace EC4
{
    public partial class EC4 : Form
    {
        private string m_filename = "";
        private TextDoc m_document = new TextDoc();

        public EC4()
        {
            InitializeComponent();
            //Binding bind = new Binding("Text", this.m_document, "Text");
            this.textArea.DataBindings.Add("Text", this.m_document, "Text", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_document.Text = "";
            this.m_filename = "";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //see if we need to request a save location
            if (m_filename.CompareTo("") == 0)
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "Extra Credit 4 Docs|*.ec4| All Files|*.*";
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    m_filename = dlg.FileName;
                }
            }

            //save the data
            using (Stream stream = new FileStream(m_filename, FileMode.Create, FileAccess.Write))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, m_document);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Extra Credit 4 Docs|*.ec4|All Files|*.*";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    using (Stream stream = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        TextDoc data = formatter.Deserialize(stream) as TextDoc;

                        //update property manually to keep event bindings
                        m_document.Text = data.Text;
                    }
                    m_filename = dlg.FileName;
                }
                catch(Exception exc)
                {
                    MessageBox.Show("Could not open file:\n" + exc.ToString());
                }
            }
        }
    }
}
