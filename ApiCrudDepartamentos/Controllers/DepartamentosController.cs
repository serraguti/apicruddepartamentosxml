using ApiCrudDepartamentos.Models;
using ApiCrudDepartamentos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudDepartamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        RepositoryDepartamentos repo;

        public DepartamentosController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<List<Departamento>> GetDepartamentos()
        {
            return this.repo.GetDepartamentos();
        }

        [HttpGet("{id}")]
        public ActionResult<Departamento> BuscarDepartamento(int id)
        {
            return this.repo.BuscarDepartamento(id);
        }

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
        [HttpPost]
        public void InsertarDepartamento(Departamento departamento)
        {
            this.repo.InsertarDepartamento(departamento.IdDepartamento
                , departamento.Nombre, departamento.Localidad);
        }

        //PUT api/[controller]
        [HttpPut]
        public void ModificarDepartamento(Departamento departamento)
        {
            this.repo.ModificarDepartamento(departamento.IdDepartamento
                , departamento.Nombre, departamento.Localidad);
        }

        //DELETE api/[controller]/{id}
        [HttpDelete("{id}")]
        public void EliminarDepartamento(int id)
        {
            this.repo.EliminarDepartamento(id);
        }
    }
}
