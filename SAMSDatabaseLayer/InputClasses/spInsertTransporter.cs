using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;


namespace SAMSDatabaseLayer.Classes
{
    public class spInsertTransporter
    {
        #region Private Members
        private string sp_Name = "spInsertTransporter";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_TRASNPOTER_ID;
        private int m_TRASNPOTER_ID2;
        private string m_NAME;
        private string m_ADDRESS;
        private string m_PHONE;
        private string m_EMAIL;
        private byte m_IS_ACTIVE;
        #endregion
        #region Public Properties
        public int TRASNPOTER_ID
        {
            set
            {
                m_TRASNPOTER_ID = value;
            }
            get
            {
                return m_TRASNPOTER_ID;
            }
        }
        public int TRASNPOTER_ID2
        {
            get
            {
                return m_TRASNPOTER_ID2;
            }
        }
        public string NAME
        {
            set
            {
                m_NAME = value;
            }
            get
            {
                return m_NAME;
            }
        }
        public string ADDRESS
        {
            set
            {
                m_ADDRESS = value;
            }
            get
            {
                return m_ADDRESS;
            }
        }
        public byte IS_ACTIVE
        {
            set
            {
                m_IS_ACTIVE = value;
            }
            get
            {
                return m_IS_ACTIVE;
            }
        }
        public string PHONE
        {
            set
            {
                m_PHONE = value;
            }
            get
            {
                return m_PHONE;
            }
        }
        public string EMAIL
        {
            set
            {
                m_EMAIL = value;
            }
            get
            {
                return m_EMAIL;
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
        public spInsertTransporter()
        {
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
                m_TRASNPOTER_ID2 = (int)((IDataParameter)(cmd.Parameters["@TRASNPOTER_ID2"])).Value;
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
                m_TRASNPOTER_ID = Convert.ToInt32(dr["TRASNPOTER_ID"]);
                m_TRASNPOTER_ID2 = Convert.ToInt32(dr["TRASNPOTER_ID2"]);
                m_NAME = Convert.ToString(dr["NAME"]);
                m_ADDRESS = Convert.ToString(dr["ADDRESS"]);
                m_PHONE = Convert.ToString(dr["PHONE"]);
                m_EMAIL = Convert.ToString(dr["EMAIL"]);
                m_IS_ACTIVE = Convert.ToByte(dr["IS_ACTIVE"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TRASNPOTER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_TRASNPOTER_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TRASNPOTER_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TRASNPOTER_ID2";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            parameter.Direction = ParameterDirection.Output;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@NAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_NAME == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_NAME;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_ACTIVE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_ACTIVE;
            pparams.Add(parameter);
            


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@ADDRESS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_ADDRESS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_ADDRESS;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PHONE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_PHONE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PHONE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EMAIL";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EMAIL == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EMAIL;
            }
            pparams.Add(parameter);


        }
        #endregion
    }
}
