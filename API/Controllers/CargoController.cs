using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts;
using Application.DTOs.Cargo;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CargoController : BaseApiController
    {
        private readonly ICargoRepository _cargoRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public CargoController(ICargoRepository cargoRepository, ILoggerService logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _cargoRepository = cargoRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCargos()
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"{location}: Intento de obtener cargos");
                var cargos = await _cargoRepository.FindAll();
                var response = _mapper.Map<IList<CargoDTO>>(cargos);
                _logger.LogInfo("Se obtuvieron cargos correctamente");

                return Ok(response);

            }
            catch (System.Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CargoCreateDTO cargoCreateDto)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"Intento de creación");

                if (cargoCreateDto == null)
                {
                    _logger.LogWarn($"Se envió una solicitud vacía");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Datos de cargo incompletos");
                    return BadRequest(ModelState);
                }

                var cargo = _mapper.Map<Cargo>(cargoCreateDto);
                var isSuccess = await _cargoRepository.Create(cargo);

                if (!isSuccess)
                    return InternalError("Creación de cargo falló");

                _logger.LogInfo($"{location}: Creación fue exitosa");
                _logger.LogInfo($"{location}: {cargo.Id}, {cargo.Nombre}");
                
                //respondo con objeto cargo, aunque deberia ser un dto
                return Created("Create", new { cargo }); 


            }
            catch (System.Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }

        private string GetControllerActionNames()
        {
            /*Porque este metodo? para examinar de forma rapida en el log desde donde sale 
            algun tipo de error*/

            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller} {action}";
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Algo salió mal. Por favor, contacte con el administrador.");
        }
    }
}