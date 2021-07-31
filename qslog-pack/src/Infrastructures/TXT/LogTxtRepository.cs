using System;
using System.IO;
using Microsoft.Extensions.Options;
using qsLogPack.Exceptions;
using qsLogPack.Infrastructures.Interfaces;
using qsLogPack.Models;

namespace qsLogPack.Infrastructures.TXT
{
    public class LogTxtRepository: ILogTxtRespository
    {
        private readonly QsLogSettings _logSettings;
        public LogTxtRepository(IOptions<QsLogSettings> logSettings)
        {
            if (logSettings == null)
            {
                throw new LogException("Configuracao do qsLog nao informada nos Settings");
            }

            _logSettings = logSettings.Value;
        }

        public void Create(string log)
        {
            try
            {
                string arquivo = _logSettings.PathLogTxt;
                if (string.IsNullOrWhiteSpace(arquivo))
                {
                    arquivo = "./logs";

                    if (!Directory.Exists(arquivo))
                    {
                        Directory.CreateDirectory(arquivo);
                    }
                }

                arquivo = arquivo + "/" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt";

                if (!File.Exists(arquivo))
                {
                    using FileStream fs = File.Create(arquivo);
                }

                using StreamWriter writer = File.AppendText(arquivo);
                writer.WriteLine(DateTime.Now.ToString());
                writer.WriteLine(log);
                writer.WriteLine("===========================================================================================");
                writer.Close();
            }
            catch
            {
                throw;
            }
        }

        public void Create(Exception ex)
        {
            try
            {
                string arquivo = _logSettings.PathLogTxt;
                if (string.IsNullOrWhiteSpace(arquivo))
                {
                    arquivo = "./logs";

                    if (!Directory.Exists(arquivo))
                    {
                        Directory.CreateDirectory(arquivo);
                    }
                }

                arquivo = arquivo + "/" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt";

                if (!File.Exists(arquivo))
                {
                    using FileStream fs = File.Create(arquivo);
                }

                using StreamWriter writer = File.AppendText(arquivo);
                writer.WriteLine(DateTime.Now.ToString());
                this.Create(ex, writer);
                writer.WriteLine("===========================================================================================");
                writer.Close();
            }
            catch
            {
                throw;
            }
        }

        private void Create(Exception ex, StreamWriter writer)
        {
            writer.WriteLine(" - EXCEPTION -  " + ex.Message);
            writer.WriteLine(" - TRACE - " + ex.StackTrace);
            writer.WriteLine("-----"); 

            if (ex.InnerException != null)
            {
                this.Create(ex.InnerException, writer);
            }
        }
    }
}