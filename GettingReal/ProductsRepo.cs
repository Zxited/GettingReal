﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GettingReal
{
    public class ProductsRepo : DatabaseRepo
    {
        internal bool CreateProduct(string productName, int amount, string placement)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spInsertProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReservedelsNavn", SqlDbType.NVarChar).Value = productName;
                    cmd.Parameters.Add("@Antal", SqlDbType.Int).Value = amount;
                    cmd.Parameters.Add("@Placering", SqlDbType.NVarChar).Value = placement;

                    connection.Open();

                    int countRowsAffected = cmd.ExecuteNonQuery();
                    if (countRowsAffected > 0)
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

        internal bool ProductOrdered(int productID, int orderNumber, string date)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spProductOrdered", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = productID;
                    cmd.Parameters.Add("@Ordrenummer", SqlDbType.Int).Value = orderNumber;
                    cmd.Parameters.Add("@OrdreDato", SqlDbType.NChar).Value = date;

                    connection.Open();

                    int countRowsAffected = cmd.ExecuteNonQuery();
                    if (countRowsAffected > 0)
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

        internal bool RemoveProduct(int id)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spRemoveProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                    connection.Open();

                    int countRowsAffected = cmd.ExecuteNonQuery();
                    if (countRowsAffected > 0)
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

        internal List<List<string>> GetAllPrducts()
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllProducts", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    return ListResult(cmd);
                }
            }
        }
    }
}
