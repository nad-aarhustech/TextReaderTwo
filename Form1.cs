namespace TextReaderTwo
{
    using System.IO;
    using System.Windows.Forms;
    using System.Text.Json;
    using System.Xml;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)

            {
                string filePath = ofd.FileName;
                string fileContent = File.ReadAllText(filePath);

                bool erJson = IsJson(fileContent);
                bool erXml = IsXml(fileContent);

                if (erJson)
                    label1.Text = "JSON";
                else if (erXml)
                    label1.Text = "XML";
                else
                    label1.Text = fileContent;

            }




        }

        public static bool IsJson(string text)
        {
            try
            {
                var json = JsonDocument.Parse(text);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        public static bool IsXml(string text)
        {
            try
            {
                var xml = new XmlDocument();
                xml.LoadXml(text);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }
    }
}