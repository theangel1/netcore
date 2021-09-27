using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts;
using Application.DTOs.Colaborador;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Interactua con la tabla colaborador
    /// </summary>
    public class ColaboradorController : BaseApiController
    {

        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly IMapper _mapper;

        public ColaboradorController(ILoggerService logger,
        IColaboradorRepository colaboradorRepository,
        IMapper mapper)
        : base(logger)
        {
            _mapper = mapper;
            _colaboradorRepository = colaboradorRepository;
        }

        /// <summary>
        /// Obtiene todos los colaboradores
        /// </summary>
        /// <returns>Una lista de colaboradores</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetColaboradores()
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Intento de obtener colaboradores");
                var colaboradores = await _colaboradorRepository.FindAll();
                var response = _mapper.Map<IList<ColaboradorDTO>>(colaboradores);
                _logger.LogInfo($"{location}: Correcto");
                return Ok(response);

            }
            catch (System.Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }

        }


        /// <summary>
        /// Obtiene un colaborador
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Un objeto colaborador</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetColaborador(int rut)
        {
            //No sé la regla de negocio, simplemente buscare por rut. en el repositorio se convierte a string

            var location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Intento de acceso para el rut {rut}");
                var colaborador = await _colaboradorRepository.FindById(rut);

                if (colaborador == null)
                {
                    _logger.LogWarn($"{location}: No se pudo obtener registro con Rut {rut}");
                    return NotFound();
                }

                var response = _mapper.Map<ColaboradorDTO>(colaborador);
                _logger.LogInfo($"{location}: Se obtuvo correctamente el RUT {rut}");
                return Ok(response);
            }
            catch (System.Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Crea un colaborador
        /// </summary>
        /// <param name="colaboradorCreateDTO"></param>
        /// <returns>Cargo</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        
        public async Task<IActionResult> Create([FromBody] ColaboradorCreateDTO colaboradorCreateDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"Intento de creación");

                if (colaboradorCreateDTO == null)
                {
                    _logger.LogWarn($"Se envió una solicitud vacía");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Datos de colaborador incompletos");
                    return BadRequest(ModelState);
                }

                var colaborador = _mapper.Map<Colaborador>(colaboradorCreateDTO);
                var isSuccess = await _colaboradorRepository.Create(colaborador);

                if (!isSuccess)
                    return InternalError("Creación de colaborador falló");

                _logger.LogInfo($"{location}: Creación fue exitosa");
                _logger.LogInfo($"{location}: Id: {colaborador.Rut}, Nombre: {colaborador.Nombres}");

                return Created("Create", new { colaborador });


            }
            catch (System.Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }


        /// <summary>
        /// Actualiza un colaborador. Hasta el momento no tengo logicas de negocio definidas
        /// </summary>
        /// <param name="rut"></param>
        /// <param name="colaboradorDto"></param>
        /// <returns>status 204</returns>
        [HttpPut("{rut}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int rut, [FromBody] ColaboradorUpdateDTO colaboradorDto)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Intento de update de registro con Rut {rut}");

                if (rut < 1 || colaboradorDto == null || rut.ToString() != colaboradorDto.Rut)
                {
                    _logger.LogInfo($"{location}: Falló update por datos incorrectos- Rut {rut}");
                    return BadRequest();
                }

                var isExists = await _colaboradorRepository.IsExists(rut);

                if (!isExists)
                    return InternalError($"{location}: Update falló");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location}: Datos incompletos");
                }
                var colaborador = _mapper.Map<Colaborador>(colaboradorDto);
                var isSuccess = await _colaboradorRepository.Update(colaborador);

                if (!isSuccess)
                    return InternalError($"{location}: Falló el update para el registro con Rut {rut}");

                _logger.LogInfo("{location}: Registro con Rut {rut} actualizado correctamente");
                return NoContent();
            }
            catch (System.Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Elimina un registro con parametro rut. No tengo logica de negocio respecto a este punto.
        /// </summary>
        /// <param name="rut">Rut sin puntos ni dv</param>
        /// <returns></returns>
        [HttpDelete("{rut}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int rut)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInfo($"{location}: Se intento eliminar un registro con Rut {rut}");

                if (rut < 1)
                {
                    _logger.LogWarn($"{location}: Data incorrecta");
                    return BadRequest();
                }

                var isExists = await _colaboradorRepository.IsExists(rut);

                if (!isExists)
                {
                    _logger.LogWarn($"{location}: No se pudo obtener registro con rut {rut}");
                    return NotFound();
                }

                var colaborador = await _colaboradorRepository.FindById(rut);
                var isSuccess = await _colaboradorRepository.Delete(colaborador);

                if (!isSuccess)
                    return InternalError($"{location}: Falló la eliminacion del registro con rut {rut}");

                _logger.LogInfo($"{location}: Registro con rut {rut} fue eliminado correctamente");
                return NoContent();

            }
            catch (System.Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }
    }
}