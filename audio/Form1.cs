using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace audio
{
    public partial class Form1 : Form
    {
        public string selectedFile { get; set; }
        public Settings settings { get; set; }
        public Renderer renderer = new Renderer();
        public Form1()
        {
            settings =new Settings();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "WAV files|*.wav";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                selectedFile = ofd.FileName;
                RenderWaveform();
            }
        }
        private void RenderWaveform()
        {
            if (selectedFile == null) return;
            //var settings = GetRendererSettings();

            pictureBox1.Image = null;
            Enabled = false;
            Task.Factory.StartNew(RenderThreadFunc);
        }

        private object GetRendererSettings()
        {
            throw new NotImplementedException();
        }

        private void RenderThreadFunc()
        {
            Image image = null;
            try
            {
                image = renderer.Render(selectedFile, settings);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            BeginInvoke((Action)(() => FinishedRender(image)));
        }

        private void FinishedRender(Image image)
        {
            pictureBox1.Image = image;
            Enabled = true;
        }

    }
}
