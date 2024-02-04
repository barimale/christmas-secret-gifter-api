using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Services.Abstractions
{
    public interface ILocalesGenerator
    {
        Task GenerateAsync();
    }
}