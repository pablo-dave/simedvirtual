﻿using Npgsql;
using SIMEDVirtual.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SIMEDVirtual.DA
{
    class Empresa
    {
        //inserta datos en la tabla persona
        public static Boolean InsertaEmpresa(
            string nombre, string cedula_juridica, string direccion, string descripcion)
        {
            int x = 0;
            string g = "";
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = conn;
                conn.Open();
                try
                {
                    command.CommandText =
                        "insert into empresa(nombre,cedula_juridica,direccion,descripcion) " +
                    "values (@nombre,@cedula,@direccion,@descripcion)";

                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@cedula", cedula_juridica);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@descripcion", descripcion);

                    x = command.ExecuteNonQuery();
                }

                catch (Exception exp)
                {
                    return false;
                }
                conn.Close();

                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        //inserta datos en la tabla persona
        public static Boolean InsertaEmpresaTelefono(string cedula_juridica,
            int telefono, string encargado)
        {
            int x = 0;
            string g = "";
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = conn;
                conn.Open();
                try
                {
                    command.CommandText =
                        "insert into empresa_telefono(cedula_juridica,telefono,encargado) " +
                    "values (@cedula,@telefono,@encargado)";

                    command.Parameters.AddWithValue("@cedula", cedula_juridica);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@encargado", encargado);

                    x = command.ExecuteNonQuery();
                }

                catch (Exception exp)
                {
                    return false;

                }
                conn.Close();

                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        //metodo que carga el nombre en el combo
        public static List<EmpresaEntity> getAllEmpresas()
        {
            List<EmpresaEntity> empresas = new List<EmpresaEntity>();
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from empresa order by nombre",
    conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmpresaEntity entidad = new EmpresaEntity();

                    //ocupo que me devuelta TODOS los datos para la fk 
                    entidad.id = Convert.ToInt32(dr[0]);
                    entidad.nombre = dr[1].ToString();
                    entidad.cedula = Convert.ToString(dr[2]);
                    entidad.direccion = dr[3].ToString();
                    entidad.descripcion = dr[4].ToString();

                    empresas.Add(entidad);
                }
                conn.Close();
            }
            return empresas;
        }


        //toda menos la empresa ninguna
        public static List<EmpresaEntity> getEmpresasMenos()
        {
            List<EmpresaEntity> empresas = new List<EmpresaEntity>();
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from empresa where id!=4 order by nombre",
    conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmpresaEntity entidad = new EmpresaEntity();

                    //ocupo que me devuelta TODOS los datos para la fk 
                    entidad.id = Convert.ToInt32(dr[0]);
                    entidad.nombre = dr[1].ToString();
                    entidad.cedula = Convert.ToString(dr[2]);
                    entidad.direccion = dr[3].ToString();
                    entidad.descripcion = dr[4].ToString();

                    empresas.Add(entidad);
                }
                conn.Close();
            }
            return empresas;
        }

        public static EmpresaEntity getEmpresaByID(String cedula)
        {
            EmpresaEntity entidad = new EmpresaEntity();
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from empresa where id='" + cedula + "'", conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {                    
                    entidad.nombre = Convert.ToString(dr[1].ToString());
                    entidad.cedula = Convert.ToString(dr[2].ToString());
                    entidad.direccion = Convert.ToString(dr[3].ToString());
                    entidad.descripcion = Convert.ToString(dr[4].ToString());
                }
                conn.Close();
            }
            return entidad;
        }
        public static EmpresaEntity getEmpresaByCedula(String cedula)
        {
            EmpresaEntity entidad = new EmpresaEntity();
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from empresa where cedula_juridica='" + cedula + "'", conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    entidad.nombre = Convert.ToString(dr[1].ToString());
                    entidad.cedula = Convert.ToString(dr[2].ToString());
                    entidad.direccion = Convert.ToString(dr[3].ToString());
                    entidad.descripcion = Convert.ToString(dr[4].ToString());
                }
                conn.Close();
            }
            return entidad;
        }

        //selecciona el id de la empresa
        public static List<EmpresaEntity> getTelefono(String cedula_juridica)
        {
            List<EmpresaEntity> empresa_telefono = new List<EmpresaEntity>();
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from empresa_telefono where cedula_juridica='" + cedula_juridica + "'", conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EmpresaEntity entidad = new EmpresaEntity();
                    entidad.telefono1 = Convert.ToInt32(dr[2].ToString());
                    entidad.encargado1 = Convert.ToString(dr[3].ToString());
                    empresa_telefono.Add(entidad);

                }
                conn.Close();
            }
            return empresa_telefono;
        }

        //edita el telefono segun la cedula de la empresa
        public static Boolean updateTelefono(String cedula_juridica, int telefono, string encargado)
        {
            int x = 0;
            string g = "";
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = conn;
                conn.Open();
                try
                {
                    command.CommandText =
                        "update empresa_telefono set telefono=@telefono, encargado=@encargado where cedula_juridica=@cedula and telefono=@telefono;";

                    command.Parameters.AddWithValue("@cedula", cedula_juridica);
                    command.Parameters.AddWithValue("@telefono", telefono);
                    command.Parameters.AddWithValue("@encargado", encargado);

                    x = command.ExecuteNonQuery();
                }

                catch (Exception)
                {
                    return false;
                }
                conn.Close();

                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        //update de empresa
        public static Boolean updateEmpresa(String cedula_juridica, string nombre, string direccion, string descripcion)
        {
            int x = 0;
            string g = "";
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = conn;
                conn.Open();
                try
                {
                    command.CommandText =
                        "update empresa set nombre=@nombre, direccion=@direccion, descripcion=@descripcion where cedula_juridica=@cedula;";

                    command.Parameters.AddWithValue("@cedula", cedula_juridica);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@direccion", direccion);
                    command.Parameters.AddWithValue("@descripcion", descripcion);

                    x = command.ExecuteNonQuery();
                }

                catch (Exception exp)
                {
                    return false;
                }
                conn.Close();

                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        //eliminar el telefono y el encargado segun cedula
        public static Boolean deleteTelefono(String cedula_juridica, int telefono)
        {
            int x = 0;
            string g = "";
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = conn;
                conn.Open();
                try
                {
                    command.CommandText =
                        "delete from empresa_telefono where cedula_juridica=@cedula and telefono=@telefono;";

                    command.Parameters.AddWithValue("@cedula", cedula_juridica);
                    command.Parameters.AddWithValue("@telefono", telefono);

                    x = command.ExecuteNonQuery();
                }

                catch (Exception exp)
                {
                    return false;
                }
                conn.Close();

                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //delete all telefonos
        public static Boolean deleteAllTelefono(String cedula_juridica)
        {
            int x = 0;
            string g = "";
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = conn;
                conn.Open();
                try
                {
                    command.CommandText =
                        "delete from empresa_telefono where cedula_juridica=@cedula;";

                    command.Parameters.AddWithValue("@cedula", cedula_juridica);

                    x = command.ExecuteNonQuery();
                }

                catch (Exception exp)
                {
                    return false;
                }
                conn.Close();

                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        //delete empresa
        public static Boolean deleteEmpresa(String cedula_juridica)
        {
            int x = 0;
            string g = "";
            NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString());
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = conn;
                conn.Open();
                try
                {
                    deleteAllTelefono(cedula_juridica);
                    command.CommandText =
                        "delete from empresa where cedula_juridica=@cedula;";

                    command.Parameters.AddWithValue("@cedula", cedula_juridica);

                    x = command.ExecuteNonQuery();
                }

                catch (Exception exp)
                {
                    return false;
                }
                conn.Close();

                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
