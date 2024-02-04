using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Services.Abstractions
{
    public interface IImageExtractor
    {
        Task SaveLocallyAsync();
    }
}