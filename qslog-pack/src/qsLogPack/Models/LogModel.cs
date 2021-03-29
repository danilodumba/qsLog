namespace qsLogPack.Models
{
    internal class LogModel
    {
        public LogModel(string description, string source, LogTypeEnum logType)
        {
            Description = description;
            Source = source;
            LogType = logType;
        }

        public LogModel(string description, LogTypeEnum logType)
        {
            Description = description;
            LogType = logType;
        }

        public string Description { get; set; }
        public string Source { get; set; }
        public LogTypeEnum LogType { get; set; }
    }

    internal enum LogTypeEnum
    {
        Information = 1,
        Warning = 2,
        Error = 3
    }
}