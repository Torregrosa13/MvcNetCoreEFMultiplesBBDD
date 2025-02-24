﻿using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    #region VIEWS
    /*
     create view V_EMPS
    as 	
        select 
        EMP_NO as IDEMPLEADO, EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO, 
        DEPT.DNOMBRE AS DEPARTAMENTO, 
        DEPT.LOC AS LOCALIDAD 	
        from EMP 	
        inner join DEPT 	
        on EMP.DEPT_NO = DEPT.DEPT_NO 
    go
     */

    /*
     create or replace view V_EMPS
       as
       select EMP.EMP_NO as IDEMPLEADO,
       EMP.APELLIDO, EMP.OFICIO,
       EMP.SALARIO, DEPT.DNOMBRE AS DEPARTAMENTO
       , DEPT.LOC AS LOCALIDAD
       from EMP
       inner join DEPT
       on EMP.DEPT_NO = DEPT.DEPT_NO;
     */
    #endregion

    #region PROCEDURES
    /*
     CREATE PROCEDURE SP_ALL_VEMPS
    as
	select * from V_EMPS
    go
     */

    /*
     create or replace procedure SP_ALL_VEMPS
        (p_cursor_emps out sys_refcursor)
        as
        begin
          open p_cursor_emps for 
          select * from V_EMPS;
        end;
    */
    #endregion
    public class RepositoryEmpleados: IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            //var consulta = from datos in this.context.EmpleadosView
            //               select datos;
            //return await consulta.ToListAsync();
            string sql = "SP_ALL_VEMPS";
            var consulta = this.context.EmpleadosView.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }

        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            var consulta = from datos in this.context.EmpleadosView
                           where datos.IdEmpleado == idEmpleado
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }
    }
}
