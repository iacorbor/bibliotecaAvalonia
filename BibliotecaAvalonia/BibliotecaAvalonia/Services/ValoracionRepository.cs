using System.Collections.Generic;
using BibliotecaAvalonia.Models;
using Microsoft.Data.Sqlite;

namespace BibliotecaAvalonia.Services
{
    public class ValoracionRepository
    {
        public List<Valoracion> ObtenerPorArticulo(int articuloId)
        {
            var lista = new List<Valoracion>();

            using var connection = DatabaseService.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                "SELECT * FROM Valoraciones WHERE ArticuloId=@id";

            command.Parameters.AddWithValue("@id", articuloId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Valoracion
                {
                    id = reader.GetInt32(0),
                    articuloId = reader.GetInt32(1),
                    puntuacion = reader.GetDouble(2),
                    comentario = reader.GetString(3),
                    usuarioId = reader.GetInt32(4)
                });
            }

            return lista;
        }

        public void Insertar(Valoracion v)
        {
            using var connection = DatabaseService.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText =
                @"INSERT INTO Valoraciones
            (ArticuloId,Puntuacion,Comentario,Usuario)
            VALUES (@articulo,@puntuacion,@comentario,@usuario)";

            command.Parameters.AddWithValue("@articulo", v.articuloId);
            command.Parameters.AddWithValue("@puntuacion", v.puntuacion);
            command.Parameters.AddWithValue("@comentario", v.comentario);
            command.Parameters.AddWithValue("@usuario", v.usuarioId);

            command.ExecuteNonQuery();
        }
        public void EliminarPorArticulo(int articuloId)
        {
            using var connection = DatabaseService.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Valoraciones WHERE ArticuloId = @articuloId";
            command.Parameters.AddWithValue("@articuloId", articuloId);

            command.ExecuteNonQuery();
        }
    }
}