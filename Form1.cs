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

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                string fileContent = File.ReadAllText(filePath);

                string erJson = IsJson(fileContent);
                string erXml = IsXml(fileContent);

                if (erJson != "error")
                    label1.Text = erJson;
                else if (erXml != "error")
                    label1.Text = erXml;
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

        public static string IsXml(string text)
        {
            try
            {
                var xml = new XmlDocument();
                xml.LoadXml(text);

                StringBuilder textBuilder = new StringBuilder();

                foreach (XmlNode contact in xml.GetElementsByTagName("contact"))
                {
                    StringBuilder contactBuilder = new StringBuilder();

                    foreach (XmlNode node in contact.ChildNodes)
                    {
                        contactBuilder.Append(node.Name);
                        contactBuilder.Append(": ");
                        contactBuilder.Append(node.InnerText);
                        contactBuilder.Append(", ");
                    }

                    textBuilder.AppendLine(contactBuilder.ToString().TrimEnd(',', ' '));
                }

                return textBuilder.ToString();

            }
            catch (XmlException)
            {
                return "error";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}