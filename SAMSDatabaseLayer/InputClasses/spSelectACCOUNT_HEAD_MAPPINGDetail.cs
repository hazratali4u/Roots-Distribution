using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;



namespace SAMSDatabaseLayer.Classes
{
	public class spSelectACCOUNT_HEAD_MAPPINGDetail
	{
		#region Private Members
		private string sp_Name = "spSelectACCOUNT_HEAD_MAPPINGDetail" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_MAPPING_ID;
		private int m_DISTRIBUTOR_ID;
		private int m_PRINCIPAL_ID;
		private int m_ACCOUNT_HEAD_ID;
		private int m_TYPE_ID;
		private int m_USER_ID;
		private DateTime m_TIME_STAMP;
		private DateTime m_LAST_UPDATED;
		private long m_CUSTOMER_ID;
		#endregion
		#region Public Properties
		public int MAPPING_ID
		{
			set
			{
				m_MAPPING_ID = value ;
			}
			get
			{
				return m_MAPPING_ID;
			}
		}
		public int DISTRIBUTOR_ID
		{
			set
			{
				m_DISTRIBUTOR_ID = value ;
			}
			get
			{
				return m_DISTRIBUTOR_ID;
			}
		}
		public int PRINCIPAL_ID
		{
			set
			{
				m_PRINCIPAL_ID = value ;
			}
			get
			{
				return m_PRINCIPAL_ID;
			}
		}
		public int ACCOUNT_HEAD_ID
		{
			set
			{
				m_ACCOUNT_HEAD_ID = value ;
			}
			get
			{
				return m_ACCOUNT_HEAD_ID;
			}
		}
		public int TYPE_ID
		{
			set
			{
				m_TYPE_ID = value ;
			}
			get
			{
				return m_TYPE_ID;
			}
		}
		public int USER_ID
		{
			set
			{
				m_USER_ID = value ;
			}
			get
			{
				return m_USER_ID;
			}
		}
		public DateTime TIME_STAMP
		{
			set
			{
				m_TIME_STAMP = value ;
			}
			get
			{
				return m_TIME_STAMP;
			}
		}
		public DateTime LAST_UPDATED
		{
			set
			{
				m_LAST_UPDATED = value ;
			}
			get
			{
				return m_LAST_UPDATED;
			}
		}
		public long CUSTOMER_ID
		{
			set
			{
				m_CUSTOMER_ID = value ;
			}
			get
			{
				return m_CUSTOMER_ID;
			}
		}


		public IDbConnection  Connection
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
		public IDbTransaction  Transaction
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
		public spSelectACCOUNT_HEAD_MAPPINGDetail()
		{
            m_DISTRIBUTOR_ID = Constants.IntNullValue;
            m_PRINCIPAL_ID = Constants.IntNullValue;
            m_CUSTOMER_ID = Constants.LongNullValue;
            m_ACCOUNT_HEAD_ID = Constants.IntNullValue;
            m_TIME_STAMP = Constants.DateNullValue;
            m_LAST_UPDATED = Constants.DateNullValue;
            m_USER_ID = Constants.IntNullValue;
            m_MAPPING_ID = Constants.IntNullValue;
		}
		#endregion
		#region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
				cmd.CommandText = sp_Name;
				cmd.Connection =   m_connection;
				if(m_transaction!=null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch(Exception e)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
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
				if(m_transaction!=null)
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
			catch(Exception exp)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				object o;
				o = command.ExecuteScalar();


				return o.ToString();
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public void FirstReader(IDataReader dr)
		{
			if(dr.Read())
			{
				m_MAPPING_ID= Convert.ToInt32(dr["MAPPING_ID"]);
				m_DISTRIBUTOR_ID= Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
				m_PRINCIPAL_ID= Convert.ToInt32(dr["PRINCIPAL_ID"]);
				m_ACCOUNT_HEAD_ID= Convert.ToInt32(dr["ACCOUNT_HEAD_ID"]);
				m_TYPE_ID= Convert.ToInt32(dr["TYPE_ID"]);
				m_USER_ID= Convert.ToInt32(dr["USER_ID"]);
				m_TIME_STAMP= Convert.ToDateTime(dr["TIME_STAMP"]);
				m_LAST_UPDATED= Convert.ToDateTime(dr["LAST_UPDATED"]);
				m_CUSTOMER_ID=Convert.ToInt64(dr["CUSTOMER_ID"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@MAPPING_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_MAPPING_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_MAPPING_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DISTRIBUTOR_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DISTRIBUTOR_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DISTRIBUTOR_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PRINCIPAL_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_PRINCIPAL_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PRINCIPAL_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ACCOUNT_HEAD_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_ACCOUNT_HEAD_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ACCOUNT_HEAD_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TYPE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_TYPE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TYPE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@USER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_USER_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_USER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TIME_STAMP" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_TIME_STAMP==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TIME_STAMP;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@LAST_UPDATED" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_LAST_UPDATED==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_LAST_UPDATED;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@CUSTOMER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_CUSTOMER_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_CUSTOMER_ID;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
