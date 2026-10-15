using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SAMSDataAccessLayer.Classes;
using SAMSCommon.Classes;

namespace SAMSDatabaseLayer.Classes
{
    public class uspUpdateSale_Invoice_Master
    {
        #region Private Members
        private string sp_Name = "uspUpdateSale_Invoice_Master";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_DISTRIBUTOR_ID;
        private int m_DELIVERYMAN_ID;
        private int m_USER_ID;
        private decimal m_TOTAL_AMOUNT;
        private decimal m_TOTAL_NET_AMOUNT;
        private decimal m_EXTRA_DISCOUNT_AMOUNT;
        private decimal m_STANDARD_DISCOUNT_AMOUNT;
        private decimal m_GST_AMOUNT;
        private decimal m_SCHEME_AMOUNT;
        private decimal m_CURRENT_CREDIT_AMOUNT;
        private decimal m_CREDIT_AMOUNT;
        private decimal m_TST_AMOUNT;
        private decimal m_SED_AMOUNT;
        private decimal m_SPECIAL_DISCOUNT;
        private decimal m_EXPIRY_DISCOUNT;
        private long m_SALE_INVOICE_ID;
        private long m_SOLD_TO;
        private string m_MANUAL_INVOICE_ID;
        private string m_REMARKS;
        private int m_PRINCIPAL_ID;

        #endregion

        #region Public Properties


        public int DISTRIBUTOR_ID
        {
            set
            {
                m_DISTRIBUTOR_ID = value;
            }
            get
            {
                return m_DISTRIBUTOR_ID;
            }
        }
        public int DELIVERYMAN_ID
        {
            set
            {
                m_DELIVERYMAN_ID = value;
            }
            get
            {
                return m_DELIVERYMAN_ID;
            }
        }
        public int PRINCIPAL_ID
        {
            set
            {
                m_PRINCIPAL_ID = value;
            }
            get
            {
                return m_PRINCIPAL_ID;
            }
        }
        public int USER_ID
        {
            set
            {
                m_USER_ID = value;
            }
            get
            {
                return m_USER_ID;
            }
        }
        public decimal TOTAL_AMOUNT
        {
            set
            {
                m_TOTAL_AMOUNT = value;
            }
            get
            {
                return m_TOTAL_AMOUNT;
            }
        }
        public decimal TOTAL_NET_AMOUNT
        {
            set
            {
                m_TOTAL_NET_AMOUNT = value;
            }
            get
            {
                return m_TOTAL_NET_AMOUNT;
            }
        }
        public decimal EXTRA_DISCOUNT_AMOUNT
        {
            set
            {
                m_EXTRA_DISCOUNT_AMOUNT = value;
            }
            get
            {
                return m_EXTRA_DISCOUNT_AMOUNT;
            }
        }
        public decimal STANDARD_DISCOUNT_AMOUNT
        {
            set
            {
                m_STANDARD_DISCOUNT_AMOUNT = value;
            }
            get
            {
                return m_STANDARD_DISCOUNT_AMOUNT;
            }
        }
        public decimal GST_AMOUNT
        {
            set
            {
                m_GST_AMOUNT = value;
            }
            get
            {
                return m_GST_AMOUNT;
            }
        }
        public decimal SCHEME_AMOUNT
        {
            set
            {
                m_SCHEME_AMOUNT = value;
            }
            get
            {
                return m_SCHEME_AMOUNT;
            }
        }
        public decimal CURRENT_CREDIT_AMOUNT
        {
            set
            {
                m_CURRENT_CREDIT_AMOUNT = value;
            }
            get
            {
                return m_CURRENT_CREDIT_AMOUNT;
            }
        }
        public decimal CREDIT_AMOUNT
        {
            set
            {
                m_CREDIT_AMOUNT = value;
            }
            get
            {
                return m_CREDIT_AMOUNT;
            }
        }
        public decimal TST_AMOUNT
        {
            set
            {
                m_TST_AMOUNT = value;
            }
            get
            {
                return m_TST_AMOUNT;
            }
        }
        public decimal SED_AMOUNT
        {
            set
            {
                m_SED_AMOUNT = value;
            }
            get
            {
                return m_SED_AMOUNT;
            }
        }
        public decimal SPECIAL_DISCOUNT
        {
            set { m_SPECIAL_DISCOUNT = value; }
            get { return m_SPECIAL_DISCOUNT; }
        }
        public decimal EXPIRY_DISCOUNT
        {
            set { m_EXPIRY_DISCOUNT = value; }
            get { return m_EXPIRY_DISCOUNT; }
        }
        public long SALE_INVOICE_ID
        {
            set
            {
                m_SALE_INVOICE_ID = value;
            }
            get
            {
                return m_SALE_INVOICE_ID;
            }
        }
        public long SOLD_TO
        {
            set
            {
                m_SOLD_TO = value;
            }
            get
            {
                return m_SOLD_TO;
            }
        }
        public string MANUAL_INVOICE_ID
        {
            set
            {
                m_MANUAL_INVOICE_ID = value;
            }
            get
            {
                return m_MANUAL_INVOICE_ID;
            }
        }

