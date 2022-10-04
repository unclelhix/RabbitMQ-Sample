
using HashGenerator.Shared.DTOs;

namespace HashGenerator.Shared.Contracts
{
    public interface ISha1Service
    {
        /// <summary>
        /// Generate Sha1 by count
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        List<HashesDTO> Generate(long count);
        /// <summary>
        /// Generate Sha1 by count and date
        /// </summary>
        /// <param name="count"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<HashesDTO> Generate(long count, DateTime date);
    }
}
