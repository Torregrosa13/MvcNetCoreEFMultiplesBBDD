using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosOracle : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosOracle(HospitalContext context)
        {
            this.context = context;
        }
        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            string sql = "begin ";
            sql += "SP_ALL_VEMPS (:p_cursor_emps);";
            sql += " end;";
            OracleParameter pamCur = new OracleParameter();
            pamCur.ParameterName = "p_cursor_emps";
            pamCur.Value = null;
            pamCur.Direction = System.Data.ParameterDirection.Output;
            pamCur.OracleDbType = OracleDbType.RefCursor;
            var consulta = this.context.EmpleadosView.FromSqlRaw(sql, pamCur);
            return await consulta.ToListAsync();
        }
        public Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            throw new NotImplementedException();
        }
    }
}
