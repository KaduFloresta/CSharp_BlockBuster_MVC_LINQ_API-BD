using System.Linq;
using DbRespositorie;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class FilmeModels
    {
        /* 
            Getters and Setters 
        */

        /// <value> Get and Set the value of IdFilme </value>
        [Key] // Data Annotations - Main key
        public int IdFilme { get; set; }
        /// <value> Get and Set the value of Titulo </value>
        [Required] // Data Annotations - Mandatory data entry
        public string Titulo { get; set; }
        /// <value> Get and Set the value of DataLancamento </value>
        [Required]
        public string DataLancamento { get; set; }
        /// <value> Get and Set the value of Sinopse </value>
        [Required]
        public string Sinopse { get; set; }
        /// <value> Get and Set the value of ValorLocacaoFilme </value>
        [Required]
        public double ValorLocacaoFilme { get; set; }
        /// <value> Get and Set the value of EstoqueFilme </value>
        [Required]
        public int EstoqueFilme { get; set; }
        /// <value> Get and Set the value of FilmeLocado </value>
        public int FilmeLocado { get; set; }
        /// <value> Get and Set the value of locacoes </value>
        public List<LocacaoModels> locacoes = new List<LocacaoModels>();

        /// <summary>
        /// Constructor - FilmeModels Object
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="dataLancamento"></param>
        /// <param name="sinopse"></param>
        /// <param name="valorLocacaoFilme"></param>
        /// <param name="estoqueFilme"></param>
        public FilmeModels(string titulo, string dataLancamento, string sinopse, double valorLocacaoFilme, int estoqueFilme)
        {
            Titulo = titulo;
            DataLancamento = dataLancamento;
            Sinopse = sinopse;
            ValorLocacaoFilme = valorLocacaoFilme;
            EstoqueFilme = estoqueFilme;
            FilmeLocado = 0;

            var db = new Context();
            db.Filmes.Add(this);
            db.SaveChanges();
        }

        /// <summary>
        /// 2nd Constructor - FilmeModels Object
        /// </summary>
        public FilmeModels()
        {

        }

        /// <summary>
        /// To find a movie (LinQ)
        /// </summary>
        /// <param name="idFilme"></param>
        /// <returns></returns>
        public static FilmeModels GetFilme(int idFilme)
        {
            var db = new Context();
            return (from filme in db.Filmes
                    where filme.IdFilme == idFilme
                    select filme).First();
        }

        /// <summary>
        /// String convertion
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var db = new Context();

            // Included method number of movies in rent
            int qtdFilme = (
                from filme in db.LocacaoFilme
                where filme.IdFilme == IdFilme
                select filme
                ).Count();

            return $"--------------------===[ FILME ]===----------------------------------------------------------------------------------------------\n" +
                    $"--> Nº ID DO FILME: {IdFilme}\n" +
                    $"-> TÍTULO: {Titulo}\n" +
                    $"-> DATA DE LANÇAMENTO: {DataLancamento}\n" +
                    $"-> SINOPSE: {Sinopse}\n" +
                    $"-> VALOR DA LOCAÇÃO: {ValorLocacaoFilme.ToString("C")}\n" +
                    $"-> QTDE EM ESTOQUE: {EstoqueFilme}\n" +
                    $"-> QTDE DE LOCAÇÕES REALIZADAS: {qtdFilme}\n" +
                    $"---------------------------------------------------------------------------------------------------------------------------------\n";
        }

        /// <summary>
        /// Add rent
        /// </summary>
        /// <param name="locacao"></param>
        public void AtribuirLocacao(LocacaoModels locacao)
        {
            locacoes.Add(locacao);
        }

        /// <summary>
        /// Return movies list from DB
        /// </summary>
        /// <returns></returns>
        public static List<FilmeModels> GetFilmes()
        {
            var db = new Context();
            return db.Filmes.ToList();
        }
    }
}