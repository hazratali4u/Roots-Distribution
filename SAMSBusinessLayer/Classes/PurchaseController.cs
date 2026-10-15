using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using SAMSDatabaseLayer.InputClasses;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Purchase, TranferOut, Purchase Return, TranferIn And Damage Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// <term>
    /// Update Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </term>
    /// <item>
    /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class PurchaseController
    {
        #region Constructor

        /// <summary>
        /// Constructor for PurchaseController
        /// </summary>
        public PurchaseController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

        #region Private Variables
        
        IDbTransaction mTransaction;
        IDbConnection mConnection;

        #endregion

        #region Public Methods

        #region Select

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="PConnection">Connection</param>
        /// <param name="PTransaction">Transaction</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPrivousePurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, IDbConnection PConnection, IDbTransaction PTransaction)
        {
            try
            {
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = PConnection;
                mPurchaseDetail.Transaction = PTransaction;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage  Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = p_User_Id;
                mPurchaseMaster.POSTING = p_Posting;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="P_DocumentDate">Date</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, DateTime P_DocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = P_DocumentDate;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting, int p_SOLD_TO)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = Constants.IntNullValue;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        
        #endregion

        #region Insert, Update

        /// <summary>
        /// Inserts Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertStockDocument(int p_DISTRIBUTOR_ID,string p_ORDER_NUMBER,int p_TYPE_ID,DateTime p_DOCUMENT_DATE,int p_SOLD_TO,int p_SOLD_FROM,decimal p_TOTAL_AMOUNT,bool p_IS_DELETE,DataTable dtPurchaseDetail,int p_Posting,string p_BuiltyNo,int p_UserId,int p_PrincipalId, string p_REMARKS) 
		{
			try
			{
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;  
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;   
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;
                
                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());

                    mPurchaseDetail.QUANTITY_CTN = int.Parse(dr["QUANTITY_CTN"].ToString());

                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;  
                    mPurchaseDetail.ExecuteQuery();
                    
                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId; 
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;   
                    mStockUpdate.ExecuteQuery();   
                }


             



                mTransaction.Commit();
                return true; 
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();  
				return false;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
			
		}

        public bool InsertStockDocumentGL(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, int p_Account_Head_ID, string p_Account_Head_Name, out string p_Voucher_No, string p_REMARKS)
        {
            try
            {
                p_Voucher_No = "";
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());

                    mPurchaseDetail.QUANTITY_CTN = int.Parse(dr["QUANTITY_CTN"].ToString());

                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                }

                #region Mapped Acount Selection
                CustomerDataController cd = new CustomerDataController();
                string CodeFileName = "";
                DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);

                if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
                {
                    CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
                    Configuration.GetAppMapping(CodeFileName);
                }

                //string asd =  Configuration.PurchaseAccount;
                long p_PrincipalAccount_ID = 88;//2002080001	Payable to (Principal Wise)
                string p_PrincipalAccount_Name = "";
                if (mPurchaseMaster.TYPE_ID == 2 || mPurchaseMaster.TYPE_ID == 3)// purchase and purchase return
                {
                    //DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, mPurchaseMaster.PRINCIPAL_ID, Constants.LongNullValue, Constants.IntNullValue);
                    //if (dtAccount != null && dtAccount.Rows.Count > 0)
                    //{
                    //    p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                    //    p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    //}
                    p_PrincipalAccount_ID = p_Account_Head_ID;
                    p_PrincipalAccount_Name = p_Account_Head_Name;

                }
                else if (mPurchaseMaster.TYPE_ID == 4 || mPurchaseMaster.TYPE_ID == 5)// transfer in ,out
                {
                    p_PrincipalAccount_ID = 844;// 3002010002 Stock Movements
                    p_PrincipalAccount_Name = "Stock Movements";
                }
                else if (mPurchaseMaster.TYPE_ID == 6)//Damage
                {
                    p_PrincipalAccount_ID = 843;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damage and Expiries Expense";
                }
                else if (mPurchaseMaster.TYPE_ID == 10)//Damage claimable
                {
                    p_PrincipalAccount_ID = 845;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damage & Expiry - Claimable";
                }
                else if (mPurchaseMaster.TYPE_ID == 8)// Short
                {
                    p_PrincipalAccount_ID = 898;//3002020076 Warehouse Incharge
                    p_PrincipalAccount_Name = "Warehouse Incharge";
                }
                else
                {
                    DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, mPurchaseMaster.PRINCIPAL_ID, Constants.LongNullValue, Constants.IntNullValue);
                    if (dtAccount != null && dtAccount.Rows.Count > 0)
                    {
                        p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                        p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    }
                }
                #endregion

                #region GL Voucher
                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                string VoucherNo = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, DateTime.Now, mTransaction, mConnection);
                p_Voucher_No = VoucherNo;
                if (mPurchaseMaster.TYPE_ID == 2)// Purchase
                {
                    string Remarks = "Default Purchase Voucher / Document # " + p_ORDER_NUMBER;
                    string Remarks2 = "Default Purchase Voucher";
                    string DocumentType = "Purchase";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, 0, mPurchaseMaster.TOTAL_AMOUNT, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, 0, mPurchaseMaster.TOTAL_AMOUNT, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }

                    }

                }
                else if (mPurchaseMaster.TYPE_ID == 3)// Purchase Return
                {
                    string Remarks = "Default Purchase Return Voucher / Document # " + p_ORDER_NUMBER;
                    string Remarks2 = "Default Purchase Return Voucher";
                    string DocumentType = "Purchase Return";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 4)// Transfer IN
                {
                    string Remarks = "Default Transfer In Voucher";
                    string DocumentType = "Transfer IN";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {

                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock in Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Way" + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 5)// Transfer OUT
                {
                    string Remarks = "Default Transfer Out Voucher";
                    string DocumentType = "Transfer Out";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Way " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 6)// Damage
                {
                    string Remarks = "Default Stock Damage Voucher / Document # " + mPurchaseMaster.PURCHASE_MASTER_ID;
                    string Remarks2 = "Default Stock Damage Voucher";
                    string DocumentType = "Stock Damage";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 843, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 7) // Opening Stock
                {
                    string Remarks = "Default Opening Stock Voucher";
                    string DocumentType = "Opening Stock";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.FaheemQureshiCapital), 0, mPurchaseMaster.TOTAL_AMOUNT, "Mr. Faheem Qureshi Capital A/C " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 10)// Damage claimable
                {

                    string Remarks = "Default Stock Damage(Claimable) Voucher / Document # " + mPurchaseMaster.PURCHASE_MASTER_ID;
                    string Remarks2 = "Default Stock Damage(Claimable) Voucher";
                    string DocumentType = "Stock Damage(Claimable)";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 845, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 8)// Short
                {
                    string Remarks = "Default Stock Short Voucher / Document # " + mPurchaseMaster.PURCHASE_MASTER_ID;
                    string Remarks2 = "Default Stock Short Voucher";
                    string DocumentType = "Stock Short";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), mPurchaseMaster.TOTAL_AMOUNT, 0, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 901, 0, mPurchaseMaster.TOTAL_AMOUNT, "Sales (All Principle) " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 898, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }

                    }
                }

                else if (mPurchaseMaster.TYPE_ID == 9)// Excess
                {
                    string Remarks = "Default Stock Excess Voucher / Document # " + mPurchaseMaster.PURCHASE_MASTER_ID;
                    string Remarks2 = "Default Stock Excess Voucher";
                    string DocumentType = "Stock Excess";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), 0, mPurchaseMaster.TOTAL_AMOUNT, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 900, 0, mPurchaseMaster.TOTAL_AMOUNT, "Unadjusted Credit Balance - Purchase " + Remarks2, mTransaction, mConnection);
                        }
                        
                    }
                }


                #endregion
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                p_Voucher_No = "";
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool InsertStockDocumentGLForPurchase(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, int p_Account_Head_ID, string p_Account_Head_Name, out string p_Voucher_No, string p_REMARKS, string p_Principal_Name, int p_OverHead_ID, decimal p_TotalOHAmount, decimal p_TotalWeight,int p_ClientType)
            
        {
            try
            {
                p_Voucher_No = "";
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT + p_TotalOHAmount;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.EXPENSE_AMOUNT = p_TotalOHAmount;
                mPurchaseMaster.EXPENSE_HEAD_ID = p_OverHead_ID;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAILWithSKU_PRICE mPurchaseDetail = new spInsertPURCHASE_DETAILWithSKU_PRICE();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.QUANTITY_CTN = int.Parse(dr["QUANTITY_CTN"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TOTAL_WEIGHT = decimal.Parse(dr["TOTAL_SKU_WEIGHT"].ToString());
                    decimal EXPENSE_AMOUNT;
                    if (p_TotalWeight == 0)
                    {
                        EXPENSE_AMOUNT = 0;
                    }
                    else
                    {
                        EXPENSE_AMOUNT = (p_TotalOHAmount / p_TotalWeight) * decimal.Parse(dr["SKU_WEIGHT"].ToString());
                    }
                    mPurchaseDetail.EXPENSE_AMOUNT = EXPENSE_AMOUNT;
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                }

                #region Mapped Acount Selection
                CustomerDataController cd = new CustomerDataController();
                string CodeFileName = "";
                DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);

                if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
                {
                    CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
                    Configuration.GetAppMapping(CodeFileName);
                }


                long p_PrincipalAccount_ID = 88;//2002080001	Payable to (Principal Wise)
                string p_PrincipalAccount_Name = "";
                if (mPurchaseMaster.TYPE_ID == 2 || mPurchaseMaster.TYPE_ID == 3)// purchase and purchase return
                {
                    //DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, mPurchaseMaster.PRINCIPAL_ID, Constants.LongNullValue, Constants.IntNullValue);
                    //if (dtAccount != null && dtAccount.Rows.Count > 0)
                    //{
                    //    p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                    //    p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    //}
                    p_PrincipalAccount_ID = p_Account_Head_ID;
                    p_PrincipalAccount_Name = p_Account_Head_Name;

                }
                else if (mPurchaseMaster.TYPE_ID == 4 || mPurchaseMaster.TYPE_ID == 5)// transfer in ,out
                {
                    p_PrincipalAccount_ID = 844;// 3002010002 Stock Movements
                    p_PrincipalAccount_Name = "Stock Movements";
                }
                else if (mPurchaseMaster.TYPE_ID == 6)//Damage
                {
                    p_PrincipalAccount_ID = 843;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damage and Expiries Expense";
                }
                else if (mPurchaseMaster.TYPE_ID == 10)//Damage claimable
                {
                    p_PrincipalAccount_ID = 845;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damage & Expiry - Claimable";
                }
                else if (mPurchaseMaster.TYPE_ID == 8)// Short
                {
                    p_PrincipalAccount_ID = 898;//3002020076 Warehouse Incharge
                    p_PrincipalAccount_Name = "Warehouse Incharge";
                }
                else
                {
                    DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, mPurchaseMaster.PRINCIPAL_ID, Constants.LongNullValue, Constants.IntNullValue);
                    if (dtAccount != null && dtAccount.Rows.Count > 0)
                    {
                        p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                        p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    }
                }
                #endregion

                #region GL Voucher
                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                string VoucherNo = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, DateTime.Now, mTransaction, mConnection);
                p_Voucher_No = VoucherNo;
                if (mPurchaseMaster.TYPE_ID == 2)// Purchase
                {
                    string ManualRemarks = "";
                    if (p_REMARKS.Equals(""))
                    {
                        ManualRemarks =  p_REMARKS;
                    }
                    else
                    {
                        ManualRemarks = " / " + p_REMARKS;
                    }
                    string Remarks = "Default Purchase Voucher / Document # " + p_ORDER_NUMBER + " / " + p_Principal_Name + " / INV #" + Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID) + ManualRemarks;
                    string Remarks2 = "Default Purchase Voucher";
                    string DocumentType = "Purchase";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml") 
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, 0, p_TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_OverHead_ID, 0, p_TotalOHAmount, Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots & Other
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                        }
                    }

                }
                else if (mPurchaseMaster.TYPE_ID == 3)// Purchase Return
                {
                    string ManualRemarks = "";
                    if (p_REMARKS.Equals(""))
                    {
                        ManualRemarks = p_REMARKS;
                    }
                    else
                    {
                        ManualRemarks = " / " + p_REMARKS;
                    }
                    string Remarks = "Default Purchase Return Voucher / Document # " + p_ORDER_NUMBER + " / " + p_Principal_Name + " / INV #" + Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID) + ManualRemarks;
                    string Remarks2 = "Default Purchase Return Voucher";
                    string DocumentType = "Purchase Return";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            //for Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                        }
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 4)// Transfer IN
                {
                    string Remarks = "Default Transfer In Voucher";
                    string DocumentType = "Transfer IN";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {

                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock in Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Way" + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 5)// Transfer OUT
                {
                    string Remarks = "Default Transfer Out Voucher";
                    string DocumentType = "Transfer Out";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Way "  + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }
                        
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 6)// Damage
                {
                    string Remarks = "Default Stock Damage Voucher";
                    string DocumentType = "Stock Damage";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0,"Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 843, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }
                            
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 10)// Damage claimable
                {
                    string Remarks = "Default Stock Damage(Claimable) Voucher";
                    string DocumentType = "Stock Damage(Claimable)";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 845, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }
                        
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 8)// Short
                {
                    string Remarks = "Default Stock Short Voucher";
                    string DocumentType = "Stock Short";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), mPurchaseMaster.TOTAL_AMOUNT, 0, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 901, 0, mPurchaseMaster.TOTAL_AMOUNT, "Sales (All Principle) " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 898, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }
                        
                    }
                }

                else if (mPurchaseMaster.TYPE_ID == 9)// Excess
                {
                    string Remarks = "Default Stock Excess Voucher";
                    string DocumentType = "Stock Excess";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock),0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), 0, mPurchaseMaster.TOTAL_AMOUNT, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 900, 0, mPurchaseMaster.TOTAL_AMOUNT, "Unadjusted Credit Balance - Purchase " + Remarks, mTransaction, mConnection);
                        }
                        
                    }
                }


                #endregion
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                p_Voucher_No = "";
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool InsertStockDocumentGLForPurchaseForQureshi(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, int p_Account_Head_ID, string p_Account_Head_Name, out string p_Voucher_No, string p_REMARKS, string p_Principal_Name, int p_OverHead_ID, decimal p_TotalOHAmount, decimal p_TotalWeight, int p_ClientType)

        {

            CustomerDataController cd = new CustomerDataController();
            string CodeFileName = "";
            DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);

            if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
            {
                CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
                Configuration.GetAppMapping(CodeFileName);
            }
            try
            {
                p_Voucher_No = "";
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT + p_TotalOHAmount;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.EXPENSE_AMOUNT = p_TotalOHAmount;
                mPurchaseMaster.EXPENSE_HEAD_ID = p_OverHead_ID;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.QUANTITY_CTN = int.Parse(dr["QUANTITY_CTN"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TOTAL_WEIGHT = p_TYPE_ID == 4 ? 0 : decimal.Parse(dr["TOTAL_SKU_WEIGHT"].ToString());
                    decimal EXPENSE_AMOUNT;
                    if (p_TotalWeight == 0)
                    {
                        EXPENSE_AMOUNT = 0;
                    }
                    else
                    {
                        EXPENSE_AMOUNT = (p_TotalOHAmount / p_TotalWeight) * decimal.Parse(dr["SKU_WEIGHT"].ToString());
                    }
                    mPurchaseDetail.EXPENSE_AMOUNT = EXPENSE_AMOUNT;
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.PRE_AVG_RATE = decimal.Parse(dr["PRE_AVG_RATE"].ToString());
                    mPurchaseDetail.ExecuteQuery();


                    if (CodeFileName == "QureshiAccountHead.xml")
                    {
                        // For Qureshi
                        UspProcessStockRegister2 mStockUpdate = new UspProcessStockRegister2();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                        mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                        mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                        mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                        mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                        mStockUpdate.FREE_QTY = decimal.Parse(dr["AVG_PRICE"].ToString()); //mPurchaseDetail.FREE_SKU;
                        mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                        mStockUpdate.ExecuteQuery();
                    }
                    else
                    {
                        // For Roots & Other
                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                        mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                        mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                        mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                        mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                        mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                        mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                        mStockUpdate.ExecuteQuery();
                    }

                    
                }

                #region Mapped Acount Selection
                


                long p_PrincipalAccount_ID = 88;//2002080001	Payable to (Principal Wise)
                string p_PrincipalAccount_Name = "";
                if (mPurchaseMaster.TYPE_ID == 2 || mPurchaseMaster.TYPE_ID == 3)// purchase and purchase return
                {
                    //DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, mPurchaseMaster.PRINCIPAL_ID, Constants.LongNullValue, Constants.IntNullValue);
                    //if (dtAccount != null && dtAccount.Rows.Count > 0)
                    //{
                    //    p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                    //    p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    //}
                    p_PrincipalAccount_ID = p_Account_Head_ID;
                    p_PrincipalAccount_Name = p_Account_Head_Name;

                }
                else if (mPurchaseMaster.TYPE_ID == 4 || mPurchaseMaster.TYPE_ID == 5)// transfer in ,out
                {
                    p_PrincipalAccount_ID = 844;// 3002010002 Stock Movements
                    p_PrincipalAccount_Name = "Stock Movements";
                }
                else if (mPurchaseMaster.TYPE_ID == 6)//Damage
                {
                    p_PrincipalAccount_ID = 843;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damage and Expiries Expense";
                }
                else if (mPurchaseMaster.TYPE_ID == 10)//Damage claimable
                {
                    p_PrincipalAccount_ID = 845;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damage & Expiry - Claimable";
                }
                else if (mPurchaseMaster.TYPE_ID == 8)// Short
                {
                    p_PrincipalAccount_ID = 898;//3002020076 Warehouse Incharge
                    p_PrincipalAccount_Name = "Warehouse Incharge";
                }
                else
                {
                    DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, mPurchaseMaster.PRINCIPAL_ID, Constants.LongNullValue, Constants.IntNullValue);
                    if (dtAccount != null && dtAccount.Rows.Count > 0)
                    {
                        p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                        p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    }
                }
                #endregion

                #region GL Voucher
                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                string VoucherNo = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, DateTime.Now, mTransaction, mConnection);
                p_Voucher_No = VoucherNo;
                string ManualRemarks = "";
                if (p_REMARKS.Equals(""))
                {
                    ManualRemarks = p_REMARKS;
                }
                else
                {
                    ManualRemarks = " / " + p_REMARKS;
                }
                if (mPurchaseMaster.TYPE_ID == 2) // Purchase
                {
                    #region PURCHASE
                    string Remarks = "Default Purchase Voucher / Document # " + p_ORDER_NUMBER + " / " + p_Principal_Name + " / INV #" + Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID) + ManualRemarks;
                    string Remarks2 = "Default Purchase Voucher";
                    string DocumentType = "Purchase";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, 0, p_TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_OverHead_ID, 0, p_TotalOHAmount, Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots & Other
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                        }
                    }
                    #endregion
                }
                else if (mPurchaseMaster.TYPE_ID == 3) // Purchase Return
                {
                    #region PURCHASE RETURN
                    string Remarks = "Default Purchase Return Voucher / Document # " + p_ORDER_NUMBER + " / " + p_Principal_Name + " / INV #" + Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID) + ManualRemarks;
                    string Remarks2 = "Default Purchase Return Voucher";
                    string DocumentType = "Purchase Return";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            //for Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                        }
                    }
                    #endregion
                }
                else if (mPurchaseMaster.TYPE_ID == 4) // Transfer IN
                {
                    #region TRANSFER IN
                    string Remarks = "Default Transfer In Voucher";
                    string DocumentType = "Transfer IN";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {

                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock in Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Way" + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                        }
                    }
                    #endregion
                }
                else if (mPurchaseMaster.TYPE_ID == 5) // Transfer OUT
                {
                    #region TRANSFER OUT
                    string Remarks = "Default Transfer Out Voucher";
                    string DocumentType = "Transfer Out";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Way " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                        }
                    }
                    #endregion
                }
                else if (mPurchaseMaster.TYPE_ID == 6) // Damage
                {
                    #region DAMAGE
                    string Remarks = "Default Stock Damage Voucher";
                    string DocumentType = "Stock Damage";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 843, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }
                    }
                    #endregion
                }
                else if (mPurchaseMaster.TYPE_ID == 10) // Damage claimable
                {
                    #region DAMAGE CLAIMABLE
                    string Remarks = "Default Stock Damage(Claimable) Voucher";
                    string DocumentType = "Stock Damage(Claimable)";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, mPurchaseMaster.PRINCIPAL_ID, Constants.Journal_Voucher, VoucherNo, 845, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }
                    }
                    #endregion
                }
                else if (mPurchaseMaster.TYPE_ID == 8) // Short
                {
                    #region SHORT
                    string Remarks = "Default Stock Short Voucher";
                    string DocumentType = "Stock Short";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), mPurchaseMaster.TOTAL_AMOUNT, 0, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 901, 0, mPurchaseMaster.TOTAL_AMOUNT, "Sales (All Principle) " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 898, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }
                    }
                    #endregion
                }

                else if (mPurchaseMaster.TYPE_ID == 9) // Excess
                {
                    #region AXCESS
                    string Remarks = "Default Stock Excess Voucher";
                    string DocumentType = "Stock Excess";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), 0, mPurchaseMaster.TOTAL_AMOUNT, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 900, 0, mPurchaseMaster.TOTAL_AMOUNT, "Unadjusted Credit Balance - Purchase " + Remarks, mTransaction, mConnection);
                        }

                    }
                    #endregion
                }

                #endregion
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                p_Voucher_No = "";
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateStockDocument(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal,string p_REMARKS)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.QUANTITY_CTN = int.Parse(dr["QUANTITY_CTN"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateStockDocumentGL(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, int p_Account_Head_ID, string p_Account_Head_Name, out string p_Voucher_No, string p_REMARKS)
        {
            try
            {
                p_Voucher_No = "";
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.DATE = DateTime.Parse(dr["TIME_STAMP"].ToString());
                    if (p_TYPE_ID == 2)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["PURCHASED"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else if (p_TYPE_ID == 3)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["PURCHASE_RETURN"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else if (p_TYPE_ID == 4)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["TRANSFER_IN"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = 0;
                    }
                    else if (p_TYPE_ID == 5)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["TRANSFER_OUT"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = 0;
                    }
                    else if (p_TYPE_ID == 6)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["DAMAGED_STOCK"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else if (p_TYPE_ID == 7)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["OPENING_STOCK"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else if (p_TYPE_ID == 8)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["SHORT"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else if (p_TYPE_ID == 9)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["EXCESS"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else
                    {
                        mPurchaseStock.QUANTITY = 0;
                    }
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.QUANTITY_CTN = int.Parse(dr["QUANTITY_CTN"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                }

                #region Mapped Acount Selection
                CustomerDataController cd = new CustomerDataController();
                string CodeFileName = "";
                DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);

                if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
                {
                    CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
                    Configuration.GetAppMapping(CodeFileName);
                }

                if (p_Principal == Constants.IntNullValue)
                {
                    p_Principal = 0;
                }
                long p_PrincipalAccount_ID = 88;//2002080001	Payable to (Principal Wise)
                string p_PrincipalAccount_Name = "";
                if (mPurchaseMaster.TYPE_ID == 2 || mPurchaseMaster.TYPE_ID == 3)// purchase and purchase return
                {
                    //DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, p_Principal, Constants.LongNullValue, Constants.IntNullValue);
                    //if (dtAccount != null && dtAccount.Rows.Count > 0)
                    //{
                    //    p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                    //    p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    //}
                    p_PrincipalAccount_ID = p_Account_Head_ID;
                    p_PrincipalAccount_Name = p_Account_Head_Name;
                }
                else if (mPurchaseMaster.TYPE_ID == 4 || mPurchaseMaster.TYPE_ID == 5)// transfer in ,out
                {
                    p_PrincipalAccount_ID = 844;// 3002010002 Stock Movements
                    p_PrincipalAccount_Name = "Stock Movements";
                }
                else if (mPurchaseMaster.TYPE_ID == 6)//Damage
                {
                    p_PrincipalAccount_ID = 843;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damage and Expiries Expense";
                }
                else if (mPurchaseMaster.TYPE_ID == 10)//Damage claimable
                {
                    p_PrincipalAccount_ID = 845;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damages and Expiries - Claimable";
                }
                else if (mPurchaseMaster.TYPE_ID == 8 )// Short
                {
                    p_PrincipalAccount_ID = 898;//3002020076 Warehouse Incharge
                    p_PrincipalAccount_Name = "Warehouse Incharge";
                }
                else
                {
                    DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, p_Principal, Constants.LongNullValue, Constants.IntNullValue);
                    if (dtAccount != null && dtAccount.Rows.Count > 0)
                    {
                        p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                        p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    }
                }
                #endregion


                #region GL Voucher
                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                string VoucherNo = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, DateTime.Now, mTransaction, mConnection);
                p_Voucher_No = VoucherNo;
                if (mPurchaseMaster.TYPE_ID == 2)// Purchase
                {
                    string Remarks = "Default Purchase Voucher / Document # " + p_ORDER_NUMBER;
                    string Remarks2 = "Default Purchase Voucher";
                    string DocumentType = "Purchase";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, 0, mPurchaseMaster.TOTAL_AMOUNT, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, 0, mPurchaseMaster.TOTAL_AMOUNT, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }
                        
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 3)// Purchase Return
                {
                    string Remarks = "Default Purchase Return Voucher / Document # " + p_ORDER_NUMBER;
                    string Remarks2 = "Default Purchase Return Voucher";
                    string DocumentType = "Purchase Return";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }
                        
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 4)// Transfer IN
                {
                    string Remarks = "Default Transfer In Voucher";
                    string DocumentType = "Transfer IN";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {

                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock in Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Way" + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 5)// Transfer OUT
                {
                    string Remarks = "Default Transfer Out Voucher";
                    string DocumentType = "Transfer Out";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Way " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 6)// Damage
                {
                    string Remarks = "Default Stock Damage Voucher / Document # " + mPurchaseMaster.PURCHASE_MASTER_ID;
                    string Remarks2 = "Default Stock Damage Voucher";
                    string DocumentType = "Stock Damage";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }
                        
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 10)// Damage(claimable)
                {
                    string Remarks = "Default Stock Damage(Claimable) Voucher / Document # " + mPurchaseMaster.PURCHASE_MASTER_ID;
                    string Remarks2 = "Default Stock Damage(Claimable) Voucher";
                    string DocumentType = "Stock Damage(Claimable)";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }
                        
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 7) // Opening Stock
                {
                    string Remarks = "Default Opening Stock Voucher";
                    string DocumentType = "Opening Stock";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.FaheemQureshiCapital), 0, mPurchaseMaster.TOTAL_AMOUNT, "Mr. Faheem Qureshi Capital A/C " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 8)// Short
                {
                    string Remarks = "Default Stock Short Voucher / Document # " + mPurchaseMaster.PURCHASE_MASTER_ID;
                    string Remarks2 = "Default Stock Short Voucher";
                    string DocumentType = "Stock Short";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), mPurchaseMaster.TOTAL_AMOUNT, 0, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 901, 0, mPurchaseMaster.TOTAL_AMOUNT, "Sales (All Principle) " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks2, mTransaction, mConnection);
                        }
                        
                    }
                }

                else if (mPurchaseMaster.TYPE_ID == 9)// Excess
                {
                    string Remarks = "Default Stock Excess Voucher / Document # " + mPurchaseMaster.PURCHASE_MASTER_ID;
                    string Remarks2 = "Default Stock Excess Voucher";
                    string DocumentType = "Stock Excess";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID,p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), 0, mPurchaseMaster.TOTAL_AMOUNT, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, "Purchases " + Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 900, 0, mPurchaseMaster.TOTAL_AMOUNT, "Unadjusted Credit balance - Purchase " + Remarks2, mTransaction, mConnection);
                        }
                        
                    }
                }


                #endregion

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                p_Voucher_No = "";
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool UpdateStockDocumentGLForPurchase(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, int p_Account_Head_ID, string p_Account_Head_Name, out string p_Voucher_No, string p_REMARKS, string p_Principal_Name, int p_OverHead_ID, decimal p_TotalOHAmount, decimal p_TotalWeight, int p_ClientType)
        {

            CustomerDataController cd = new CustomerDataController();
            string CodeFileName = "";
            DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);

            if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
            {
                CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
                Configuration.GetAppMapping(CodeFileName);
            }
            try
            {
                p_Voucher_No = "";
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT + p_TotalOHAmount;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.EXPENSE_AMOUNT = p_TotalOHAmount;
                mPurchaseMaster.EXPENSE_HEAD_ID = p_OverHead_ID;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.DATE = DateTime.Parse(dr["TIME_STAMP"].ToString());
                    if (p_TYPE_ID == 2)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["PURCHASED"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = int.Parse(dr["FREE_STOCK_IN"].ToString()) - int.Parse(dr["FREE_SKU"].ToString());
                    }
                    else if (p_TYPE_ID == 3)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["PURCHASE_RETURN"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = int.Parse(dr["FREE_STOCK_IN"].ToString()) - int.Parse(dr["FREE_SKU"].ToString());
                    }
                    else if (p_TYPE_ID == 4)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["TRANSFER_IN"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = int.Parse(dr["FREE_STOCK_IN"].ToString()) - int.Parse(dr["FREE_SKU"].ToString());
                    }
                    else if (p_TYPE_ID == 5)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["TRANSFER_OUT"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = int.Parse(dr["FREE_STOCK_IN"].ToString()) - int.Parse(dr["FREE_SKU"].ToString());
                    }
                    else if (p_TYPE_ID == 6)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["DAMAGED_STOCK"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = int.Parse(dr["FREE_STOCK_IN"].ToString()) - int.Parse(dr["FREE_SKU"].ToString());
                    }
                    else if (p_TYPE_ID == 7)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["OPENING_STOCK"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else if (p_TYPE_ID == 8)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["SHORT"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else if (p_TYPE_ID == 9)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["EXCESS"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else
                    {
                        mPurchaseStock.QUANTITY = 0;
                    }
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAILWithSKU_PRICE mPurchaseDetail = new spInsertPURCHASE_DETAILWithSKU_PRICE();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.QUANTITY_CTN = int.Parse(dr["QUANTITY_CTN"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TOTAL_WEIGHT = decimal.Parse(dr["TOTAL_SKU_WEIGHT"].ToString());
                    decimal EXPENSE_AMOUNT;
                    if (p_TotalWeight == 0)
                    {
                        EXPENSE_AMOUNT = 0;
                    }
                    else
                    {
                        EXPENSE_AMOUNT = (p_TotalOHAmount / p_TotalWeight) * decimal.Parse(dr["SKU_WEIGHT"].ToString());
                    }
                    mPurchaseDetail.EXPENSE_AMOUNT = EXPENSE_AMOUNT;
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    //mPurchaseDetail.PRE_AVG_RATE = decimal.Parse(dr["PRE_AVG_RATE"].ToString());
                    mPurchaseDetail.ExecuteQuery();


                    //if (CodeFileName == "QureshiAccountHead.xml")
                    //{
                    //    // For Qureshi
                    //    UspProcessStockRegister2 mStockUpdate = new UspProcessStockRegister2();
                    //    mStockUpdate.Connection = mConnection;
                    //    mStockUpdate.Transaction = mTransaction;
                    //    mStockUpdate.PRINCIPAL_ID = p_Principal;
                    //    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    //    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    //    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    //    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    //    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                    //    mStockUpdate.FREE_QTY = int.Parse(dr["AVG_PRICE"].ToString()); //mPurchaseDetail.FREE_SKU;
                    //    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    //    mStockUpdate.ExecuteQuery();
                    //}
                    //else
                    //{
                        // For Roots & Other
                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.PRINCIPAL_ID = p_Principal;
                        mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                        mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                        mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                        mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                        mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                        mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                        mStockUpdate.ExecuteQuery();
                    //}

                   
                }

                #region Mapped Acount Selection
                

                long p_PrincipalAccount_ID = 88;//2002080001	Payable to (Principal Wise)
                string p_PrincipalAccount_Name = "";
                if (mPurchaseMaster.TYPE_ID == 2 || mPurchaseMaster.TYPE_ID == 3)// purchase and purchase return
                {
                    //DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, p_Principal, Constants.LongNullValue, Constants.IntNullValue);
                    //if (dtAccount != null && dtAccount.Rows.Count > 0)
                    //{
                    //    p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                    //    p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    //}
                    p_PrincipalAccount_ID = p_Account_Head_ID;
                    p_PrincipalAccount_Name = p_Account_Head_Name;
                }
                else if (mPurchaseMaster.TYPE_ID == 4 || mPurchaseMaster.TYPE_ID == 5)// transfer in ,out
                {
                    p_PrincipalAccount_ID = 844;// 3002010002 Stock Movements
                    p_PrincipalAccount_Name = "Stock Movements";
                }
                else if (mPurchaseMaster.TYPE_ID == 6)//Damage
                {
                    p_PrincipalAccount_ID = 843;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damage and Expiries Expense";
                }
                else if (mPurchaseMaster.TYPE_ID == 10)//Damage claimable
                {
                    p_PrincipalAccount_ID = 845;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damages and Expiries - Claimable";
                }
                else if (mPurchaseMaster.TYPE_ID == 8)// Short
                {
                    p_PrincipalAccount_ID = 898;//3002020076 Warehouse Incharge
                    p_PrincipalAccount_Name = "Warehouse Incharge";
                }
                else
                {
                    DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, p_Principal, Constants.LongNullValue, Constants.IntNullValue);
                    if (dtAccount != null && dtAccount.Rows.Count > 0)
                    {
                        p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                        p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    }
                }
                #endregion


                #region GL Voucher
                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                string VoucherNo = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, DateTime.Now, mTransaction, mConnection);
                p_Voucher_No = VoucherNo;
                if (mPurchaseMaster.TYPE_ID == 2)// Purchase
                {
                    string ManualRemarks = "";
                    if (p_REMARKS.Equals(""))
                    {
                        ManualRemarks = p_REMARKS;
                    }
                    else
                    {
                        ManualRemarks = " / " + p_REMARKS;
                    }
                    string Remarks = "Default Purchase Voucher / Document # " + p_ORDER_NUMBER + " / " + p_Principal_Name + " / INV #" + Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID) + ManualRemarks;
                    string Remarks2 = "Default Purchase Voucher";
                    string DocumentType = "Purchase";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, 0, p_TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_OverHead_ID, 0, p_TotalOHAmount, Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots & Other
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                        }
                    }

                }
                else if (mPurchaseMaster.TYPE_ID == 3)// Purchase Return
                {
                    string ManualRemarks = "";
                    if (p_REMARKS.Equals(""))
                    {
                        ManualRemarks = p_REMARKS;
                    }
                    else
                    {
                        ManualRemarks = " / " + p_REMARKS;
                    }
                    string Remarks = "Default Purchase Return Voucher / Document # " + p_ORDER_NUMBER + " / " + p_Principal_Name + " / INV #" + Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID) + ManualRemarks;
                    string Remarks2 = "Default Purchase Return Voucher";
                    string DocumentType = "Purchase Return";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            //for Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                        }
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 4)// Transfer IN
                {
                    string Remarks = "Default Transfer In Voucher";
                    string DocumentType = "Transfer IN";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {

                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock in Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Way" + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 5)// Transfer OUT
                {
                    string Remarks = "Default Transfer Out Voucher";
                    string DocumentType = "Transfer Out";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Way " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 6)// Damage
                {
                    string Remarks = "Default Stock Damage Voucher";
                    string DocumentType = "Stock Damage";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 843, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 10)// Damage claimable
                {
                    string Remarks = "Default Stock Damage(Claimable) Voucher";
                    string DocumentType = "Stock Damage(Claimable)";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 845, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 8)// Short
                {
                    string Remarks = "Default Stock Short Voucher";
                    string DocumentType = "Stock Short";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), mPurchaseMaster.TOTAL_AMOUNT, 0, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 901, 0, mPurchaseMaster.TOTAL_AMOUNT, "Sales (All Principle) " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 898, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }

                    }
                }

                else if (mPurchaseMaster.TYPE_ID == 9)// Excess
                {
                    string Remarks = "Default Stock Excess Voucher";
                    string DocumentType = "Stock Excess";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), 0, mPurchaseMaster.TOTAL_AMOUNT, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 900, 0, mPurchaseMaster.TOTAL_AMOUNT, "Unadjusted Credit Balance - Purchase " + Remarks, mTransaction, mConnection);
                        }

                    }
                }


                #endregion

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                p_Voucher_No = "";
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateStockDocumentGLForPurchaseForQureshi(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal, int p_Account_Head_ID, string p_Account_Head_Name, out string p_Voucher_No, string p_REMARKS, string p_Principal_Name, int p_OverHead_ID, decimal p_TotalOHAmount, decimal p_TotalWeight, int p_ClientType)
        {

            CustomerDataController cd = new CustomerDataController();
            string CodeFileName = "";
            DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);

            if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
            {
                CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
                Configuration.GetAppMapping(CodeFileName);
            }
            try
            {
                p_Voucher_No = "";
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT + p_TotalOHAmount;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.EXPENSE_AMOUNT = p_TotalOHAmount;
                mPurchaseMaster.EXPENSE_HEAD_ID = p_OverHead_ID;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.DATE = DateTime.Parse(dr["TIME_STAMP"].ToString());
                    if (p_TYPE_ID == 2)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["PURCHASED"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = int.Parse(dr["FREE_STOCK_IN"].ToString()) - int.Parse(dr["FREE_SKU"].ToString());
                    }
                    else if (p_TYPE_ID == 3)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["PURCHASE_RETURN"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = int.Parse(dr["FREE_STOCK_IN"].ToString()) - int.Parse(dr["FREE_SKU"].ToString());
                    }
                    else if (p_TYPE_ID == 4)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["TRANSFER_IN"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = int.Parse(dr["FREE_STOCK_IN"].ToString()) - int.Parse(dr["FREE_SKU"].ToString());
                    }
                    else if (p_TYPE_ID == 5)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["TRANSFER_OUT"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = int.Parse(dr["FREE_STOCK_IN"].ToString()) - int.Parse(dr["FREE_SKU"].ToString());
                    }
                    else if (p_TYPE_ID == 6)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["DAMAGED_STOCK"].ToString()) - int.Parse(dr["QTY"].ToString());
                        mPurchaseStock.FREE_QTY = int.Parse(dr["FREE_STOCK_IN"].ToString()) - int.Parse(dr["FREE_SKU"].ToString());
                    }
                    else if (p_TYPE_ID == 7)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["OPENING_STOCK"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else if (p_TYPE_ID == 8)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["SHORT"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else if (p_TYPE_ID == 9)
                    {
                        mPurchaseStock.QUANTITY = int.Parse(dr["EXCESS"].ToString()) - int.Parse(dr["QTY"].ToString());
                    }
                    else
                    {
                        mPurchaseStock.QUANTITY = 0;
                    }
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.QUANTITY_CTN = int.Parse(dr["QUANTITY_CTN"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TOTAL_WEIGHT = decimal.Parse(dr["TOTAL_SKU_WEIGHT"].ToString());
                    decimal EXPENSE_AMOUNT;
                    if (p_TotalWeight == 0)
                    {
                        EXPENSE_AMOUNT = 0;
                    }
                    else
                    {
                        EXPENSE_AMOUNT = (p_TotalOHAmount / p_TotalWeight) * decimal.Parse(dr["SKU_WEIGHT"].ToString());
                    }
                    mPurchaseDetail.EXPENSE_AMOUNT = EXPENSE_AMOUNT;
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.PRE_AVG_RATE = decimal.Parse(dr["PRE_AVG_RATE"].ToString());
                    mPurchaseDetail.ExecuteQuery();


                    if (CodeFileName == "QureshiAccountHead.xml")
                    {
                        // For Qureshi
                        UspProcessStockRegister2 mStockUpdate = new UspProcessStockRegister2();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.PRINCIPAL_ID = p_Principal;
                        mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                        mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                        mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                        mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                        mStockUpdate.FREE_QTY = decimal.Parse(dr["AVG_PRICE"].ToString()); //mPurchaseDetail.FREE_SKU;
                        mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                        mStockUpdate.ExecuteQuery();
                    }
                    else
                    {
                        // For Roots & Other
                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.PRINCIPAL_ID = p_Principal;
                        mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                        mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                        mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                        mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                        mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                        mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                        mStockUpdate.ExecuteQuery();
                    }


                }

                #region Mapped Acount Selection


                long p_PrincipalAccount_ID = 88;//2002080001	Payable to (Principal Wise)
                string p_PrincipalAccount_Name = "";
                if (mPurchaseMaster.TYPE_ID == 2 || mPurchaseMaster.TYPE_ID == 3)// purchase and purchase return
                {
                    //DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, p_Principal, Constants.LongNullValue, Constants.IntNullValue);
                    //if (dtAccount != null && dtAccount.Rows.Count > 0)
                    //{
                    //    p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                    //    p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    //}
                    p_PrincipalAccount_ID = p_Account_Head_ID;
                    p_PrincipalAccount_Name = p_Account_Head_Name;
                }
                else if (mPurchaseMaster.TYPE_ID == 4 || mPurchaseMaster.TYPE_ID == 5)// transfer in ,out
                {
                    p_PrincipalAccount_ID = 844;// 3002010002 Stock Movements
                    p_PrincipalAccount_Name = "Stock Movements";
                }
                else if (mPurchaseMaster.TYPE_ID == 6)//Damage
                {
                    p_PrincipalAccount_ID = 843;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damage and Expiries Expense";
                }
                else if (mPurchaseMaster.TYPE_ID == 10)//Damage claimable
                {
                    p_PrincipalAccount_ID = 845;//5002020013//Damage and Expiries Expense
                    p_PrincipalAccount_Name = "Damages and Expiries - Claimable";
                }
                else if (mPurchaseMaster.TYPE_ID == 8)// Short
                {
                    p_PrincipalAccount_ID = 898;//3002020076 Warehouse Incharge
                    p_PrincipalAccount_Name = "Warehouse Incharge";
                }
                else
                {
                    DataTable dtAccount = cd.SelectAccountHeadMapping(1, Constants.IntNullValue, p_Principal, Constants.LongNullValue, Constants.IntNullValue);
                    if (dtAccount != null && dtAccount.Rows.Count > 0)
                    {
                        p_PrincipalAccount_ID = long.Parse(dtAccount.Rows[0]["Account_head_ID"].ToString());
                        p_PrincipalAccount_Name = dtAccount.Rows[0]["ACCOUNT_NAME"].ToString();

                    }
                }
                #endregion


                #region GL Voucher
                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                string VoucherNo = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, DateTime.Now, mTransaction, mConnection);
                p_Voucher_No = VoucherNo;
                if (mPurchaseMaster.TYPE_ID == 2)// Purchase
                {
                    string ManualRemarks = "";
                    if (p_REMARKS.Equals(""))
                    {
                        ManualRemarks = p_REMARKS;
                    }
                    else
                    {
                        ManualRemarks = " / " + p_REMARKS;
                    }
                    string Remarks = "Default Purchase Voucher / Document # " + p_ORDER_NUMBER + " / " + p_Principal_Name + " / INV #" + Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID) + ManualRemarks;
                    string Remarks2 = "Default Purchase Voucher";
                    string DocumentType = "Purchase";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, 0, p_TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_OverHead_ID, 0, p_TotalOHAmount, Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots & Other
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                        }
                    }

                }
                else if (mPurchaseMaster.TYPE_ID == 3)// Purchase Return
                {
                    string ManualRemarks = "";
                    if (p_REMARKS.Equals(""))
                    {
                        ManualRemarks = p_REMARKS;
                    }
                    else
                    {
                        ManualRemarks = " / " + p_REMARKS;
                    }
                    string Remarks = "Default Purchase Return Voucher / Document # " + p_ORDER_NUMBER + " / " + p_Principal_Name + " / INV #" + Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID) + ManualRemarks;
                    string Remarks2 = "Default Purchase Return Voucher";
                    string DocumentType = "Purchase Return";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_Account_Head_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                        }
                        else
                        {
                            //for Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, Remarks2, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, p_PrincipalAccount_ID, mPurchaseMaster.TOTAL_AMOUNT, 0, Remarks2, mTransaction, mConnection);
                        }
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 4)// Transfer IN
                {
                    string Remarks = "Default Transfer In Voucher";
                    string DocumentType = "Transfer IN";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {

                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock in Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Way" + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 5)// Transfer OUT
                {
                    string Remarks = "Default Transfer Out Voucher";
                    string DocumentType = "Transfer Out";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Way " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 6)// Damage
                {
                    string Remarks = "Default Stock Damage Voucher";
                    string DocumentType = "Stock Damage";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 843, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 10)// Damage claimable
                {
                    string Remarks = "Default Stock Damage(Claimable) Voucher";
                    string DocumentType = "Stock Damage(Claimable)";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, p_Principal, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.DamageStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Damage Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 897, 0, mPurchaseMaster.TOTAL_AMOUNT, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, p_Principal, Constants.Journal_Voucher, VoucherNo, 845, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }

                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 8)// Short
                {
                    string Remarks = "Default Stock Short Voucher";
                    string DocumentType = "Stock Short";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), mPurchaseMaster.TOTAL_AMOUNT, 0, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 901, 0, mPurchaseMaster.TOTAL_AMOUNT, "Sales (All Principle) " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 898, mPurchaseMaster.TOTAL_AMOUNT, 0, p_PrincipalAccount_Name + " " + Remarks, mTransaction, mConnection);
                        }

                    }
                }

                else if (mPurchaseMaster.TYPE_ID == 9)// Excess
                {
                    string Remarks = "Default Stock Excess Voucher";
                    string DocumentType = "Stock Excess";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), 0, mPurchaseMaster.TOTAL_AMOUNT, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.CostOfGoodsSold), 0, mPurchaseMaster.TOTAL_AMOUNT, "Cost Of Goods Sold " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.ShortOrAccessStock), mPurchaseMaster.TOTAL_AMOUNT, 0, "Short Or Access Stock " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 897, mPurchaseMaster.TOTAL_AMOUNT, 0, "Purchases " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, 900, 0, mPurchaseMaster.TOTAL_AMOUNT, "Unadjusted Credit Balance - Purchase " + Remarks, mTransaction, mConnection);
                        }

                    }
                }


                #endregion

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                p_Voucher_No = "";
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool InsertPurchaseDocument(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId)
        {
            CustomerDataController cd = new CustomerDataController();
            string CodeFileName = "";
            DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);

            if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
            {
                CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
                Configuration.GetAppMapping(CodeFileName);
            }
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.QUANTITY_CTN = int.Parse(dr["QUANTITY_CTN"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    mPurchaseDetail.PRE_AVG_RATE = decimal.Parse(dr["PRE_AVG_RATE"].ToString());
                    mPurchaseDetail.ExecuteQuery();

                    if (CodeFileName == "QureshiAccountHead.xml")
                    {
                        if (mPurchaseMaster.TYPE_ID == 4)// Transfer IN
                        {
                            // For Qureshi
                            UspProcessStockRegister2 mStockUpdate = new UspProcessStockRegister2();
                            mStockUpdate.Connection = mConnection;
                            mStockUpdate.Transaction = mTransaction;
                            mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                            mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                            mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                            mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                            mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                            mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                            mStockUpdate.FREE_QTY = decimal.Parse(dr["AVG_PRICE"].ToString()); //mPurchaseDetail.FREE_SKU;
                            mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                            mStockUpdate.ExecuteQuery();
                        }
                        else
                        {
                            UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                            mStockUpdate.Connection = mConnection;
                            mStockUpdate.Transaction = mTransaction;
                            mStockUpdate.PRINCIPAL_ID = int.Parse(dr["PRINCIPAL_ID"].ToString());
                            mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                            mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                            mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                            mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                            mStockUpdate.STOCK_QTY = int.Parse(dr["QTY"].ToString());
                            mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                            mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                            mStockUpdate.ExecuteQuery();
                        }
                    }
                    else
                    {
                        // For Roots & Other
                        UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                        mStockUpdate.Connection = mConnection;
                        mStockUpdate.Transaction = mTransaction;
                        mStockUpdate.PRINCIPAL_ID = p_PrincipalId;
                        mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                        mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                        mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                        mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                        mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY + (int.Parse(dr["QUANTITY_CTN"].ToString()) * int.Parse(dr["UNITS_IN_CASE"].ToString()));
                        mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                        mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                        mStockUpdate.ExecuteQuery();
                    }
                    //UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    //mStockUpdate.Connection = mConnection;
                    //mStockUpdate.Transaction = mTransaction;
                    //mStockUpdate.PRINCIPAL_ID = int.Parse(dr["PRINCIPAL_ID"].ToString());
                    //mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    //mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    //mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    //mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    //mStockUpdate.STOCK_QTY = int.Parse(dr["QTY"].ToString());
                    //mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    //mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    //mStockUpdate.ExecuteQuery();
                }

                #region Mapped Acount Selection
                //CustomerDataController cd = new CustomerDataController();
                //string CodeFileName = "";
                //DataTable dtMappSetting = cd.SelectMappingSetting(Constants.IntNullValue, Constants.IntNullValue);

                //if (dtMappSetting != null || dtMappSetting.Rows.Count > 0)
                //{
                //    CodeFileName = dtMappSetting.Rows[0]["CODE_FILE"].ToString();
                //    Configuration.GetAppMapping(CodeFileName);
                //}

                long p_PrincipalAccount_ID = 88;//2002080001	Payable to (Principal Wise)
                string p_PrincipalAccount_Name = "";
                #endregion

                #region GL ACCOUNT
                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                string VoucherNo = LController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, DateTime.Now, mTransaction, mConnection);
                if (mPurchaseMaster.TYPE_ID == 4)// Transfer IN
                {
                    string Remarks = "Default Transfer In Voucher";
                    string DocumentType = "Transfer IN";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {

                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock in Trade " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Way" + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }
                    }
                }
                else if (mPurchaseMaster.TYPE_ID == 5)// Transfer OUT
                {
                    string Remarks = "Default Transfer Out Voucher";
                    string DocumentType = "Transfer Out";
                    if (LController.PostingGLMaster(mPurchaseMaster.DISTRIBUTOR_ID, 0, VoucherNo, Constants.Journal_Voucher, mPurchaseMaster.DOCUMENT_DATE, mPurchaseMaster.TYPE_ID, Convert.ToString(mPurchaseMaster.PURCHASE_MASTER_ID), Remarks, p_UserId, DocumentType, mPurchaseMaster.PURCHASE_MASTER_ID, mPurchaseMaster.TYPE_ID, mTransaction, mConnection))
                    {
                        if (CodeFileName == "QureshiAccountHead.xml")
                        {
                            // For Qureshi
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.StockInWay), mPurchaseMaster.TOTAL_AMOUNT, 0, "Stock In Way " + Remarks, mTransaction, mConnection);
                            LController.PostingGLDetail(p_DISTRIBUTOR_ID, 0, Constants.Journal_Voucher, VoucherNo, long.Parse(Configuration.Purchase), 0, mPurchaseMaster.TOTAL_AMOUNT, "Stock In Trade " + Remarks, mTransaction, mConnection);
                        }
                        else
                        {
                            // For Roots

                        }

                    }
                }
                #endregion

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
       
        /// <summary>
        /// Updates Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_Principal">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdatePurchaseDocument(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId,int p_Principal)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER(); 
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;  
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;   
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;  
                mPurchaseMaster.ExecuteQuery();
               
                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID,mConnection,mTransaction);
                
                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID; 
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }
                                
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    
                   //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;  
                    mPurchaseDetail.ExecuteQuery();

                    UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                    mStockUpdate.Connection = mConnection;
                    mStockUpdate.Transaction = mTransaction;
                    mStockUpdate.PRINCIPAL_ID = p_Principal; 
                    mStockUpdate.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mStockUpdate.DISTRIBUTOR_ID = mPurchaseMaster.DISTRIBUTOR_ID;
                    mStockUpdate.STOCK_DATE = mPurchaseMaster.DOCUMENT_DATE;
                    mStockUpdate.SKU_ID = mPurchaseDetail.SKU_ID;
                    mStockUpdate.STOCK_QTY = mPurchaseDetail.QUANTITY;
                    mStockUpdate.FREE_QTY = mPurchaseDetail.FREE_SKU;
                    mStockUpdate.BATCHNO = mPurchaseDetail.BATCH_NO;
                    mStockUpdate.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Posts Pending Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_Type_Id">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool PostPendingDocument(long p_PURCHASE_MASTER_ID,int p_Type_Id,int p_Distributor_Id,int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePURCHASE_MASTERCheckIN mPurchaseMaster = new spUpdatePURCHASE_MASTERCheckIN();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_Id;
                mPurchaseMaster.TYPE_ID = p_Type_Id;  
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #endregion
    }
}