        public string REMARKS
        {
            set
            {
                m_REMARKS = value;
            }
            get
            {
                return m_REMARKS;
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
        public uspUpdateSale_Invoice_Master()
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
                m_DISTRIBUTOR_ID = Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
                m_DELIVERYMAN_ID = Convert.ToInt32(dr["DELIVERYMAN_ID"]);
                m_USER_ID = Convert.ToInt32(dr["USER_ID"]);
                m_TOTAL_AMOUNT = Convert.ToDecimal(dr["TOTAL_AMOUNT"]);
                m_TOTAL_NET_AMOUNT = Convert.ToDecimal(dr["TOTAL_NET_AMOUNT"]);
                m_EXTRA_DISCOUNT_AMOUNT = Convert.ToDecimal(dr["EXTRA_DISCOUNT_AMOUNT"]);
                m_STANDARD_DISCOUNT_AMOUNT = Convert.ToDecimal(dr["STANDARD_DISCOUNT_AMOUNT"]);
                m_GST_AMOUNT = Convert.ToDecimal(dr["GST_AMOUNT"]);
                m_SCHEME_AMOUNT = Convert.ToDecimal(dr["SCHEME_AMOUNT"]);
                m_CURRENT_CREDIT_AMOUNT = Convert.ToDecimal(dr["CURRENT_CREDIT_AMOUNT"]);
                m_CREDIT_AMOUNT = Convert.ToDecimal(dr["CREDIT_AMOUNT"]);
                m_TST_AMOUNT = Convert.ToDecimal(dr["TST_AMOUNT"]);
                m_SED_AMOUNT = Convert.ToDecimal(dr["SED_AMOUNT"]);
                m_SALE_INVOICE_ID = Convert.ToInt64(dr["SALE_INVOICE_ID"]);
                m_SOLD_TO = Convert.ToInt64(dr["SOLD_TO"]);
                m_MANUAL_INVOICE_ID = Convert.ToString(dr["MANUAL_INVOICE_ID"]);
                m_REMARKS = Convert.ToString(dr["REMARKS"]);
                m_PRINCIPAL_ID = Convert.ToInt32(dr["PRINCIPAL_ID"]);

            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DISTRIBUTOR_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DISTRIBUTOR_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DISTRIBUTOR_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DELIVERYMAN_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DELIVERYMAN_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DELIVERYMAN_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@USER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_USER_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_USER_ID;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PRINCIPAL_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PRINCIPAL_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PRINCIPAL_ID;
            }
            pparams.Add(parameter);
            




            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TOTAL_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_TOTAL_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TOTAL_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TOTAL_NET_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_TOTAL_NET_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TOTAL_NET_AMOUNT;
            }
            pparams.Add(parameter);




            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EXTRA_DISCOUNT_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_EXTRA_DISCOUNT_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EXTRA_DISCOUNT_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@STANDARD_DISCOUNT_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_STANDARD_DISCOUNT_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_STANDARD_DISCOUNT_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@GST_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_GST_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_GST_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SCHEME_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_SCHEME_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SCHEME_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CURRENT_CREDIT_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_CURRENT_CREDIT_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CURRENT_CREDIT_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CREDIT_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_CREDIT_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CREDIT_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TST_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_TST_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TST_AMOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SED_AMOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_SED_AMOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SED_AMOUNT;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SPECIAL_DISCOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_SPECIAL_DISCOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SPECIAL_DISCOUNT;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EXPIRY_DISCOUNT";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Money);
            if (m_EXPIRY_DISCOUNT == Constants.DecimalNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EXPIRY_DISCOUNT;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SALE_INVOICE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_SALE_INVOICE_ID == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SALE_INVOICE_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SOLD_TO";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_SOLD_TO == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SOLD_TO;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@MANUAL_INVOICE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_MANUAL_INVOICE_ID == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_MANUAL_INVOICE_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@REMARKS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.NVarChar);
            if (m_REMARKS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_REMARKS;
            }
            pparams.Add(parameter);
            


        }
        #endregion
    }
}