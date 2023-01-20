namespace InOutLib
{
    public class FileHelper
    {
        #region private attributs
        private string _path;//to the file
        private string _fileName;
        protected string _fullPath;//concat path + filename
        private List<string> _lines = new List<string>();//file content extracted
        #endregion private attributs

        #region public methods
        public FileHelper(string path, string fileName)
        {
            string fullPath = path + @"//" + fileName;
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException();
            }
            _path = path;
            _fileName = fileName;
            _fullPath = fullPath;
        }

        public List<string> Lines
        {
            get
            {
                return _lines;
            }
        }

        public void ExtractFileContent()
        {
            StreamReader streamReader = new StreamReader(_fullPath);
            string line;
            // Reads and stores lines from the file until eof.
            while ((line = streamReader.ReadLine()) != null)
            {
                this.Lines.Add(line);
            }
            streamReader.Close();

            if (this.Lines.Count == 0)
            {
                throw new EmptyFileException();
            }
        }

        public void Split(int fileSize)
        {
            int linesToPrint = _lines.Count;//used to detect when the orginal file is fully splitted
            int splitFileNumber = 0;//used to get a number of files

            while (linesToPrint != 0)
            {
                splitFileNumber++;
                string destinationfullPath = _path + @"//" + "Split" + splitFileNumber.ToString("00") + ".csv";
                StreamWriter streamWriter = new StreamWriter(destinationfullPath);
                int currentFileSize = 0;
                while (fileSize > currentFileSize)
                {
                    currentFileSize++;
                    linesToPrint--;
                    streamWriter.WriteLine(currentFileSize);
                }
                streamWriter.Close();
            }
        }
        #endregion public methods
    }

    #region nested classes
    public class FileHelperException : Exception { }

    public class EmptyFileException : FileHelperException { }
    
    public class StructureException : FileHelperException { }
    #endregion nested classes
}
