using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SAMSDatabaseLayer.InputClasses
{
   public class uspSelectChequeRegister
    {
        #region Private Members
        private string sp_Name = "uspSelectChequeRegister";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;


        private int m_distributor_id;
        private int m_Location_id;
        private int m_TYPE_ID;
        private DateTime m_DateFrom;
        private DateTime m_dateto;
        #endregion


        #region Public Properties
        public int distributor_id
        {
            set
            {
                m_distributor_id = value;
            }
            get
            {
                return m_distributor_id;
            }
        }

        public int Location_id
        {
            set
            {
                m_Location_id = value;
            }
            get
            {
                return m_Location_id;
            }
        }


      


        public int TYPE_ID
        {
            set
            {
                m_TYPE_ID = value;
            }
            get
            {
                return m_TYPE_ID;
            }
        }





        public DateTime DateFrom
        {
            set
            {
                m_DateFrom = value;
            }
            get
            {
                return m_DateFrom;
            }
        }


        public DateTime dateto
        {
            set
            {
                m_dateto = value;
            }
            get
            {
                return m_dateto;
            }
        }




        public IDbConnection Connection
        {
            set
            {
                m_connection = value;
            }
            get
            {
                return m_connection;
            }
        }
        public IDbTransaction Transaction
        {
            set
            {
                m_transaction = value;
            }
            get
            {
                return m_transaction;
            }
        }
        #endregion


        #region Constructor
        public uspSelectChequeRegister()
        {
            m_distributor_id = Constants.IntNullValue;
            m_Location_id = Constants.IntNullValue;
            m_TYPE_ID = Constants.IntNullValue;
            m_DateFrom = Constants.DateNullValue;
            m_dateto = Constants.DateNullValue;
        }
        #endregion

        #region public Methods
        public bool ExecuteQuery()
        {
            try
            {
                IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp_Name;
                cmd.Connection = m_connection;
                if (m_transaction != null)
                {
                    cmd.Transaction = m_transaction;
                }
                GetParameterCollection(ref cmd);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {


            }
        }


        public IDataReader ExecuteReader()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDataReader dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }


        public DataTable ExecuteTable()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                command.CommandTimeout = 0;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
                da.SelectCommand = command;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {


            }
        }


        public string ExecuteScalar()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                object o;
                o = command.ExecuteScalar();


                return o.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }


        public void FirstReader(IDataReader dr)
        {
            if (dr.Read())
            {
                m_distributor_id = Convert.ToInt32(dr["distributor_id"]);
                m_Location_id = Convert.ToInt32(dr["Location_id"]);
                m_TYPE_ID = Convert.ToInt32(dr["TYPE_ID"]);
                m_DateFrom = Convert.ToDateTime(dr["DateFrom"]);
                m_dateto = Convert.ToDateTime(dr["dateto"]);
            }
        }


        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@distributor_id";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_distributor_id == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_distributor_id;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Location_id";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_Location_id == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Location_id;
            }
            pparams.Add(parameter);


           


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TYPE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_TYPE_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TYPE_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DateFrom";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_DateFrom == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DateFrom;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@dateto";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_dateto == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_dateto;
            }
            pparams.Add(parameter);


        }
        #endregion
    }
}
