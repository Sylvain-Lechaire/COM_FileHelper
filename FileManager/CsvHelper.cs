namespace InOutLib
{
    public class CsvHelper : FileHelper
    {
        #region private attributs
        //TODO Private attributs - 2pts
        private static string _path;
        private char _separator = ';';

        #endregion private attributs

        #region constructor
        public CsvHelper(string path, string fileName, char separator = ';') : base(path, fileName)
        {
            //TODO Constructor - 3pts
            _separator = separator; 
            _path = path;   
        }
        #endregion constructor

        #region public methods 
        public void ExtractFileContent()
        {
            //TODO ExtractFileContent - 6pts
            

        }
        #endregion public methods

        #region private methods
        private bool IsCharSupported(char separator)
        {
            //TODO IsCharSupported - 2pts
            throw new NotImplementedException();
            //return _separator;
        }
        #endregion privates methods

        #region nested classes
        public class CsvHelperException : FileHelperException{}
        public class UnsupportedSeparatorException : CsvHelperException { }
        public class StructureException : CsvHelperException { }
        #endregion nested classes
    }
}
