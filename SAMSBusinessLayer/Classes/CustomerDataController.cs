using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using SAMSDatabaseLayer.InputClasses;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Customer Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Customer
    /// </item>
    /// <term>
    /// Update Customer
    /// </term>
    /// <item>
    /// Get Customer
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
	public class CustomerDataController
	{
        #region Constructor

        /// <summary>
        /// Constructor For CustomerDataController
        /// </summary>
        public CustomerDataController()
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
        /// Gets Customer Credit Limit
        /// </summary>
        /// <remarks>
        /// Returns Customer Credit Limit as Datatable
        /// </remarks>
        /// <param name="pDistributorId">Location</param>
        /// <param name="pCustomerId">Customer</param>
        /// <param name="pPrincipalId">Principal</param>
        /// <returns>Customer Credit Limit as Datatable</returns>
        public DataTable SelectCustomerCreditLimit(int pDistributorId,long pCustomerId,int pPrincipalId)
        {
            IDbConnection _mConnection = null;
            try
            {
                _mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                _mConnection.Open();
                spSelectCUSTOMER_PRINCIPAL_CREDITLIMIT mCustomer = new spSelectCUSTOMER_PRINCIPAL_CREDITLIMIT();
                mCustomer.Connection = _mConnection;
                mCustomer.DISTRIBUTOR_ID = pDistributorId;
                mCustomer.CUSTOMER_ID = pCustomerId;
                mCustomer.PRINCIPAL_ID = pPrincipalId;
                DataTable dt = mCustomer.ExecuteTable();  
                return dt;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (_mConnection != null && _mConnection.State == ConnectionState.Open)
                {
                    _mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Gets All Customers Opening Credit
        /// </summary>
        /// <remarks>
        /// Returns All Customers Opening Credit as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Document_Date"></param>
        /// <returns>All Customers Opening Credit as Datatable</returns>
        public DataTable SelectOpeningCredit(int p_Distributor_Id, DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectOpCredit mDistGeoArea = new UspSelectOpCredit();
                mDistGeoArea.Connection = mConnection;
                mDistGeoArea.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistGeoArea.DOCUMENT_DATE = p_Document_Date;
                DataTable dt = mDistGeoArea.ExecuteTable();
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
        public DataTable SelectOpeningCredit2(int p_Distributor_Id, DateTime p_Document_Date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectOpCredit2 mDistGeoArea = new UspSelectOpCredit2();
                mDistGeoArea.Connection = mConnection;
                mDistGeoArea.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistGeoArea.DOCUMENT_DATE = p_Document_Date;
                DataTable dt = mDistGeoArea.ExecuteTable();
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
        /// Gets Customers RouteWise
        /// </summary>
        /// <remarks>
        /// Returns Customers RouteWise as Datatable
        /// </remarks>
        /// <param name="strRoutes">Routes</param>
        /// <param name="strCustomerTypes">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <returns>Customers RouteWise as Datatable</returns>
        public DataTable SelectCustomerByRoute(string strRoutes, string strCustomerTypes, int p_Distributor_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectRouteCustomer mCustomer = new uspSelectRouteCustomer();
                mCustomer.Connection = mConnection;

                mCustomer.ROUTES = strRoutes;
                mCustomer.CUSTOMER_TYPES = strCustomerTypes;
                mCustomer.DISTRIBUTOR_ID = p_Distributor_Id;
                DataTable dt = mCustomer.ExecuteTable();
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
        /// Gets Customer With Invoices
        /// </summary>
        /// <remarks>
        /// Retruns Customers With Invoices as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <param name="p_document_date">Date</param>
        /// <returns>Customers With Invoices as Datatable</returns>
        public DataTable SelectPrincipalCustomer(int p_Distributor_Id, int p_PrincipalId, DateTime p_document_date)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectInvoiceBuiltyInfo mCustData = new UspSelectInvoiceBuiltyInfo();
                mCustData.Connection = mConnection;

                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.PRINCIPAL_ID = p_PrincipalId;
                mCustData.Document_date = p_document_date;
                mCustData.CUSTOMER_ID = Constants.LongNullValue;
                mCustData.PRINCIPAL_ID = p_PrincipalId;
                DataTable dt = mCustData.ExecuteTable();
                DataView dv = new DataView(dt);
                dv.Sort = "CUSTOMER_NAME";
                dt = dv.ToTable();
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
        /// Gets Customers
        /// </summary>
        /// <remarks>
        /// Returns Customers as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Area">Market</param>
        /// <param name="p_Route">Route</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>Customers as Datatable</returns>
        public DataTable SelectPrincipalCustomer(int p_Distributor_Id, int p_Area, int p_Route, int p_PrincipalId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectPRINCIPALCUSTOMER mCustData = new UspSelectPRINCIPALCUSTOMER();
                mCustData.Connection = mConnection;

                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.AREA_ID = p_Area;
                mCustData.ROUTE_ID = p_Route;
                mCustData.PRINCIPAL_ID = p_PrincipalId;
                DataTable dt = mCustData.ExecuteTable();
                DataView dv = new DataView(dt);
                dv.Sort = "CUSTOMER_NAME";
                dt = dv.ToTable();
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
        /// Gets Customers
        /// </summary>
        /// <remarks>
        /// Returns Customers as Datatable
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Area">Market</param>
        /// <param name="p_Route">Route</param>
        /// <returns>Customers as Datatable</returns>
        public DataTable SelectAllCustomer(int p_Distributor_Id, int p_Area, int p_Route, int p_Type)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCUSTOMER mCustData = new spSelectCUSTOMER();
                mCustData.Connection = mConnection;

                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.AREA_ID = p_Area;
                mCustData.TOWN_ID = p_Route;
                mCustData.TYPE_ID = p_Type;
                DataTable dt = mCustData.ExecuteTable();
                DataView dv = new DataView(dt);
                dv.Sort = "CUSTOMER_NAME";
                dt = dv.ToTable();
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

        public DataTable SelectAllCustomer(int p_Distributor_Id, int p_Area, int p_Route,int p_TOWN_ID, int p_Type)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCUSTOMER mCustData = new spSelectCUSTOMER();
                mCustData.Connection = mConnection;

                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.AREA_ID = p_Area;
                mCustData.ROUTE_ID = p_Route;
                mCustData.TOWN_ID = p_TOWN_ID;
                mCustData.TYPE_ID = p_Type;
                DataTable dt = mCustData.ExecuteTable();
                DataView dv = new DataView(dt);
                dv.Sort = "CUSTOMER_NAME";
                dt = dv.ToTable();
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

        public DataTable SelectAllCustomer_UnMapped(int p_Distributor_Id, int p_Area, int p_Route)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCUSTOMER_UnMapped mCustData = new spSelectCUSTOMER_UnMapped();
                mCustData.Connection = mConnection;

                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.AREA_ID = p_Area;
                mCustData.ROUTE_ID = p_Route;
                DataTable dt = mCustData.ExecuteTable();
                DataView dv = new DataView(dt);
                dv.Sort = "CUSTOMER_NAME";
                dt = dv.ToTable();
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
        /// Gets Customer Opening Balance
        /// </summary>
        /// <remarks>
        /// Returns Customer Opening Balance as Datatable
        /// </remarks>
        /// <param name="pDistributorId">Location</param>
        /// <param name="pCustomerId">Customer</param>
        /// <param name="pFromDate">DateFrom</param>
        /// <param name="pPrincipalId">Principal</param>
        /// <returns>Customer Opening Balance as Datatable</returns>
        public DataTable SelectPrincipalCustomerOpBalance(int pDistributorId, int pCustomerId, DateTime pFromDate, int pPrincipalId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspCustomerOpBalance mCustData = new UspCustomerOpBalance();
                mCustData.Connection = mConnection;

                mCustData.Distributor_id = pDistributorId;
                mCustData.principal_id = pPrincipalId;
                mCustData.From_Date = pFromDate;
                mCustData.customer_id = pCustomerId;
                DataTable dt = mCustData.ExecuteTable();
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
        /// Gets Customer For Different Search Criterias
        /// </summary>
        /// <remarks>
        /// Returns Customer as Datatable
        /// </remarks>
        /// <param name="pDistributorId">Location</param>
        /// <param name="pFieldname">FieldToSearchFor</param>
        /// <param name="pParametername">ValueToSearch</param>
        /// <returns>Customer as Datatable</returns>
        public DataTable UspSelectCustomer(int pDistributorId, string pFieldname, string pParametername)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectCUSTOMER mCustData = new UspSelectCUSTOMER();
                mCustData.Connection = mConnection;
                mCustData.FEILDNAME = pFieldname;
                mCustData.PARAMETER = pParametername;
                mCustData.DISTRIBUTOR_ID = pDistributorId;
                DataTable dt = mCustData.ExecuteTable();
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
        /// Gets Customer For Different Search Criterias
        /// </summary>
        /// <remarks>
        /// Returns Customer as Datatable
        /// </remarks>
        /// <param name="pUserId">User</param>
        /// <param name="pDistributorId">Location</param>
        /// <param name="pFieldname">FieldToSearchFor</param>
        /// <param name="pParametername">ValueToSearch</param>
        /// <returns>Customer as Datatable</returns>
        public DataTable UspSelectCustomer(int pUserId, int pDistributorId, string pFieldname, string pParametername)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspRptSelectCUSTOMER mCustData = new UspRptSelectCUSTOMER();
                mCustData.Connection = mConnection;
                mCustData.FEILDNAME = pFieldname;
                mCustData.PARAMETER = pParametername;
                mCustData.USER_ID = pUserId;
                mCustData.DISTRIBUTOR_ID = pDistributorId;
                DataTable dt = mCustData.ExecuteTable();
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
        /// Gets Customer Credit/Advance Balance
        /// </summary>
        /// <remarks>
        /// Returns Customer Credit/Advance Balance
        /// </remarks>
        /// <param name="pCustomerId">Customer</param>
        /// <param name="pPrincipalId">Principal</param>
        /// <param name="pDistributorId">Location</param>
        /// <param name="tranType">Type</param>
        /// <returns>Customer Credit/Advance Balance</returns>
        public DataTable SelectCustomerCreditBalance(long pCustomerId, int pPrincipalId, int pDistributorId, int tranType)
        {
            IDbConnection mConnection = null;
            try
            {
                Configuration.GetAccountHead();

                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectCustomerBalance mCustData = new UspSelectCustomerBalance();
                mCustData.Connection = mConnection;
                mCustData.CUSTOMER_ID = pCustomerId;
                mCustData.PRINCIPAL_ID = pPrincipalId;
                mCustData.DISTRIBUTOR_ID = pDistributorId;
                mCustData.TranType = tranType;
                mCustData.ACCOUNT_HEAD_ID = long.Parse(Configuration.AccountReceivable);

                DataTable dt = mCustData.ExecuteTable();
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
        /// Shifts Customers Between Locations, Routes, Markets
        /// </summary>
        /// <remarks>
        /// Returns True On Success And False On Failure
        /// </remarks>
        /// <param name="p_fromDistributor_Id">LocationFrom</param>
        /// <param name="p_toDistributor">LocationTo</param>
        /// <param name="p_fromArea_Id">MarketFrom</param>
        /// <param name="p_to_area_id">MarketTo</param>
        /// <param name="p_FROM_ROUTE_ID">RouteFrom</param>
        /// <param name="p_TO_ROUTE_ID">RouteTo</param>
        /// <param name="p_CUSTOMER_ID">Customer</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool CustomerShifting(int p_fromDistributor_Id, int p_toDistributor, int p_fromArea_Id, int p_to_area_id, int p_FROM_ROUTE_ID, int p_TO_ROUTE_ID, int p_CUSTOMER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspCustomerShifting mCustomerShift = new UspCustomerShifting();
                mCustomerShift.Connection = mConnection;
                mCustomerShift.FROM_DISTRIBUTOR_ID = p_fromDistributor_Id;
                mCustomerShift.FROM_AREA_ID = p_fromArea_Id;
                mCustomerShift.TO_DISTRIBUTOR_ID = p_toDistributor;
                mCustomerShift.TO_AREA_ID = p_to_area_id;
                mCustomerShift.FROM_ROUTE_ID = p_FROM_ROUTE_ID;
                mCustomerShift.TO_ROUTE_ID = p_TO_ROUTE_ID;
                mCustomerShift.CUSTOMER_ID = p_CUSTOMER_ID;
                mCustomerShift.ExecuteQuery();
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
		       
        /// <summary>
        /// Inserts Customer Credit Limit
        /// </summary>
        /// <remarks>
        /// Returns True On Success And False On Failure
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Customer_Id">Customer</param>
        /// <param name="p_Principal">Principal</param>
        /// <param name="p_CreditLimit">CreditLimit</param>
        /// <param name="p_CreditDays">CreditDays</param>
        /// <param name="p_ChannelTypeId">Channel</param>
        /// <param name="p_Category">Category</param>
        /// <param name="p_class">Class</param>
        /// <param name="p_Type">Type</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertCustomerCreditLimit(int p_Distributor_Id, long p_Customer_Id, int p_Principal, decimal p_CreditLimit,int p_CreditDays,int p_ChannelTypeId,int p_Category,string p_class,string p_Type,bool p_IsActive)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER_PRINCIPAL_CREDITLIMIT mCustData = new spInsertCUSTOMER_PRINCIPAL_CREDITLIMIT();

                mCustData.Connection = mConnection;

                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.CUSTOMER_ID = p_Customer_Id;
                mCustData.PRINCIPAL_ID = p_Principal;
                mCustData.CREDITLIMIT_VALUE = p_CreditLimit;
                mCustData.TIME_STAMP = DateTime.Today;
                mCustData.CREDIT_DAYS = p_CreditDays;
                mCustData.BUSINESS_TYPE_ID = p_Category;
                mCustData.CHANNEL_TYPE_ID = p_ChannelTypeId;
                mCustData.CLASSFICATION = p_class;
                mCustData.CUSTOMER_TYPE = p_Type;
                mCustData.IS_ACTIVE = p_IsActive; 
                mCustData.ExecuteQuery();
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
 
        /// <summary>
        /// Updates Customer Credit Limit
        /// </summary>
        /// <remarks>
        /// Returns True On Success And False On Failure
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Customer_Id">Customer</param>
        /// <param name="p_Principal">Principal</param>
        /// <param name="p_CreditLimit">CreditLimit</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdateCustomerCreditLimit(int p_Distributor_Id, long p_Customer_Id, int p_Principal, decimal p_CreditLimit)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER_PRINCIPAL_CREDITLIMIT mCustData = new spUpdateCUSTOMER_PRINCIPAL_CREDITLIMIT();

                mCustData.Connection = mConnection;
                mCustData.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustData.CUSTOMER_ID = p_Customer_Id;
                mCustData.PRINCIPAL_ID = p_Principal;
                mCustData.CREDITLIMIT_VALUE = p_CreditLimit;
                 
                mCustData.ExecuteQuery();
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
         
        /// <summary>
        /// Inserts Customer
        /// </summary>
        /// <remarks>
        /// Returns "Record Updated Customer Code " + Inserted Customer ID On Success And "Some Error Update Record" On Failure
        /// </remarks>
        /// <param name="p_Customer_Id">Customer</param>
        /// <param name="p_Is_Gst_Registered">IsGSTReg</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="p_Channel_Type_Id">Type</param>
        /// <param name="p_Volume_Class_Id">Class</param>
        /// <param name="p_Business_Type_Id">BusinessType</param>
        /// <param name="p_Area_Id">Market</param>
        /// <param name="p_Route_Id">Route</param>
        /// <param name="p_Town_Id">Town</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Gst_Number">GSTNumber</param>
        /// <param name="p_Contact_Person">ContactPerson</param>
        /// <param name="p_Contact_Number">ContactNo</param>
        /// <param name="p_Email_Address">Email</param>
        /// <param name="p_Customer_Code">Code</param>
        /// <param name="p_Customer_Name">Name</param>
        /// <param name="p_Address">Address</param>
        /// <param name="p_RegDate">RegDate</param>
        /// <param name="p_Stand">Stand</param>
        /// <param name="p_Cooler">Cooler</param>
        /// <param name="p_CNIC">CNIC</param>
        /// <param name="p_NTN">NTN</param>
        /// <returns>"Record Updated Customer Code " + Inserted Customer ID On Success And "Some Error Update Record" On Failure</returns>
        public string InsertCustomer(long p_Customer_Id, bool p_Is_Gst_Registered, bool p_Is_Active,int p_Channel_Type_Id, int p_Volume_Class_Id, int p_Business_Type_Id,int p_Area_Id, int p_Route_Id, int p_Town_Id, int p_Distributor_Id,
                       string p_Gst_Number, string p_Contact_Person, string p_Contact_Number, string p_Email_Address, string p_Customer_Code, string p_Customer_Name, string p_Address, DateTime p_RegDate, byte p_Stand, int p_PriceGroup, string p_CNIC, string p_NTN, int Designation_ID, int Employee_ID
            //,int p_TERM_AMOUNT
            )
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertCUSTOMER mCustomer = new spInsertCUSTOMER();
                mCustomer.Connection = mConnection;

                mCustomer.CUSTOMER_ID = p_Customer_Id;
                mCustomer.ADDRESS = p_Address;
                mCustomer.AREA_ID = p_Area_Id;
                mCustomer.BUSINESS_TYPE_ID = p_Business_Type_Id;
                mCustomer.CHANNEL_TYPE_ID = p_Channel_Type_Id;
                mCustomer.CONTACT_NUMBER = p_Contact_Number;
                mCustomer.CONTACT_PERSON = p_Contact_Person;
                mCustomer.CUSTOMER_CODE = p_Customer_Code;
                mCustomer.CUSTOMER_NAME = p_Customer_Name;
                mCustomer.EMAIL_ADDRESS = p_Email_Address;
                mCustomer.CNIC = p_CNIC;
                mCustomer.GST_NUMBER = p_Gst_Number;
                mCustomer.NTN = p_NTN;
                mCustomer.IS_GST_REGISTERED = p_Is_Gst_Registered;
                mCustomer.TOWN_ID = p_Town_Id;
                mCustomer.PROMOTION_CLASS  = p_Volume_Class_Id;
                mCustomer.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustomer.TOWN_ID = p_Town_Id;
                mCustomer.ROUTE_ID = p_Route_Id;
                mCustomer.IS_ACTIVE = p_Is_Active;
                mCustomer.TIME_STAMP = System.DateTime.Now;
                mCustomer.LASTUPDATE_DATE = System.DateTime.Now;
                mCustomer.REGDATE = p_RegDate;
                mCustomer.IS_COOLER = p_PriceGroup;
                mCustomer.IS_STAND = p_Stand;
                mCustomer.DESIGNATION_ID = Designation_ID;
                mCustomer.EMPLOYEE_ID = Employee_ID;
                mCustomer.ExecuteQuery();
                return "Record Updated Customer Code " + p_Customer_Code;

             

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return "Some Error Update Record";
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
        /// Inserts Or Updates Customer Tagging
        /// </summary>
        /// <remarks>
        /// Returns True On Success And False On Failure
        /// </remarks>
        /// <param name="p_saleInvoiceId">Invoice</param>
        /// <param name="p_CreditType">Type</param>
        /// <param name="p_CreditAmount">Amount</param>
        /// <param name="p_Remarks">Remarks</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdateCustomerTaging(long p_saleInvoiceId,int p_CreditType,decimal p_CreditAmount,string p_Remarks)
        {

            IDbConnection mConnection = null;
            try
            {
                spInsertCUSTOMER_CREDITTAGING mCustData = new spInsertCUSTOMER_CREDITTAGING();
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mCustData.Connection = mConnection;
                mCustData.SALE_INVOICE_ID = p_saleInvoiceId;
                mCustData.CREDIT_TYPE = p_CreditType;
                mCustData.CREDIT_AMOUNT = p_CreditAmount;
                mCustData.REMARKS = p_Remarks;  
                return mCustData.ExecuteQuery();
                
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
        
        /// <summary>
        /// Updates Customer
        /// </summary>
        /// <remarks>
        /// Returns "Record Updated Customer Code " + Inserted Customer ID On Success And "Some Error Update Record" On Failure
        /// </remarks>
        /// <param name="p_Customer_Id">Customer</param>
        /// <param name="p_Is_Gst_Registered">GSTReg</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="p_Channel_Type_Id">Channel</param>
        /// <param name="p_Volume_Class_Id">Class</param>
        /// <param name="p_Business_Type_Id">BusinessType</param>
        /// <param name="p_Area_Id">Market</param>
        /// <param name="p_Route_Id">Route</param>
        /// <param name="p_Town_Id">Town</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Gst_Number">GSTNo</param>
        /// <param name="p_Contact_Person">ContactPerson</param>
        /// <param name="p_Contact_Number">ContactNo</param>
        /// <param name="p_Email_Address">Email</param>
        /// <param name="p_Customer_Code">Code</param>
        /// <param name="p_Customer_Name">Name</param>
        /// <param name="p_Address">Address</param>
        /// <param name="p_RegDate">RegDate</param>
        /// <param name="p_Stand">Stand</param>
        /// <param name="p_Cooler">Cooler</param>
        /// <param name="p_CNIC">CNIC</param>
        /// <param name="p_NTN">NTN</param>
        /// <returns>"Record Updated Customer Code " + Inserted Customer ID On Success And "Some Error Update Record" On Failure</returns>
        public string UpdateCustomer(long p_Customer_Id, bool p_Is_Gst_Registered, bool p_Is_Active,
                       int p_Channel_Type_Id, int p_Volume_Class_Id, int p_Business_Type_Id,int p_Area_Id, int p_Route_Id, int p_Town_Id, int p_Distributor_Id,string p_Gst_Number, string p_Contact_Person, string p_Contact_Number, string p_Email_Address,
                       string p_Customer_Code, string p_Customer_Name, string p_Address, DateTime p_RegDate,byte p_Stand,int p_PriceGroup,string p_CNIC, string p_NTN, int Designation_ID, int Employee_ID
            //, int p_TERM_AMOUNT
            )
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCUSTOMER mCustomer = new spUpdateCUSTOMER();
                mCustomer.Connection = mConnection;

                mCustomer.CUSTOMER_ID = p_Customer_Id;
                mCustomer.ADDRESS = p_Address;
                mCustomer.AREA_ID = p_Area_Id;
                mCustomer.BUSINESS_TYPE_ID  = p_Business_Type_Id;
                mCustomer.CHANNEL_TYPE_ID = p_Channel_Type_Id;
                mCustomer.CONTACT_NUMBER = p_Contact_Number;
                mCustomer.CONTACT_PERSON = p_Contact_Person;
                mCustomer.CUSTOMER_CODE = p_Customer_Code;
                mCustomer.CUSTOMER_NAME = p_Customer_Name;
                mCustomer.EMAIL_ADDRESS = p_Email_Address;
                mCustomer.CNIC = p_CNIC;
                mCustomer.GST_NUMBER = p_Gst_Number;
                mCustomer.NTN = p_NTN;
                mCustomer.REGDATE = p_RegDate;
                mCustomer.IS_GST_REGISTERED = p_Is_Gst_Registered;
                mCustomer.TOWN_ID = p_Town_Id;
                mCustomer.AREA_ID = p_Area_Id;
                mCustomer.ROUTE_ID = p_Route_Id;
                mCustomer.PROMOTION_CLASS = p_Volume_Class_Id;
                mCustomer.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustomer.IS_ACTIVE = p_Is_Active;
                mCustomer.TIME_STAMP = System.DateTime.Now;
                mCustomer.LASTUPDATE_DATE = System.DateTime.Now;
                mCustomer.IS_COOLER = p_PriceGroup;
                mCustomer.IS_STAND = p_Stand;
                mCustomer.DESIGNATION_ID = Designation_ID;
                mCustomer.EMPLOYEE_ID = Employee_ID;
                mCustomer.ExecuteQuery();
                return "Record Updated Customer Code " + p_Customer_Code;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return "Some error Update Record";
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
        /// Inserts Customer From Excel File
        /// </summary>
        /// <remarks>
        /// Returns True On Success And False On Failure
        /// </remarks>
        /// <param name="p_TownId">Town</param>
        /// <param name="p_DistributorId">Location</param>
        /// <param name="pFileName">ExcelFile</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool ImportCustomer(int p_TownId, int p_DistributorId, string pFileName)
        {
            IDbConnection mConnection = null;
            FileStream Sourcefile = null;
            StreamReader ReadSourceFile = null;
            IDbTransaction mTransaction = null;

            mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
            mConnection.Open();
            mTransaction = ProviderFactory.GetTransaction(mConnection);

            Sourcefile = new FileStream(pFileName, FileMode.Open);
            ReadSourceFile = new StreamReader(Sourcefile);
            string FileContents = "";
            try
            {
                while ((FileContents = ReadSourceFile.ReadLine()) != null)
                {

                    string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                    spInsertCUSTOMER mDistRoute = new spInsertCUSTOMER();
                    mDistRoute.Connection = mConnection;
                    mDistRoute.Transaction = mTransaction;
                    mDistRoute.CUSTOMER_ID = long.Parse(ParametersArr[0].ToString());
                    mDistRoute.DISTRIBUTOR_ID = p_DistributorId;
                    mDistRoute.CUSTOMER_CODE = ParametersArr[1].ToString();
                    mDistRoute.CUSTOMER_NAME = ParametersArr[2].ToString();
                    mDistRoute.ADDRESS = ParametersArr[3].ToString();
                    mDistRoute.CONTACT_PERSON = ParametersArr[4].ToString();
                    mDistRoute.CONTACT_NUMBER = ParametersArr[5].ToString();
                    mDistRoute.IS_GST_REGISTERED = false;
                    mDistRoute.GST_NUMBER = null;
                    mDistRoute.IS_ACTIVE = true;
                    mDistRoute.REGDATE = System.DateTime.Now;
                    mDistRoute.TIME_STAMP = System.DateTime.Now;
                    mDistRoute.LASTUPDATE_DATE = System.DateTime.Now;
                    mDistRoute.TOWN_ID = p_TownId;
                    mDistRoute.AREA_ID = int.Parse(ParametersArr[6].ToString());
                    mDistRoute.ROUTE_ID = int.Parse(ParametersArr[7].ToString());
                    mDistRoute.CHANNEL_TYPE_ID = int.Parse(ParametersArr[8].ToString());
                    mDistRoute.BUSINESS_TYPE_ID = int.Parse(ParametersArr[9].ToString());
                    mDistRoute.PROMOTION_CLASS = int.Parse(ParametersArr[10].ToString());
                    mDistRoute.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;


            }

            catch (Exception excp)
            {
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                ExceptionPublisher.PublishException(excp);
                return false;

            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();

            }

        }


        public int AccountHeadMapping(long p_Customer_Id,int p_Distributor_Id,int p_principal_Id, int p_Account_ID,int p_Type_ID, int p_User_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertACCOUNT_HEAD_MAPPING mCustomer = new spInsertACCOUNT_HEAD_MAPPING();
                mCustomer.Connection = mConnection;

                mCustomer.CUSTOMER_ID = p_Customer_Id;
                mCustomer.DISTRIBUTOR_ID = p_Distributor_Id;
                mCustomer.PRINCIPAL_ID = p_principal_Id;
                mCustomer.ACCOUNT_HEAD_ID = p_Account_ID;
                mCustomer.TYPE_ID = p_Type_ID;
                mCustomer.USER_ID = p_User_ID;
                mCustomer.TIME_STAMP = System.DateTime.Now;
                mCustomer.LAST_UPDATED = System.DateTime.Now;

                var x = mCustomer.ExecuteQuery();
                return x;



            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return -1;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable SelectAccountHeadMapping(int Type_ID, int p_Distributor_Id, int p_Principal_ID, long p_Customer_ID, int p_Account_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectACCOUNT_HEAD_MAPPINGDetail ObjPrint = new spSelectACCOUNT_HEAD_MAPPINGDetail();
                SAMSBusinessLayer.Reports.dsSalesPurchaseRegister ds = new SAMSBusinessLayer.Reports.dsSalesPurchaseRegister();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_Id;
                ObjPrint.TYPE_ID = Type_ID;
                ObjPrint.PRINCIPAL_ID = p_Principal_ID;
                ObjPrint.CUSTOMER_ID = p_Customer_ID;
                ObjPrint.ACCOUNT_HEAD_ID = p_Account_ID;

                DataTable dt = ObjPrint.ExecuteTable();
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

        public DataTable SelectMappingSetting(int Type_ID, int p_Distributor_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectMAPPINGSETTING ObjPrint = new spSelectMAPPINGSETTING();
                ObjPrint.Connection = mConnection;
                ObjPrint.DISTRIBUTOR_ID = p_Distributor_Id;
                ObjPrint.TYPE_ID = Type_ID;
                DataTable dt = ObjPrint.ExecuteTable();
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

        #endregion
    }
}
