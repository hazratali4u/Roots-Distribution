using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
namespace SAMSDatabaseLayer.Classes
{
	public class uspPrintSALE_ORDER_PROMOTIONDC
	{
		#region Private Members
		private string sp_Name = "uspPrintSALE_ORDER_PROMOTIONDC" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_DISTRIBTOR_ID;
		private int m_TYPE_ID;
		private string m_InvoiceIDs;
		#endregion
		#region Public Properties
		public int DISTRIBTOR_ID
		{
			set
			{
				m_DISTRIBTOR_ID = value ;
			}
			get
			{
				return m_DISTRIBTOR_ID;
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
		public  string InvoiceIDs
		{
			set
			{
				m_InvoiceIDs = value ;
			}
			get
			{
				return m_InvoiceIDs;
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
		public uspPrintSALE_ORDER_PROMOTIONDC()
		{
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
				m_DISTRIBTOR_ID= Convert.ToInt32(dr["DISTRIBTOR_ID"]);
				m_TYPE_ID= Convert.ToInt32(dr["TYPE_ID"]);
				m_InvoiceIDs= Convert.ToString(dr["InvoiceIDs"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DISTRIBTOR_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DISTRIBTOR_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DISTRIBTOR_ID;
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
			parameter.ParameterName = "@InvoiceIDs" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_InvoiceIDs== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_InvoiceIDs;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
