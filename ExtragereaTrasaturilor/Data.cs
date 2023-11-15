using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using WordFrequency;

namespace ExtragereaTrasaturilor
{
    internal class Data
    {

        private PorterStemmer porterStemmer;
        private string testingFolderPath;
        private string trainingFolderPath;
        private string stopWordsFilePath;
        private string largeDataFolderPath;
        private string outputFilePath;
        private List<string> stopWordsList;
        private List<string> globalVector;
        private List<List<string>> topicsList;
        private List<List<string>> rareVectors;
        private int fileIndex;
        private StreamWriter writer;
        private string folderName;

        public Data()
        {
            porterStemmer = new PorterStemmer();

            testingFolderPath = "D:\\School\\Data Mining\\ExtragereaTrasaturilor\\Reuters\\Reuters_34\\Testing";
            trainingFolderPath = "D:\\School\\Data Mining\\ExtragereaTrasaturilor\\Reuters\\Reuters_34\\Training";
            largeDataFolderPath = "D:\\School\\Data Mining\\ExtragereaTrasaturilor\\Reuters\\Reuters_7083";
            stopWordsFilePath = "D:\\School\\Data Mining\\ExtragereaTrasaturilor\\ExtragereaTrasaturilor\\stopwords.txt";
            outputFilePath = "D:\\School\\Data Mining\\ExtragereaTrasaturilor\\Output\\File.txt";
            stopWordsList = LoadStopWords(stopWordsFilePath);
            globalVector = new();
            rareVectors = new();
            topicsList = new();
        }

        public void ReadXmlsFromFolder(string folderPath)
        {
            try
            {
                string[] files = Directory.GetFiles(folderPath);

                foreach (string filePath in files)
                {
                    XmlDocument xmlDoc = new();
                    xmlDoc.Load(filePath);

                    List<string> extractedWords = ExtractWordsFromElement(xmlDoc, "//title");
                    extractedWords.AddRange(ExtractWordsFromElement(xmlDoc, "//text"));
                    List<string> codesWords = ExtractCodeAttributesFromElement(xmlDoc, "//codes[@class='bip:topics:1.0']/code");

                    rareVectors.Add(new List<string>());
                    writer.Write("D" + (fileIndex + 1) + " ");
                    AddWordsToGlobalVector(extractedWords);
                    foreach (string word in rareVectors[fileIndex])
                    {
                        writer.Write(word + " ");
                    }
                    topicsList.Add(codesWords);

                    writer.Write("# ");
                    foreach (string word in topicsList[fileIndex])
                    {
                        writer.Write(word + " ");
                    }
                    writer.WriteLine("# " + folderName);
                    writer.WriteLine();
                    fileIndex++;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void ProcessData()
        {
            fileIndex = 0;
            writer = new StreamWriter(outputFilePath);
            writer.WriteLine("@data");
            folderName = "Testing";
            ReadXmlsFromFolder(testingFolderPath);
            folderName = "Training";
            ReadXmlsFromFolder(trainingFolderPath);
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }

        public void ProcessLargeData()
        {
            fileIndex = 0;
            writer = new StreamWriter(outputFilePath);
            folderName = "LargeDataSet";
            writer.WriteLine("@data");
            ReadXmlsFromFolder(largeDataFolderPath);
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }

        public void AddWordsToGlobalVector(List<string> words)
        {
            while (words.Count > 0)
            {
                string word = words[0];

                if (!globalVector.Contains(word))
                {
                    globalVector.Add(word);
                }

                int position = globalVector.IndexOf(word);
                int occurrences = CountOccurrences(words, word);
                rareVectors[fileIndex].Add(position + ":" + occurrences);
                RemoveAllOccurrences(words, word);

            }
        }

        public int CountOccurrences(List<string> list, string searchString)
        {
            return list.Count(s => s == searchString);
        }

        public void RemoveAllOccurrences(List<string> list, string stringToRemove)
        {
            list.RemoveAll(item => item == stringToRemove);
        }

        public List<string> ExtractWordsFromElement(XmlDocument xmlDoc, string xpath)
        {
            XmlNodeList elements = xmlDoc.SelectNodes(xpath);

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
            XmlNodeList elements = xmlDoc.SelectNodes(xpath);

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
            Regex regex = new Regex(@"\b[A-Za-z]+\b");

            MatchCollection matches = regex.Matches(text);

            List<string> words = new List<string>();
            foreach (Match match in matches)
            {
                string word = match.Value.ToLower();
                if (!stopWordsList.Contains(word))
                {
                    words.Add(porterStemmer.StemWord(word));
                }
            }

            return words;
        }


        public List<string> LoadStopWords(string filePath)
        {
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