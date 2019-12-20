using Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace PersistenciaDapper
{
    public class Persistencia
    {
        #region Gênero

        public List<Genero> ConsultarGenero()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoLocadora"].ConnectionString))
            {
                return db.Query<Genero>("SELECT ID, NOME, DTACRIACAO, ATIVO FROM GENERO").ToList();
            }
        }

        public void IncluirGenero(Genero genero)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoLocadora"].ConnectionString))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Nome", genero.Nome, DbType.AnsiString);
                parametros.Add("@DtaCriacao", genero.DtaCriacao, DbType.DateTime);
                parametros.Add("@Ativo", genero.Ativo, DbType.Boolean);

                db.Execute("INSERT INTO GENERO (NOME, DTACRIACAO, ATIVO) Values(@Nome, @DtaCriacao, @Ativo);", parametros);
            }
        }

        public void AlterarGenero(Genero genero)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoLocadora"].ConnectionString))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Nome", genero.Nome, DbType.AnsiString);
                parametros.Add("@DtaCriacao", genero.DtaCriacao, DbType.DateTime);
                parametros.Add("@Ativo", genero.Ativo, DbType.Boolean);
                parametros.Add("@Id", genero.Id, DbType.Int32);

                db.Execute("UPDATE GENERO SET NOME = @Nome, DTACRIACAO = @DtaCriacao, ATIVO = @Ativo WHERE ID = @Id;", parametros);
            }
        }

        public void ExcluirGenero(Int32 Id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoLocadora"].ConnectionString))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Id", Id, DbType.Int32);

                db.Execute("DELETE FROM GENERO WHERE ID = @Id;", parametros);
            }
        }

        #endregion

        #region Locação

        public List<Locacao> ConsultarLocacao(int Id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoLocadora"].ConnectionString))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Id", Id, DbType.Int32);

               return db.Query<Locacao>("SELECT ID, CPF, DTALOCACAO FROM LOCACAO WHERE ID = @Id;", parametros).ToList();
            }
        }

        public List<Filme> ConsultarFilmeLocado(int Id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoLocadora"].ConnectionString))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Id", Id, DbType.Int32);

                return db.Query<Filme>("SELECT f.id, f.nome FROM Filme f JOIN LocacaoFilmes lf ON lf.Filme_Id = f.Id WHERE lf.Locacao_Id = @Id;", parametros).ToList();
            }
        }

        public void ExcluirLocacaoFilmes(int Id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoLocadora"].ConnectionString))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Id", Id, DbType.Int32);

                db.Execute("DELETE FROM DBO.LOCACAOFILMES WHERE LOCACAO_ID = @Id;", parametros);
            }
        }

        public void InserirLocacaoFilmes(int locacaoId, int filmeId)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoLocadora"].ConnectionString))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Locacao_Id", locacaoId, DbType.Int32);
                parametros.Add("@Filme_Id", filmeId, DbType.Int32);

                db.Execute("INSERT INTO LOCACAOFILMES (LOCACAO_ID, FILME_ID) Values(@Locacao_Id, @Filme_Id);", parametros);
            }
        }

        #endregion

    }
}
