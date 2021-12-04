using System.Threading.Tasks;

namespace Albergue.Administrator.Services
{
    public interface IImageExtractor
    {
        Task SaveLocallyAsync();
    }
}