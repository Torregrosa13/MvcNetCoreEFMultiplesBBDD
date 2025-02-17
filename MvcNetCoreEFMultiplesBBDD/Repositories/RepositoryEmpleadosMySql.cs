using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    #region VIEWS Y PROCEDURES
    /*
     alter view V_EMPS
        as 	
        select 
        EMP_NO as IDEMPLEADO, EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO, 
        DEPT.DNOMBRE AS DEPARTAMENTO, 
        DEPT.LOC AS LOCALIDAD 	
        from EMP 	
        inner join DEPT 	
        on EMP.DEPT_NO = DEPT.DEPT_NO 
     */

    /*
     DELIMITER $$
    CREATE PROCEDURE SP_ALL_VEMPS()
    BEGIN
        SELECT * FROM V_EMPS;
    END $$

    DELIMITER ;

     */
    #endregion
    public class RepositoryEmpleadosMySql : IRepositoryEmpleados
    {
        HospitalContext context;

        public RepositoryEmpleadosMySql(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            //var consulta = from datos in this.context.EmpleadosView select datos;
            string sql = "CALL SP_ALL_VEMPS()";
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
