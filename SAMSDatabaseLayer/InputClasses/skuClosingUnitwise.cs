using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SAMSDatabaseLayer.InputClasses
{
   public class skuClosingUnitwise
    {
        #region Private Members
        private string sp_Name = "skuClosingUnitwise";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_distributorId;
        private int m_supplierId;
        private string m_category_id;
        private int m_principal_Id;
        private DateTime m_date;
        private DateTime m_Fromdate;
        private int m_userId;
        private int m_typeId;
        #endregion
        #region Public Properties
        public int PRINCIPAL_ID
        {
            set
            {
                m_principal_Id = value;
            }
            get
            {
                return m_principal_Id;
            }
        }
        public int distributorId
        {
            set
            {
                m_distributorId = value;
            }
            get
            {
                return m_distributorId;
            }
        }
        public int supplierId
        {
            set
            {
                m_supplierId = value;
            }
            get
            {
                return m_supplierId;
            }
        }
        public string category_id
        {
            set
            {
                m_category_id = value;
            }
            get
            {
                return m_category_id;
            }
        }
        public DateTime date
        {
            set
            {
                m_date = value;
            }
            get
            {
                return m_date;
            }
        }
        public DateTime Fromdate
        {
            set
            {
                m_Fromdate = value;
            }
            get
            {
                return m_Fromdate;
            }
        }
        public int userId
        {
            set
            {
                m_userId = value;
            }
            get
            {
                return m_userId;
            }
        }

        public int typeId
        {
            set
            {
                m_typeId = value;
            }
            get
            {
                return m_typeId;
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
        public skuClosingUnitwise()
        {
            m_Fromdate = Constants.DateNullValue;
            m_category_id = null;
            m_principal_Id = Constants.IntNullValue;
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
                m_distributorId = Convert.ToInt32(dr["distributorId"]);
                m_supplierId = Convert.ToInt32(dr["supplierId"]);
                m_category_id = Convert.ToString(dr["category_id"]);
                m_date = Convert.ToDateTime(dr["date"]);
                m_userId = Convert.ToInt32(dr["userId"]);
                m_typeId = Convert.ToInt32(dr["typeId"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@distributorId";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_distributorId == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_distributorId;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@supplierId";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_supplierId == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_supplierId;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@categoryId";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_category_id == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_category_id;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@Fromdate";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_Fromdate == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_Fromdate;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@date";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_date == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_date;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@userId";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_userId == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_userId;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@typeId";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_typeId == Constants.ShortNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_typeId;
            }
            pparams.Add(parameter);



            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PRINCIPAL_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_principal_Id == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_principal_Id;
            }
            pparams.Add(parameter);
        }
        #endregion
    }
}
