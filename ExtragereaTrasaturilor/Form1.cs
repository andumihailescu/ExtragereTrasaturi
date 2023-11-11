using System.Text.RegularExpressions;
using System.Xml;

namespace ExtragereaTrasaturilor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            OpenFiles();
        }

        public void OpenFiles()
        {
            string testingFolderPath = "D:\\School\\Data Mining\\ExtragereaTrasaturilor\\Reuters\\Reuters_34\\Testing";
            string trainingFolderPath = "D:\\School\\Data Mining\\ExtragereaTrasaturilor\\Reuters\\Reuters_34\\Training";


            string folderPath = testingFolderPath;

            try
            {
                string[] files = Directory.GetFiles(folderPath);

                foreach (string filePath in files)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);

                    List<string> titleWords = ExtractWordsFromElement(xmlDoc, "//title");
                    List<string> textWords = ExtractWordsFromElement(xmlDoc, "//text");
                    List<string> codesWords = ExtractCodeAttributesFromElement(xmlDoc, "//codes[@class='bip:topics:1.0']/code");

                    DisplayWords("Title Words", titleWords);
                    DisplayWords("Text Words", textWords);
                    DisplayWords("Codes Words", codesWords);
                }
            }
            catch (Exception ex)
            {

            }

            folderPath = trainingFolderPath;

            try
            {
                string[] files = Directory.GetFiles(folderPath);

                foreach (string filePath in files)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filePath);

                    List<string> titleWords = ExtractWordsFromElement(xmlDoc, "//title");
                    List<string> textWords = ExtractWordsFromElement(xmlDoc, "//text");
                    List<string> codesWords = ExtractCodeAttributesFromElement(xmlDoc, "//codes[@class='bip:topics:1.0']/code");

                    DisplayWords("Title Words", titleWords);
                    DisplayWords("Text Words", textWords);
                    DisplayWords("Codes Words", codesWords);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<string> ExtractWordsFromElement(XmlDocument xmlDoc, string xpath)
        {
            // Select the specified elements using XPath
            XmlNodeList elements = xmlDoc.SelectNodes(xpath);

            // Extract words from each selected element
            List<string> words = new List<string>();
            foreach (XmlNode element in elements)
            {
                string elementText = element.InnerText;
                words.AddRange(ExtractWords(elementText));
            }

            return words;
        }

        public List<string> ExtractCodeAttributesFromElement(XmlDocument xmlDoc, string xpath)
        {
            // Select the specified elements using XPath
            XmlNodeList elements = xmlDoc.SelectNodes(xpath);

            // Extract code attribute values from each selected element
            List<string> codeValues = new List<string>();
            foreach (XmlNode element in elements)
            {
                XmlAttribute codeAttribute = element.Attributes["code"];
                if (codeAttribute != null)
                {
                    string codeValue = codeAttribute.Value;
                    codeValues.Add(codeValue);
                }
            }

            return codeValues;
        }

        public List<string> ExtractWords(string text)
        {
            // Use a regular expression to extract words (assumes words are alphanumeric and may contain apostrophes)
            Regex regex = new Regex(@"\b[A-Za-z]+\b");

            // Match words in the text
            MatchCollection matches = regex.Matches(text);

            // Convert matches to a list of strings
            List<string> words = new List<string>();
            foreach (Match match in matches)
            {
                words.Add(match.Value);
            }

            return words;
        }

        public void DisplayWords(string header, List<string> words)
        {
            Console.WriteLine($"{header}:");
            foreach (string word in words)
            {
                Console.WriteLine($"- {word}");
            }
            Console.WriteLine();
        }
    }
}