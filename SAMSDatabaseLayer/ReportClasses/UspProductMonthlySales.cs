using System;
using System.Data;
using SAMSDataAccessLayer.Classes;
using SAMSCommon.Classes;


namespace SAMSDatabaseLayer.Classes
{
	public class UspProductMonthlySales
	{
		#region Private Members
		private string sp_Name = "UspProductMonthlySales" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_Distributor_id;
		private int m_Principal_id;
		private int m_SKU_ID;
		private int m_Type_Id;
        private int m_NoOfMonths;
		private DateTime m_Year;
		#endregion
		#region Public Properties
		public int Distributor_id
		{
			set
			{
				m_Distributor_id = value ;
			}
			get
			{
				return m_Distributor_id;
			}
		}
		public int Principal_id
		{
			set
			{
				m_Principal_id = value ;
			}
			get
			{
				return m_Principal_id;
			}
		}
		public int SKU_ID
		{
			set
			{
				m_SKU_ID = value ;
			}
			get
			{
				return m_SKU_ID;
			}
		}
		public int Type_Id
		{
			set
			{
				m_Type_Id = value ;
			}
			get
			{
				return m_Type_Id;
			}
		}
        public int NoOfMonths
        {
            set { m_NoOfMonths = value; }
            get { return m_NoOfMonths; }
        }
		public DateTime Year
		{
			set
			{
				m_Year = value ;
			}
			get
			{
				return m_Year;
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
		public UspProductMonthlySales()
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
                m_Distributor_id = Convert.ToInt32(dr["distributorId"]);
                m_Principal_id = Convert.ToInt32(dr["principalId"]);
                m_SKU_ID = Convert.ToInt32(dr["skuId"]);
                m_Type_Id = Convert.ToInt32(dr["typeId"]);
				m_Year= Convert.ToDateTime(dr["year"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@distributorId" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_Distributor_id==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Distributor_id;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@principalId"; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_Principal_id==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Principal_id;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@skuId"; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_SKU_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_SKU_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@typeId"; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_Type_Id==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Type_Id;
			}
			pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@NoOfMonths";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_NoOfMonths == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_NoOfMonths;
            }
            pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@year"; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
			if(m_Year==Constants.DateNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_Year;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}