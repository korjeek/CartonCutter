using System.IO;
using System.Threading.Tasks;

namespace CartonCutter.Services.Interfaces;

public interface IProgramService<TResult>
{
    Task<TResult>? ExecuteProgramWithFile(Stream stream);
}