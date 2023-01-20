using InOutLib;
using static InOutLib.CsvHelper;

namespace TestInOutLib
{
    public class TestCsvHelper
    {
        #region private attributs
        private CsvHelper _csvHelper;
        private static string _path = Directory.GetCurrentDirectory();
        private static string _fileName = "testFile.csv";
        private char _separator = ';';
        #endregion private attributs

        [SetUp]
        public void Setup()
        {
            if (File.Exists(_path + "//" + _fileName))
            {
                File.Delete(_path + "//" + _fileName);
            }
            StreamWriter streamWriter = new StreamWriter(_path + "//" + _fileName);
            streamWriter.Close();
        }
        
        [Test]
        public void CsvHelper_UnsupportedSeparator_ThrowException()
        {
            //given
            //refer to Setup() method
            char separator = '+';

            //when
            //Event is triggered by the assertion

            //then
            Assert.Throws<UnsupportedSeparatorException>(() => new CsvHelper(_path, _fileName, separator));
        }

        [Test]
        public void CsvHelper_UnstructuredLine_ThrowException()
        {
            //given
            //refer to Setup() method
            StreamWriter streamWriter = new StreamWriter(_path + "//" + _fileName);
            for (int i = 0; i < 10; i++)
            {
                streamWriter.WriteLine("" + _separator + _separator + _separator);//"" to force the string conversion
            }
            streamWriter.WriteLine("unstructredLine");//we add a last with an other structure
            streamWriter.Close();
            _csvHelper = new CsvHelper(_path, _fileName, _separator);

            //then
            //Event is triggered by the assertion

            //when
            Assert.Throws<UnsupportedSeparatorException>(() => _csvHelper.ExtractFileContent());
        }

        [Test]
        public void CsvHelper_EmptyLine_ThrowException()
        {
            //given
            //refer to Setup() method
            StreamWriter streamWriter = new StreamWriter(_path + "//" + _fileName);
            for (int i = 0; i < 10; i++)
            {
                streamWriter.WriteLine("" + i + _separator + i);//"" to force the string conversion
            }
            streamWriter.WriteLine("");
            streamWriter.Close();
            _csvHelper = new CsvHelper(_path, _fileName, _separator);

            //then
            //Event is triggered by the assertion

            //when
            Assert.Throws<CsvHelper.StructureException>(() => _csvHelper.ExtractFileContent());
        }

        [Test]
        public void CsvHelper_NominalCase_GetFileContent()
        {
            //given
            //refer to Setup() method
            int expectedAmountOfLines = 20;
            int actualAmountOfLines = 0;
            StreamWriter streamWriter = new StreamWriter(_path + "//" + _fileName);
            for (int i = 0; i < expectedAmountOfLines; i++)
            {
                streamWriter.WriteLine(i + ";" + ";" + ";");
            }
            streamWriter.Close();
            _csvHelper = new CsvHelper(_path, _fileName);
            _csvHelper.ExtractFileContent();

            //when
            actualAmountOfLines = _csvHelper.Lines.Count;

            //then
            Assert.That(actualAmountOfLines, Is.EqualTo(expectedAmountOfLines));
        }

        [TearDown]
        public void Cleanup()
        {
            string[] listOfFilesResult = Directory.GetFiles(_path, "*.csv");
            foreach (string file in listOfFilesResult)
            {
                File.Delete(file);
            }
        }
    }
}
