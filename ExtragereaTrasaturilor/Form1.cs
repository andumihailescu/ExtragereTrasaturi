using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml;
using WordFrequency;

namespace ExtragereaTrasaturilor
{
    public partial class Form1 : Form
    {
        private PorterStemmer porterStemmer = new PorterStemmer();
        private string testingFolderPath = "D:\\School\\Data Mining\\ExtragereaTrasaturilor\\Reuters\\Reuters_34\\Testing";
        private string trainingFolderPath = "D:\\School\\Data Mining\\ExtragereaTrasaturilor\\Reuters\\Reuters_34\\Training";
        private string stopWordsFilePath = "D:\\School\\Data Mining\\ExtragereaTrasaturilor\\ExtragereaTrasaturilor\\stopwords.txt";
        private List<string> stopWordsList;
        private List<string> globalVector = new();

        public Form1()
        {
            InitializeComponent();

            stopWordsList = LoadStopWords(stopWordsFilePath);

            OpenFiles();
        }

        private void CreateGlobalVector(List<string> words)
        {
            foreach (string word in words) 
            { 
                if (!globalVector.Contains(word))
                {
                    globalVector.Add(word);
                }
            }
        }

        public void OpenFiles()
        {
            

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

                    CreateGlobalVector(titleWords);
                    CreateGlobalVector(textWords);
                    CreateGlobalVector(codesWords);


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

                    CreateGlobalVector(titleWords);
                    CreateGlobalVector(textWords);
                    CreateGlobalVector(codesWords);

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

            // Extract globalVector from each selected element
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
            // Use a regular expression to extract globalVector (assumes globalVector are alphanumeric and may contain apostrophes)
            Regex regex = new Regex(@"\b[A-Za-z]+\b");

            // Match globalVector in the text
            MatchCollection matches = regex.Matches(text);

            // Convert matches to a list of strings
            List<string> words = new List<string>();
            foreach (Match match in matches)
            {
                string word = match.Value.ToLower();
                if (!stopWordsList.Contains(word))
                {
                    words.Add(porterStemmer.StemWord(word));
                    //globalVector.Add(match.Value);
                }
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

        static List<string> LoadStopWords(string filePath)
        {
            // Read stopwords from file and return as a list
            try
            {
                return File.ReadAllLines(filePath).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading stopwords: {ex.Message}");
                return new List<string>();
            }
        }
    }
}