﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsAdminResidentes.Services;
using WsAdminResidentes.Models;
using WsAdminResidentes.Classes;
using System.Net;
using WsAdminResidentes.Models.Utilidades;
using System.IO;
using WsAdminResidentes.Services.Utilidades;
using EvaluadorFinancieraWS.Services.Utilidades;
using Microsoft.AspNetCore.Http;

namespace WsAdminResidentes.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ArchivosController : ControllerBase
    {
        private IArchivosService _service;
        public ArchivosController(IArchivosService service)
        {
            this._service = service;
        }

        [HttpPost]
        public ActionResult Subir([FromForm] ArchivoModel std)
        {
            try {
                string Nombre = std.Nombre;
                var Archivo = std.Archivo;
                Guid ruta = this._service.SubirArchivo(Archivo);

                return Ok(new {
                    Ruta = ruta,
                    Exito = 1,
                    Mensaje = "Archivo Cargado correctamente"
                });
            }
            catch (Exception er) {
                return BadRequest(er);
            }
        }

        [HttpPost("CargarExcel")]
        public ActionResult SubirExcel([FromForm] IFormFile archivo)
        {
            //RESTRINGIR A EXCEL
            try
            {
                string Nombre = "carga";
                _service.AgregarFormato("xlsx");
                Guid ruta = this._service.SubirArchivo(archivo);

                return Ok(new
                {
                    Ruta = ruta,
                    Exito = 1,
                    Mensaje = "Archivo Cargado correctamente"
                });
            }
            catch (Exception er)
            {
                return BadRequest(er);
            }
        }

        [HttpPost("Subir")]
        public ActionResult Subir([FromForm] IFormFile avatar)
        {
            //RESTRINGIR A IMAGNES
            try
            {
                string Nombre = "Avatar";
                Guid ruta = this._service.SubirArchivo(avatar);

                return Ok(new
                {
                    Ruta = ruta,
                    Exito = 1,
                    Mensaje = "Archivo Cargado correctamente"
                });
            }
            catch (Exception er)
            {
                return BadRequest(er);
            }
        }
    }
}