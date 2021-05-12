using System;
namespace qsLogPack.Infrastructures.Interfaces
{
    public interface ILogTxtRespository
    {
         void Create(Exception ex);
         void Create(string log);
    }
}