using System.Data.SqlClient;

namespace Reportes.DAO
{
    internal static class MetodosExtension
    {

        /// <summary>
        /// Obienen el valor de una columna de un datareader por tipo de dato
        /// </summary>
        /// <typeparam name="T">Generico que representa el tipo de dato</typeparam>
        /// <param name="reader"></param>
        /// <param name="nombreColumna"></param>
        /// <returns></returns>
        public static T ObtenerValorColumnaPorTipo<T>(this SqlDataReader reader, string nombreColumna)
        {
            try
            {
                var indexColumna = reader.GetOrdinal(nombreColumna);
                return reader.IsDBNull(indexColumna) ? default(T) : reader.GetFieldValue<T>(indexColumna);
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }
    }
}
