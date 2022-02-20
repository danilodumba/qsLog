using qsLibPack.Application;
namespace qsLog.Applications.Core
{
    public abstract class Model<TKey>: Input
    {
        public TKey Id { get; set; }
    }
}