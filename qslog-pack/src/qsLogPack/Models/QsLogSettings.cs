namespace qsLogPack.Models
{
    public class QsLogSettings
    {
        public string LogApi { get; set; }
        public string ApiKey { get; set; }
        public string PathLogTxt { get; set; }
        public bool GenerateLogTxt { get; set; } = true;
        public bool UseHabbitMQ { get; set; } = false;
    }
}