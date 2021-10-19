using System.Data;
using Microsoft.Data.SqlClient;

SqlConnection connexion = new SqlConnection();
SqlCommand commande = new SqlCommand("SELECT * FROM TEST",
connexion);
IDataReader lecteur = null;
try
{
    connexion.Open();
    lecteur =
commande.ExecuteReader(CommandBehavior.CloseConnection);
    if (lecteur.Read())
        Console.WriteLine(lecteur.GetString(0));
}
finally
{
    if (lecteur != null && !lecteur.IsClosed)
        lecteur.Close();
}

