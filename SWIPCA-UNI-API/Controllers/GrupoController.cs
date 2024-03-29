﻿using Microsoft.AspNetCore.Mvc;
using SWIPCA_UNI_API.DataAccess;
using SWIPCA_UNI_API.Models;

namespace SWIPCA_UNI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Grupo>>> GetObtenerGrupo()
        {
            var DA_Grupos = new DA_Grupo();
            var result = await DA_Grupos.ObtenerGrupo();

            return Ok(result);
        }
    }
}
