using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts;
using Application.DTOs;
using Application.DTOs.Cargo;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.DapperConnection;

namespace API.Controllers
{
    /// <summary>
    /// Interactua con la tabla cargo
    /// </summary>
    public class CargoController : BaseApiController
    {
        private readonly ICargoRepository _cargoRepository;
        private readonly IMapper _mapper;

        public CargoController(ICargoRepository cargoRepository, IMapper mapper,
        ILoggerService logger) : base(logger)
        {
            _mapper = mapper;
            _cargoRepository = cargoRepository;
        }
        
        /// <summary>
        /// Obtiene un cargo
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Un objeto cargo</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCargo(int id)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Se intentó obtener cargo con id {id}");
                var cargo = await _cargoRepository.FindById(id);

                if (cargo == null)
                {
                    _logger.LogWarn($"{location}: Cargo con id {id} no se encontró");
                    return NotFound();
                }

                var response = _mapper.Map<CargoDTO>(cargo);
                _logger.LogInfo($"{location}: Se encontro el cargo con id {id}");
                
                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }


        /// <summary>
        /// Obtiene todos los cargos
        /// </summary>
        /// <returns>Lista de cargos</returns>
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


        /// <summary>
        /// Actualiza un cargo. Se debe tener en cuenta si es que se envia un parametro vacio, 
        /// se actualizará tal cual. Se podría modificar la logica.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cargoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] CargoUpdateDTO cargoDto)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Intento de update con el registro: {id}");

                if (id < 1 || cargoDto == null || id != cargoDto.Id)
                {
                    _logger.LogInfo($"{location}: Falló el update con datos incorrectos - id: {id}");
                    return BadRequest();
                }

                var isExist = await _cargoRepository.IsExists(id);

                if (!isExist)
                    return InternalError($"{location}: Update falló");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location}: Datos incompletos");
                    return BadRequest(ModelState);
                }

                var cargo = _mapper.Map<Cargo>(cargoDto);
                var isSuccess = await _cargoRepository.Update(cargo);

                if (!isSuccess)
                    return InternalError($"{location}: Falló update con el registro: {id}");

                _logger.LogInfo($"{location}: Registro con id: {id} correctamente actualizado");
                return NoContent();

            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }


        /// <summary>
        /// Crea un cargo
        /// </summary>
        /// <param name="cargoDto"></param>
        /// <returns>Cargo</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CargoCreateDTO cargoDto)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"{location}: Intento de creación");

                if (cargoDto == null)
                {
                    _logger.LogWarn($"{location}: Se envió una solicitud vacía");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location}: Datos de cargo incompletos");
                    return BadRequest(ModelState);
                }

                var cargo = _mapper.Map<Cargo>(cargoDto);
                var isSuccess = await _cargoRepository.Create(cargo);

                if (!isSuccess)
                    return InternalError($"{location}: Creación de cargo falló");

                _logger.LogInfo($"{location}: Creación fue exitosa");
                _logger.LogInfo($"{location}: Id: {cargo.Id}, Nombre: {cargo.Nombre}");

                //respondo con objeto cargo, aunque deberia ser un dto por el tema de ID
                return Created("Create", new { cargo });


            }
            catch (System.Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }



        /// <summary>
        /// Elimina un cargo por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"{location}: Intento de eliminar cargo con id {id}");

                if (id < 1)
                {
                    _logger.LogInfo($"{location}: Datos incorrectos");
                    return BadRequest();
                }

                var isExist = await _cargoRepository.IsExists(id);

                if (!isExist)
                {
                    _logger.LogWarn($"{location}: Cargo con id {id} no encontrado");
                    return NotFound();
                }

                var cargo = await _cargoRepository.FindById(id);
                var isSuccess = await _cargoRepository.Delete(cargo);

                if (!isSuccess)
                    return InternalError($"{location}: Falló la eliminación.");

                _logger.LogWarn($"{location}: Cargo con id: {id} eliminado exitosamente");

                return NoContent();

            }
            catch (Exception e)
            {

                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

    
    
        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionCursoRequestDTO data)
        {
            return await _cargoRepository.GetPaginacion(data);
        }
    }
}