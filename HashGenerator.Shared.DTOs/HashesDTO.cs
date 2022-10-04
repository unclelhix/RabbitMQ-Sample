using HashGenerator.Models;
using Mapster;

namespace HashGenerator.Shared.DTOs
{
    public class HashesDTO : IRegister
    {
        public long? Id { get; set; }      
        public string Sha1 { get; set; }
        public DateTime CreatedOn { get; set; }
        
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Hashes,HashesDTO>();

            config.NewConfig<HashesDTO, Hashes>();
        }
    }
}
