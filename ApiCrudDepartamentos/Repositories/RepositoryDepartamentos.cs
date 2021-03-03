using ApiCrudDepartamentos.Data;
using ApiCrudDepartamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudDepartamentos.Repositories
{
    public class RepositoryDepartamentos
    {
        DepartamentosContext context;
        public RepositoryDepartamentos(DepartamentosContext context)
        {
            this.context = context;
        }

        public List<Departamento> GetDepartamentos()
        {
            return this.context.Departamentos.ToList();
        }

        public Departamento BuscarDepartamento(int id)
        {
            return this.context.Departamentos
                .SingleOrDefault(x => x.IdDepartamento == id);
        }

        public void InsertarDepartamento(int id, String nombre
            , String localidad)
        {
            Departamento departamento = new Departamento();
            departamento.IdDepartamento = id;
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            this.context.Departamentos.Add(departamento);
            this.context.SaveChanges();
        }

        public void ModificarDepartamento(int id, String nombre
            , String localidad)
        {
            Departamento departamento = this.BuscarDepartamento(id);
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            this.context.SaveChanges();
        }

        public void EliminarDepartamento(int id) {
            Departamento departamento = this.BuscarDepartamento(id);
            this.context.Departamentos.Remove(departamento);
            this.context.SaveChanges();
        }
    }
}
