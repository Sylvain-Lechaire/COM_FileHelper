using InOutLib;

namespace TestInOutLib
{
    public class TestsFileHelper
    {
        #region private attributs
        private FileHelper _fileHelper;
        private static string path = Directory.GetCurrentDirectory();
        private static string fileName = "testFile.csv";
        #endregion private attributs

        #region public methods
        [SetUp]
        public void Setup()
        {
            if (File.Exists(path + "//" + fileName))
            {
                File.Delete(path + "//" + fileName);
            }
            StreamWriter streamWriter = new StreamWriter(path + "//" + fileName);
            streamWriter.Close();
        }

        [Test]
        public void FileHelper_NominalCase_GetFileContent()
        {
            //given
            //refer to Init() method
            int expectedAmountOfLines = 20;
            int actualAmountOfLines = 0;
            StreamWriter streamWriter = new StreamWriter(path + "//" + fileName);
            for (int i = 0; i < expectedAmountOfLines; i++)
            {
                streamWriter.WriteLine(i);
            }
            streamWriter.Close();
            _fileHelper = new FileHelper(path, fileName);
            _fileHelper.ExtractFileContent();

            //when
            actualAmountOfLines = _fileHelper.Lines.Count;

            //then
            Assert.That(actualAmountOfLines, Is.EqualTo(expectedAmountOfLines));
        }

        [Test]
        public void FileHelper_InexistingFile_ThrowException()
        {
            //given
            string wrongPath = "falkjalj";
            string wrongFileName = "wrong.csv";

            //when
            //Event is triggered by the assertion

            //then
            Assert.Throws<FileNotFoundException>(() => new FileHelper(wrongPath, wrongFileName));
        }

        [Test]
        public void FileHelper_EmptyFile_ThrowException()
        {
            //given
            //refer to Init() method
            _fileHelper = new FileHelper(path, fileName);

            //when
            //Event is triggered by the assertion

            //then
            Assert.Throws<EmptyFileException>(() => _fileHelper.ExtractFileContent());
        }
        #endregion public methods
    }
}