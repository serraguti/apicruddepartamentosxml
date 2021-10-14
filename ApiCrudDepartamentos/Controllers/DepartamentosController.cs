using ApiCrudDepartamentos.Helpers;
using ApiCrudDepartamentos.Models;
using ApiCrudDepartamentos.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ApiCrudDepartamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class DepartamentosController : ControllerBase
    {
        RepositoryDepartamentos repo;

        public DepartamentosController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<string> GetDepartamentos()
        {
            List<Departamento> lista = this.repo.GetDepartamentos();
            String salida = 
            HelperSerializacion.Serialize<List<Departamento>>(lista);
            return salida;
        }

        [HttpGet("{id}")]
        public ActionResult<string> BuscarDepartamento(int id)
        {
            Departamento dept = this.repo.BuscarDepartamento(id);
            String salida =
            HelperSerializacion.Serialize<Departamento>(dept);
            return salida;
        }

        //[HttpGet]
        //public ActionResult<List<Departamento>> GetDepartamentos()
        //{
        //    return this.repo.GetDepartamentos();
        //}

        //[HttpGet("{id}")]
        //public ActionResult<Departamento> BuscarDepartamento(int id)
        //{
        //    return this.repo.BuscarDepartamento(id);
        //}

        //EN LAS CONSULTAS DE ACCION SOLAMENTE TENDREMOS
        //(POR DEFECTO) UN METODO POST, PUT Y DELETE
        //LOS METODOS DE ACCION NO DEVUELVEN NADA (void)
        //PUEDEN DEVOLVER RESPUESTAS HTTP SI ALGO NO NOS GUSTA
        //PODRIAN SER TANTO VOID COMO ACTIONRESULT
        //SOLAMENTE SI SON ACTIONRESULT, PODREMOS DEVOLVER 
        //RESPUESTAS HTTP
        //public ActionResult Insertar()
        //{
        //    return new HttpStatusCodeResult(500);
        //}

        //LO QUE SE RECIBE SON OBJETOS
        //SI QUEREMOS PARAMETROS, TENDRIAMOS QUE REALIZAR
        //[Route("{id}/{nombre}/{localidad}"]
        //POST api/[controller]/99/I+D/GIJON
        //POST api/[controller]
        //[HttpPost]
        //public void InsertarDepartamento(Departamento departamento)
        //{
        //    this.repo.InsertarDepartamento(departamento.IdDepartamento
        //        , departamento.Nombre, departamento.Localidad);
        //}

        [HttpPost]
        public IActionResult InsertarDepartamentoXML
            ([FromBody] XElement request)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Departamento));
            using (TextReader reader = new StringReader(request.ToString()))
            {
                Departamento result = 
                    (Departamento)serializer.Deserialize(reader);
                this.repo.InsertarDepartamento(result.IdDepartamento
                                , result.Nombre, result.Localidad);
            }
            //return Ok();
            return StatusCode(StatusCodes.Status200OK,
                    "Todo OK Jose Luis");
        }

        //PUT api/[controller]
        //[HttpPut]
        //public void ModificarDepartamento(Departamento departamento)
        //{
        //    this.repo.ModificarDepartamento(departamento.IdDepartamento
        //        , departamento.Nombre, departamento.Localidad);
        //}


        [HttpPut]
        public IActionResult ModificarDepartamento([FromBody] XElement request)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Departamento));
            using (TextReader reader = new StringReader(request.ToString()))
            {
                Departamento result =
                    (Departamento)serializer.Deserialize(reader);
                this.repo.ModificarDepartamento(result.IdDepartamento
                                , result.Nombre, result.Localidad);
            }
            return StatusCode(StatusCodes.Status200OK,
                    "Todo OK Jose Luis");
        }

        //DELETE api/[controller]/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarDepartamento(int id)
        {
            this.repo.EliminarDepartamento(id);
            return StatusCode(StatusCodes.Status200OK,
                    "Todo OK Jose Luis");
        }
    }
}
