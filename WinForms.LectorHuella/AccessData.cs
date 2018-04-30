using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WinForms.LectorHuella
{
    public class AccessData
    {
        private static string resultado = string.Empty;
        private static Model model;

        public static string InsertUser(Model user)
        {
            try
            {
                using (var conn = new SqlConnection(Helper.GetConn()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("RegistrarUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@action", Helper.GetEnumDescription(Accion.INSERT));
                        cmd.Parameters.AddWithValue("@nombre", user.nombre);
                        cmd.Parameters.AddWithValue("@huella", user.huella);
                        cmd.Parameters.Add("@resultado", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        resultado = cmd.Parameters["@resultado"].Value.ToString();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido una Exception :( " + ex);
            }

            return resultado;
        }

        public static List<Model> VerificarHuella(out string resultado)
        {
            var listUser = new List<Model>();
            resultado = string.Empty;

            try
            {
                using (var conn = new SqlConnection(Helper.GetConn()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("RegistrarUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@action", Helper.GetEnumDescription(Accion.SELECT_BY_ITEM));
                        cmd.Parameters.Add("@resultado", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                    model = new Model();
                                    model.id = Guid.Parse(dr["id"].ToString());
                                    model.nombre = dr["nombre"].ToString();
                                    model.huella = (byte[])dr["huella"];
                                    model.fechaAlta = Convert.ToDateTime(dr["fechaAlta"]);
                                    listUser.Add(model);
                            }
                        }
                        resultado = cmd.Parameters["@resultado"].Value.ToString();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido una Exception :( " + ex);
            }

            return listUser;
        }
    }
}