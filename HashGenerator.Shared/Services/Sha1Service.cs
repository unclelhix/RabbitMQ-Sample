
using HashGenerator.Shared.Contracts;
using HashGenerator.Shared.DTOs;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HashGenerator.Shared.Services
{
    public sealed class Sha1Service : ISha1Service
    {
        private readonly IDateTimeService _dateTimeService;
        public Sha1Service(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }
        public List<HashesDTO> Generate(long count)
        {
            var hashes = new List<HashesDTO>();

            for (int i = 0; i < count; i++)
            {
                hashes.Add(new HashesDTO
                { 
                    Sha1 = Hash(Guid.NewGuid().ToString("N")),
                    CreatedOn = _dateTimeService.Now
                });
            }

            return hashes;
        }
        public List<HashesDTO> Generate(long count, DateTime date)
        {
            var hashes = new List<HashesDTO>();

            for (int i = 0; i < count; i++)
            {
                hashes.Add(new HashesDTO
                {
                    Sha1 = Hash(Guid.NewGuid().ToString("N")),
                    CreatedOn = date
                });
            }

            return hashes;
        }

        private string SerializeObject(List<object> hashes)
        {

            byte[] utf8bytesJson = JsonSerializer.SerializeToUtf8Bytes(hashes);

            string strJson = Encoding.UTF8.GetString(utf8bytesJson);

            return strJson;
        }

        private string Hash(string guid)
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(guid);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", "");
                return hash;
            }
            
        }
   
    }
}
