namespace TextReaderTwo
{
    using System.IO;
    using System.Windows.Forms;
    using System.Text.Json;
    using System.Xml;
    using System.Text;

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

                string erJson = IsJson(fileContent);
                bool erXml = IsXml(fileContent);

                if (erJson == "error")
                    label1.Text = erJson;
                else if (erXml)
                    label1.Text = "XML";
                else
                    label1.Text = fileContent;

            }




        }

        public static String IsJson(string text)
        {
            try
            {

                var doc = JsonDocument.Parse(text);
                var array = doc.RootElement.EnumerateArray();
                StringBuilder textBuilder = new StringBuilder();

                foreach (var element in array)
                {
                    
                    var navn = element.GetProperty("navn").GetString();
                    var adresse = element.GetProperty("adresse").GetString();
                    var tlf_nummer = element.GetProperty("tlf_nummer").GetString();
                    
                    textBuilder.AppendLine($"name: {navn}, adresse: {adresse}, tlf: {tlf_nummer}");
                }
                return textBuilder.ToString();

           
            }
            catch (JsonException)
            {
                return "error";
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