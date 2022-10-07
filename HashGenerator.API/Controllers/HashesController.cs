
using HashGenerator.Core.Queries;
using HashGenerator.Producer;
using HashGenerator.Shared.Contracts;
using HashGenerator.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HashGenerator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashesController : ControllerBase
    {
        private readonly IMediator _mediator;   
        private readonly ISha1Service _sha1Service;
        private readonly IHashGeneratorProducer _hashGeneratorProducer;
        public HashesController(
            IMediator mediator,
            ISha1Service sha1Service,
            IHashGeneratorProducer hashGeneratorProducer)
        {
            _hashGeneratorProducer = hashGeneratorProducer;
            _sha1Service = sha1Service;
            _mediator = mediator;
        }


        [HttpGet(template: nameof(GetHashes), Name = nameof(GetHashes))]
        public async Task<IActionResult> GetHashes()
        {
            var response = await _mediator.Send(new GetHashesQuery());

            return Ok(response);
        }

        [HttpPost(template: nameof(GenerateHashes), Name = nameof(GenerateHashes))]
        public async Task<IActionResult> GenerateHashes()
        {
            DateTime date = DateTime.Now;
            int hashToGenerate = 40000;
           
            try
            {
                Parallel.For(0, 4, i => {
                    
                    var dateAdd = date.AddDays(i).AddHours(i).AddMinutes(5+i).AddMilliseconds(30);

                    var thread = new Thread(()=>Process(hashToGenerate, dateAdd).Wait());

                    thread.Start();

                });

                
               await Task.Yield();
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }

            return Ok();
        }

        private async Task Process(int count, DateTime date) {                       

            var result = _sha1Service.Generate(count, date);

            await _hashGeneratorProducer.Publish<List<HashesDTO>>(result, $"hash_{Guid.NewGuid().ToString()}");
                     
        }
    }
}
