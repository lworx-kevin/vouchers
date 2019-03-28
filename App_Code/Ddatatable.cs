﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;
using System.Reflection;
using Microsoft.ApplicationBlocks.Data;
using Dapper;
using System.Configuration;
using System.Data.SqlTypes;

/// <summary>
/// Summary description for Ddatatable
/// </summary>
public class Ddatatable
{
    static IDbConnection db;

	public Ddatatable()
	{
        

		//
		// TODO: Add constructor logic here
		//
	}

    public static string getJSON(dynamic Data)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(Data);
        return JSONString;
    }

    #region Coupon Types

    public static string InsertCouponTypes(Coupons.CouponType objType)
    {

        
         #region Audit
            Audit objAudit = new Audit();
            objAudit.User = -1;
            objAudit.Function = "InsertCouponTypes";
            objAudit.TimeStamp = DateTime.Now;
        #endregion
         try
        {
            var result = "[]";
            if (objType.Id > 0 && objType.Action.ToString().ToLower() == "edit")
            {
               

                string EditCouponType = DBStrings.EditCouponType;
                result = db.Query<String>(EditCouponType, new
                {
                    Code = objType.Code,
                    Description = objType.Description,
                    Id = objType.Id
                }).First();
                objAudit.Text = "Existing coupon type has been edited by user. Edited CouponTypeId="+objType.Id;
            }
            else if (objType.Id > 0 && objType.Action.ToString().ToLower() == "validate")
            {
                string ValidateCouponType = DBStrings.ValidateCouponType;
                 result = db.Query<String>(ValidateCouponType, new
                {
                    Status = objType.Status,
                    Id = objType.Id
                }).First();
                 objAudit.Text = "Existing coupon type has been validated by user. Validated CouponTypeId=" + objType.Id;

            }
             else
            {
                string InsertCouponType = DBStrings.InsertCouponType;
                result = db.Query<String>(InsertCouponType, new
                {
                   Code =  objType.Code,
                   Description =  objType.Description,
                   Status =  objType.Status,
                  Created =   DateTime.Now
                }).First();
                objAudit.Text = "New coupon type has been added by user";

            }
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON(result);
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }

    }

    public static string GetCouponTypes()
    {
        try
        {
            Initialize();
            String GetCouponType = DBStrings.GetCouponType;
            var data = (List<Coupons.CouponType>)db.Query<Coupons.CouponType>(GetCouponType);
            return getJSON(data);
        }
        catch(Exception ex)
        {
            return getJSON("Failure");

        }

    }

    public static string GetCouponTypeById(Int32 TypeId)
    {
        try
        {
            String GetCouponTypeById = DBStrings.GetCouponTypeById;
            var data = (List<Coupons.CouponType>)db.Query<Coupons.CouponType>(GetCouponTypeById, new { TypeId = TypeId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    public static string DeleteCouponTypeById(Int32 TypeId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteCouponTypeById";
        objAudit.Text = "Existing Coupon Type has been deleted by user.Deleted TypeId= "+ TypeId;
        #endregion

        try
        {
            String DeleteCouponTypeById = DBStrings.DeleteCouponTypeById;
            db.Query<String>(DeleteCouponTypeById, new { TypeId = TypeId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }
    }

    #endregion



    #region Coupon Value

    public static string InsertCouponValue(Coupons.CouponValue objValue)
    {


        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "InsertCouponValue";
        objAudit.TimeStamp = DateTime.Now;
        #endregion
        try
        {
            var result = "[]";
            if (objValue.Id > 0 && objValue.Action.ToString().ToLower() == "edit")
            {


                string EditCouponValue = DBStrings.EditCouponValue;
                result = db.Query<String>(EditCouponValue, new
                {
                    Label = objValue.Label,
                    Value = objValue.Value,
                    Id = objValue.Id
                }).First();
                objAudit.Text = "Existing coupon value has been edited by user. Edited CouponValueId=" + objValue.Id;
            }
            else if (objValue.Id > 0 && objValue.Action.ToString().ToLower() == "validate")
            {
                string ValidateCouponValue = DBStrings.ValidateCouponValue;
                result = db.Query<String>(ValidateCouponValue, new
                {
                    Status = objValue.Status,
                    Id = objValue.Id
                }).First();
                objAudit.Text = "Existing coupon value has been validated by user. Validated CouponValueId=" + objValue.Id;

            }
            else
            {
                string InsertCouponValue = DBStrings.InsertCouponValue;
                result = db.Query<String>(InsertCouponValue, new
                {
                    Label = objValue.Label,
                    Value = objValue.Value,
                    Status = objValue.Status,
                    Created = DateTime.Now
                }).First();
                objAudit.Text = "New coupon type has been added by user";

            }
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON(result);
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }

    }

    public static string GetCouponValue()
    {
        try
        {
            Initialize();
            String GetCouponValue= DBStrings.GetCouponValue;
            var data = (List<Coupons.CouponValue>)db.Query<Coupons.CouponValue>(GetCouponValue);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }

    }

    public static string GetCouponValueById(Int32 ValueId)
    {
        try
        {
            String GetCouponValueById = DBStrings.GetCouponValueById;
            var data = (List<Coupons.CouponValue>)db.Query<Coupons.CouponValue>(GetCouponValueById, new { ValueId = ValueId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    public static string DeleteCouponValueById(Int32 ValueId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteCouponValueById";
        objAudit.Text = "Existing Coupon Type has been deleted by user.Deleted ValueId= " + ValueId;
        #endregion

        try
        {
            String DeleteCouponValueById = DBStrings.DeleteCouponValueById;
            db.Query<String>(DeleteCouponValueById, new { ValueId = ValueId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }
    }

    #endregion

    #region CouponFunding

    //Get Coupon Funding Data
    //Created on 06/03/2017
    public static string GetCouponFundingData()
    {
        try
        {
            Initialize();
            String GetCouponFundingData = DBStrings.GetCouponFundingData;
            var data = (List<Coupons.CouponFunding>)db.Query<Coupons.CouponFunding>(GetCouponFundingData);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    // Get Coupon Funding Data by Id
    //Created on 06/03/2017
    public static string GetCouponFundingDataById(Int32 FundId)
    {
        try
        {
            String GetCouponFundingDataById = DBStrings.GetCouponFundingDataById;
            var data = (List<Coupons.CouponFunding>)db.Query<Coupons.CouponFunding>(GetCouponFundingDataById, new { FundId = FundId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    //Delete Coupon Funding Record
    //Created on 06/03/2017
    public static string DeleteCouponFundingDataById(Int32 FundId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteCouponFundingDataById";
        objAudit.Text = "Existing Coupon Funding data has been deleted by user.Deleted FundId= " + FundId;
        #endregion

        try
        {
            String DeleteCouponFundingDataById = DBStrings.DeleteCouponFundingDataById;
            db.Query<String>(DeleteCouponFundingDataById, new { FundId = FundId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }
    }

    public static string InsertCouponFundingData(Coupons.CouponFunding objFund)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "InsertCouponFundingData";
        objAudit.TimeStamp = DateTime.Now;
        #endregion
        try
        {
            var result = "[]";
            if (objFund.Id > 0 && objFund.Action.ToString().ToLower() == "edit")
            {


                string EditCouponFundingData = DBStrings.EditCouponFundingData;
                result = db.Query<String>(EditCouponFundingData, new
                {
                    GlCode = objFund.GlCode,
                    Department = objFund.Department,
                    Status = objFund.Status,
                    Id = objFund.Id
                }).First();
                objAudit.Text = "Existing coupon funding has been edited by user. Edited CouponFundId=" + objFund.Id;
            }
            else if (objFund.Id > 0 && objFund.Action.ToString().ToLower() == "validate")
            {
              
                string ValidateCouponFundingData = DBStrings.ValidateCouponFundingData;
                result = db.Query<String>(ValidateCouponFundingData, new
                {
                    Status = objFund.Status,
                    Id = objFund.Id
                }).First();
                objAudit.Text = "Existing coupon funding has been validated by user. Validated CouponFundId=" + objFund.Id;

            }
            else
            {
                string InsertCouponFundingData = DBStrings.InsertCouponFundingData;
                result = db.Query<String>(InsertCouponFundingData, new
                {
                    GlCode = objFund.GlCode,
                    Department = objFund.Department,
                    Status = objFund.Status,
                    Created = DateTime.Now
                }).First();
                objAudit.Text = "New coupon Funding has been added by user";

            }
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON(result);
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }
    }

    #endregion

    #region Coupon category
    public static string GetCouponCategories()
    {
        try
        {
            Initialize();
            String GetAllCouponCategories = DBStrings.GetAllCouponCategories;
            var data = (List<Coupons.CouponCategory>)db.Query<Coupons.CouponCategory>(GetAllCouponCategories);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }


    public static string GetCouponCategoryById(Int32 CatId)
    {
        try
        {
            String GetCouponCategoryById = DBStrings.GetCouponCategoryById;
            var data = (List<Coupons.CouponCategory>)db.Query<Coupons.CouponCategory>(GetCouponCategoryById, new { CatId = CatId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }
    public static string InsertCouponCategory(Coupons.CouponCategory objCat)
    {


        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "InsertCouponCategory";
        objAudit.TimeStamp = DateTime.Now;
        #endregion
        try
        {
            var result = "[]";
            if (objCat.Id > 0 && objCat.Action.ToString().ToLower() == "edit")
            {


                string EditCouponCategory = DBStrings.EditCouponCategory;
                result = db.Query<String>(EditCouponCategory, new
                {
                    Category = objCat.Category.ToUpper(),
                    CouponPrefix = objCat.CouponPrefix,
                    Description = objCat.Description,
                    Id = objCat.Id,
                    PayC = objCat.PayC
                }).First();
                objAudit.Text = "Existing coupon category has been edited by user. Edited CouponCategoryId=" + objCat.Id;
            }
            else if (objCat.Id > 0 && objCat.Action.ToString().ToLower() == "validate")
            {
                string ValidateCouponCategory = DBStrings.ValidateCouponCategory;
                result = db.Query<String>(ValidateCouponCategory, new
                {
                    Status = objCat.Status,
                    Id = objCat.Id
                }).First();
                objAudit.Text = "Existing coupon category has been validated by user. Validated CouponCategoryId=" + objCat.Id;

            }
            else
            {
                string InsertCouponCategory = DBStrings.InsertCouponCategory;
                result = db.Query<String>(InsertCouponCategory, new
                {
                    Category = objCat.Category.ToUpper(),
                    CouponPrefix = objCat.CouponPrefix,
                    Description = objCat.Description,

                    Status = objCat.Status,
                    Created = DateTime.Now,
                    PayC = objCat.PayC
                }).First();
                objAudit.Text = "New coupon category has been added by user";

            }
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON(result);
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }

    }

    public static string DeleteCouponCatById(Int32 CatId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteCouponCategoryById";
        objAudit.Text = "Existing coupon category has been deleted by user.Deleted TypeId= " + CatId;
        #endregion

        try
        {
            String DeleteCouponCategoryById = DBStrings.DeleteCouponCategoryById;
            db.Query<String>(DeleteCouponCategoryById, new { CatId = CatId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }

    }

    #endregion

    #region CouponProduct

    //Get Coupon Funding Data
    //Created on 08/03/2017
    public static string GetCouponProductData()
    {
        try
        {
            Initialize();
            String GetAllProducts = DBStrings.GetAllProducts;
            var data = (List<Coupons.CouponProduct>)db.Query<Coupons.CouponProduct>(GetAllProducts);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    // Get Coupon Funding Data by Id
    //Created on 06/03/2017
    public static string GetCouponProductDataById(Int32 ProductId)
    {
        try
        {
            String GetCouponProductById = DBStrings.GetCouponProductById;
            var data = (List<Coupons.CouponProduct>)db.Query<Coupons.CouponProduct>(GetCouponProductById, new { ProductId = ProductId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    //Delete Coupon Funding Record
    //Created on 06/03/2017
    public static string DeleteCouponProductDataById(Int32 ProductId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteCouponProductDataById";
        objAudit.Text = "Existing Coupon Type has been deleted by user.Deleted TypeId= " + ProductId;
        #endregion

        try
        {
            String DeleteCouponProductDataById = DBStrings.DeleteCouponProductDataById;
            db.Query<String>(DeleteCouponProductDataById, new { ProductId = ProductId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }
    }

    public static string InsertCouponProductData(Coupons.CouponProduct objProduct)
    {

        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "InsertCouponProductData";
        objAudit.TimeStamp = DateTime.Now;
        #endregion
        try
        {
            var result = "[]";
            if (objProduct.Id > 0 && objProduct.Action.ToString().ToLower() == "edit")
            {


                string EditCouponProduct = DBStrings.EditCouponProduct;
                result = db.Query<String>(EditCouponProduct, new
                {
                    ProductName = objProduct.ProductName,
                    ProductCode = objProduct.ProductCode,
                    ProductType = objProduct.ProductType,
                    Id = objProduct.Id
                }).First();
                objAudit.Text = "Existing coupon product has been edited by user. Edited CouponProductId=" + objProduct.Id;
            }
            else if (objProduct.Id > 0 && objProduct.Action.ToString().ToLower() == "validate")
            {
                string ValidateCouponProduct = DBStrings.ValidateCouponProduct;
                result = db.Query<String>(ValidateCouponProduct, new
                {
                    Status = objProduct.Status,
                    Id = objProduct.Id
                }).First();
                objAudit.Text = "Existing coupon product has been validated by user. Validated CouponProductId=" + objProduct.Id;

            }
            else
            {
                string InsertCouponProduct = DBStrings.InsertCouponProduct;
                result = db.Query<String>(InsertCouponProduct, new
                {
                    ProductName = objProduct.ProductName,
                    ProductCode = objProduct.ProductCode,
                    ProductType = objProduct.ProductType,
                    Status = objProduct.Status,
                    Created = DateTime.Now
                }).First();
                objAudit.Text = "New coupon product has been added by user";

            }
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON(result);
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }

    }

    #endregion


    #region CouponProductType

    //Get Coupon Funding Data
    //Created on 08/03/2017
    public static string GetCouponProductTypeData()
    {
        try
        {
            Initialize();
            String GetAllProducts = DBStrings.GetAllProductType;
            var data = (List<Coupons.CouponProductType>)db.Query<Coupons.CouponProductType>(GetAllProducts);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    // Get Coupon Funding Data by Id
    //Created on 06/03/2017
    public static string GetCouponProductTypeDataById(Int32 ProductId)
    {
        try
        {
            Initialize();
            String GetCouponProductTypeById = DBStrings.GetCouponProductTypeById;
            var data = (List<Coupons.CouponProductType>)db.Query<Coupons.CouponProductType>(GetCouponProductTypeById, new { ProductId = ProductId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    //Delete Coupon Funding Record
    //Created on 06/03/2017
    public static string DeleteCouponProductTypeDataById(Int32 ProductId)
    {
        Initialize();

        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteCouponProductTypeDataById";
        objAudit.Text = "Existing Coupon Product Type has been deleted by user.Deleted TypeId= " + ProductId;
        #endregion

        try
        {
            String DeleteCouponProductTypeDataById = DBStrings.DeleteCouponProductTypeDataById;
            db.Query<String>(DeleteCouponProductTypeDataById, new { ProductId = ProductId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }
    }

    public static string InsertCouponProductTypeData(Coupons.CouponProductType objProduct)
    {
        Initialize();
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "InsertCouponProductTypeData";
        objAudit.TimeStamp = DateTime.Now;
        #endregion
        try
        {
            var result = "[]";
            if (objProduct.Id > 0 && objProduct.Action.ToString().ToLower() == "edit")
            {


                string EditCouponProductType = DBStrings.EditCouponProductType;
                result = db.Query<String>(EditCouponProductType, new
                {

                                              ProductCode= objProduct.ProductCode,
                        ProductType=  objProduct.ProductType,
                        dbUser= objProduct.dbUser,
                       dbPwd= objProduct.dbPwd,
                        dbName= objProduct.dbName,
                        dbTable= objProduct.dbTable,
                        dbProductCode= objProduct.dbProductCode,
                        dbProductName=objProduct.dbProductName,
                        dbHotelName= objProduct.dbHotelName,
                        dbHotelChain= objProduct.dbHotelChain,
                        dbFlightNumber= objProduct.dbFlightNumber,
                        dbAirlineName= objProduct.dbAirlineName,
                        dbCountryCode= objProduct.dbCountryCode,
                        dbAirportCode= objProduct.dbAirportCode,
                        dbStatusCode= objProduct.dbStatusCode,
                                              DBStatusValue = objProduct.dbStatusValue,
                            Status= objProduct.Status,
                            Id= objProduct.Id,
                            Action= objProduct.Action
                }).First();
                objAudit.Text = "Existing coupon product type has been edited by user. Edited CouponProductTypeId=" + objProduct.Id;
            }
            else if (objProduct.Id > 0 && objProduct.Action.ToString().ToLower() == "validate")
            {
                string ValidateCouponProductType = DBStrings.ValidateCouponProductType;
                result = db.Query<String>(ValidateCouponProductType, new
                {
                    Status = objProduct.Status,
                    Id = objProduct.Id
                }).First();
                objAudit.Text = "Existing coupon product type has been validated by user. Validated CouponProductTypeId=" + objProduct.Id;

            }
            else
            {
                string InsertCouponProductType = DBStrings.InsertCouponProductType;
                result = db.Query<String>(InsertCouponProductType, new
                {
                    ProductCode = objProduct.ProductCode,
                    ProductType = objProduct.ProductType,
                    dbUser = objProduct.dbUser,
                    dbPwd = objProduct.dbPwd,
                    dbName = objProduct.dbName,
                    dbTable = objProduct.dbTable,
                    dbProductCode = objProduct.dbProductCode,
                    dbProductName = objProduct.dbProductName,
                    dbHotelName = objProduct.dbHotelName,
                    dbHotelChain = objProduct.dbHotelChain,
                    dbFlightNumber = objProduct.dbFlightNumber,
                    dbAirlineName = objProduct.dbAirlineName,
                    dbCountryCode = objProduct.dbCountryCode,
                    dbAirportCode = objProduct.dbAirportCode,
                    dbStatusCode = objProduct.dbStatusCode,
                    DBStatusValue = objProduct.dbStatusValue,
                    Status = objProduct.Status,
                    Id = objProduct.Id,
                    Action = objProduct.Action
                }).First();
                objAudit.Text = "New coupon product type has been added by user";

            }
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON(result);
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }

    }

    #endregion

    

    #region Coupon Brands
    public static string GetCouponBrands()
    {
        try
        {
            Initialize();
            String GetAllBrands = DBStrings.GetAllBrands;
            var data = (List<Coupons.CouponBrand>)db.Query<Coupons.CouponBrand>(GetAllBrands);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }

    }


    public static string InsertCouponBrand(Coupons.CouponBrand objBrand)
    {

        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "InsertCouponBrand";
        objAudit.TimeStamp = DateTime.Now;
        #endregion
        try
        {
            var result = "[]";
            if (objBrand.Id > 0 && objBrand.Action.ToString().ToLower() == "edit")
            {


                string EditCouponBrand = DBStrings.EditCouponBrand;
                result = db.Query<String>(EditCouponBrand, new
                {
                    BrandName = objBrand.BrandName,
                    BrandImage = objBrand.BrandImage,
                    Id = objBrand.Id
                }).First();
                objAudit.Text = "Existing coupon brand has been edited by user. Edited CouponBrandId=" + objBrand.Id;
            }
            else if (objBrand.Id > 0 && objBrand.Action.ToString().ToLower() == "validate")
            {
                string ValidateCouponBrand = DBStrings.ValidateCouponBrand;
                result = db.Query<String>(ValidateCouponBrand, new
                {
                    Status = objBrand.Status,
                    Id = objBrand.Id
                }).First();
                objAudit.Text = "Existing coupon brand has been validated by user. Validated CouponBrandId=" + objBrand.Id;

            }
            else
            {
                string InsertCouponBrand = DBStrings.InsertCouponBrand;
                result = db.Query<String>(InsertCouponBrand, new
                {
                    BrandName = objBrand.BrandName,
                    BrandImage = objBrand.BrandImage,
                    Status = objBrand.Status,
                    Created = DateTime.Now
                }).First();
                objAudit.Text = "New coupon brand has been added by user";

            }
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON(result);
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }
    }


    public static string GetCouponBrandById(Int32 BrandId)
    {
        try
        {
            String GetCouponBrandById = DBStrings.GetCouponBrandById;
            var data = (List<Coupons.CouponBrand>)db.Query<Coupons.CouponBrand>(GetCouponBrandById, new { BrandId = BrandId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    //Delete Coupon Funding Record
    //Created on 06/03/2017
    public static string DeleteCouponBrandById(Int32 BrandId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteCouponBrandById";
        objAudit.Text = "Existing Coupon Brand has been deleted by user.Deleted BrandId= " + BrandId;
        #endregion

        try
        {
            String DeleteCouponBrandDataById = DBStrings.DeleteCouponBrandDataById;
            db.Query<String>(DeleteCouponBrandDataById, new { BrandId = BrandId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            InsertAuditLog(objAudit);

            objAudit.Status = ex.Message.ToString();

            return getJSON("Failure");

        }
    }

    #endregion

    #region Vouchers
    public static string GetVouchers()
    {
        Initialize();
        String GetVoucher = DBStrings.GetVoucherGrid;
        var data = (List<Voucher>)db.Query<Voucher>(GetVoucher);
        return getJSON(data);
    }



    public static string InsertVoucher(Voucher objData)
    {
        #region Audit
            Audit objAudit = new Audit();
            objAudit.User = -1;
            objAudit.Function = "InsertVoucher";
            objAudit.Text = "New voucher has been added by user";
            objAudit.TimeStamp = DateTime.Now;
        #endregion
         try
        {

            if (objData.Id > 0 && objData.Action.ToString().ToLower() == "edit")
            {
                string EditVoucherData = DBStrings.EditVoucherData;
                var result = db.Execute(EditVoucherData, new
                {
                    Id = objData.Id,

                    TypeId = objData.CouponTypeId,
                    CampaignId = objData.CampaignId,
                    FundingId = objData.CouponFundingId,

                    CategoryId = objData.CouponCatId,
                    BrandId = objData.CouponBrandId,
                  
                    objData.FirstName,
                    objData.LastName,
                    objData.VoucherAmount,
                    objData.IncProduct,
                    objData.ExclProduct,
                   
                    objData.ExpiryDate,
                    objData.Status
                    
                    
                });
                objAudit.Text = "Existing voucher  has been edited by user. Edited VoucherId=" + objData.Id;

            }
              
            else if (objData.Id > 0 && objData.Action.ToString().ToLower() == "validate")
            {
                string ValidateVoucherData = DBStrings.ValidateVoucherData;
                var result = db.Execute(ValidateVoucherData, new
                {
                    objData.Status,
                    Id = objData.Id,
                });
                objAudit.Text = "Existing voucher has been approved by user. Edited VoucherId=" + objData.Id;

            }
            else
            {

                string insertVoucherData = DBStrings.insertVoucherData;
                #region Get Campaign Category Prefix
                String GetCategoryPrefix = DBStrings.GetCategoryPrefix;
                String GetCategoryPrefixByCategoryId = DBStrings.GetCategoryPrefixByCategoryId;
                String CouponPrefix = "";

                CouponPrefix = db.Query<String>(GetCategoryPrefixByCategoryId, new
            {
                CategoryId = objData.CouponCatId
            }).Single();

                #endregion

                #region Coupon Types
                string GetTypeDescById = DBStrings.GetCouponTypeById;
                string TypeDesc = db.Query<String>(GetTypeDescById, new
                {
                    TypeId = objData.CouponTypeId
                }).Single();
                #endregion

                #region Coupon Prefix for Code
                String NewPrefix = CouponPrefix.Trim();

                #endregion

                #region Pad Camapign Id with zero
                Int32 Length = Convert.ToInt32(objData.CampaignId.ToString().Length);
                Int32 DigitToAppend = 5;
                String PCampId;
                if (Length != 0)
                {
                    PCampId = objData.CampaignId.ToString();
                }
                else
                {
                    PCampId = "00000";
                }
                while (Length < DigitToAppend)
                {
                    PCampId = "0" + PCampId;
                    Length++;
                }
                #endregion

                #region checkifcodeexists
                String NewVoucherCode = "";
                String Exist = "0";

                while (NewVoucherCode == "")
                {
                    String GetVoucher = DBStrings.GetVoucherCode;
                    //Exist = db.Query<String>(GetVoucher, new { NewVoucherCode = NewVoucherCode }).DefaultIfEmpty().ToString();
                    Boolean NotExist = true;
                    while (NotExist)
                    {
                        byte[] gb = Guid.NewGuid().ToByteArray();


                        long l = 1;
                        foreach (byte b in Guid.NewGuid().ToByteArray())
                        {
                            l *= ((int)b + 1);
                        }


                        //NewVoucherCode = TypeDesc + NewPrefix + PCampId + l.ToString().Replace("-", "").Substring(1, 6);
                        //NewVoucherCode = TypeDesc + NewPrefix + PCampId + l.ToString().Replace("-", "").Substring(1, 6);
                        String RandomNumb = l.ToString().Replace("-", "").Substring(1, 8);
                        Int32 INPrefix; Int32 IRandomNum;
                        Int32.TryParse(NewPrefix, out INPrefix);
                        Int32.TryParse(RandomNumb, out IRandomNum);
                        String DigitCode = (((INPrefix + IRandomNum) / INPrefix) % 100).ToString();
                        NewVoucherCode = NewPrefix + RandomNumb + DigitCode; 

                        String InsertVoucher = DBStrings.InsertVoucher;
                        String data = db.Query<String>(InsertVoucher, new { NewVoucherCode = NewVoucherCode }).Single();
                        NotExist = data == "Repeat" ? true : false;
                    }
                }
                #endregion




                DateTime Created = DateTime.Now;
                DateTime IssueDate = DateTime.Now;

                var result = db.Execute(insertVoucherData, new
                {
                    CampaignId = objData.CampaignId,
                    TypeId = objData.CouponTypeId
                          ,
                    FundingId = objData.CouponFundingId
                          ,
                    CategoryId = objData.CouponCatId
                          ,
                    BrandId = objData.CouponBrandId
                    ,
                    NewVoucherCode,
                    objData.FirstName,
                    objData.LastName,
                    objData.VoucherAmount,
                    objData.IncProduct,
                    objData.ExclProduct,
                    IssueDate,
                    objData.ExpiryDate,
                    objData.Status,
                    Created
                });

            }




            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON("Success");
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }
    }

    public static string GetVoucherById(Int32 VoucherId)
    {
        try
        {
            String GetVoucherById = DBStrings.GetVoucherCodeById;
            var data = (List<Voucher>)db.Query<Voucher>(GetVoucherById, new { VoucherId = VoucherId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }

    }
    public static string DeleteVoucherbyId(Int32 VoucherId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteVoucherbyId";
        objAudit.Text = "Existing voucher has been deleted by user. Deleted VoucherID=" + VoucherId;
        #endregion

        try
        {
            String DeleteVoucher = DBStrings.DeleteVoucher;
            db.Query<String>(DeleteVoucher, new { VoucherId = VoucherId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }


    }


    //Get Compaign data to bind dropdown
    public static string GetVoucherCampaignData()
    {
        db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        String GetVoucherCampaignData = DBStrings.GetVoucherCampaignData;
        var data = (List<Voucher>)db.Query<Voucher>(GetVoucherCampaignData);
        return getJSON(data);
    }

    #endregion

    #region Vouchers With Campaign
    public static string GetVCouponsListing( Int32 Id)
    {
        Initialize();
        String GetVCouponsListing = DBStrings.GetVCouponsListing;
        var data = (List<CouponGeneration>)db.Query<CouponGeneration>(GetVCouponsListing, new { Id = Id });
        return getJSON(data);
    }


    public static string GetVouchersWithCampaign()
    {
        Initialize();
        String GetVoucher = DBStrings.GetVoucherGenerationdata;
        var data = (List<GenerateCoupon>)db.Query<GenerateCoupon>(GetVoucher);
        return getJSON(data);
    }
    public static string GetVouchersCampaignById(Int32 Id)
    {
        Initialize();
        String GetVoucher = DBStrings.GetVoucherGenerationdataById;
        var data = (List<GenerateCoupon>)db.Query<GenerateCoupon>(GetVoucher, new { Id = Id });
        return getJSON(data);
    }



    public static string InsertVoucherWithCampaign(VOCampaign objVData, List<GridDataArray> objGridData)
    {
        String VoucherId = "0";
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "InsertVoucherWithCampaign";
        objAudit.Text = "New voucher has been added by user";
        objAudit.TimeStamp = DateTime.Now;
        #endregion

        try
        {

            if (objVData.Id > 0 && objVData.Action.ToString().ToLower() == "validate")
            {
                string ValidateVoucherGen = DBStrings.ValidateVoucherGen;
                db.Query<String>(ValidateVoucherGen, new
                {

                    Id = objVData.Id

                });
                objAudit.Text = "Existing coupon brand has been edited by user. Edited CouponBrandId=" + objVData.Id;
            }
            else
            {
                #region Insert Voucher Geneartion Data
                String insertVoucherGenerationData = DBStrings.insertVoucherGenerationData;

                var ReturnGenId = db.Query<String>(insertVoucherGenerationData, new
                {
                    CampaignId = objVData.Campaign,
                    Distribution = objVData.Distribution,
                    Status = 0,
                    Coupons = objGridData.Count

                }).FirstOrDefault();
                #endregion

                foreach (GridDataArray GD in objGridData)
                {



                    try
                    {
                        string insertCouponGnerationData = DBStrings.insertCouponGnerationData;
                        #region Insert Voucher
                        NewVoucherData VResp = new NewVoucherData();
                        string ReturnTypeandCat = DBStrings.ReturnTypeandCat;




                        DateTime Created = DateTime.Now;
                        DateTime IssueDate = DateTime.Now;



                        VoucherId = db.Query<String>(insertCouponGnerationData, new
                        {
                        generation_id  = ReturnGenId,

                        campaign_id =objVData.Campaign,

                        email  = GD.Email,

                        firstname = GD.FirstName,

                        lastname =GD.LastName,

                        status = 0,
                        distribution = 0,
                        distributed =0
,
                        
                        }).FirstOrDefault();
                        #endregion





                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }










            InsertAuditLog(objAudit);
            return getJSON("Success");
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }
    }

    public static string GetVoucherWithCampaignById(Int32 VoucherId)
    {
        try
        {
            String GetVoucherWithCampaignById = DBStrings.GetVoucherWithCampaignById;
            var data = (List<VCGrid>)db.Query<VCGrid>(GetVoucherWithCampaignById, new { VoucherId = VoucherId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }

    }





    public static string SendVoucherWithCampaignbyId(Int32 GenId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "SendVoucherWithCampaignbyId";
        objAudit.Text = "Following records have been marked as ready to sent. Marked VoucherGenerationID=" + GenId;
        #endregion

        try
        {
            String SendVoucherWithCampaignbyId = DBStrings.SendVoucherWithCampaignbyId;
            db.Query<String>(SendVoucherWithCampaignbyId, new { GenerationId = GenId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }


    }


    #region Coupon Generation

    public static string DeleteCouponById(Int32 CouponId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteCouponById";
        objAudit.Text = "Existing coupon has been deleted by user. Deleted CouponID=" + CouponId;
        #endregion

        try
        {
            String DeleteCouponById = DBStrings.DeleteCouponById;
            db.Query<String>(DeleteCouponById, new { Id = CouponId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }


    }

    public static string EditGenerateCouponData(CouponGeneration  objVData)
    {
        String VoucherId = "0";
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "EditGenerateCouponData";
        objAudit.Text = "Bulk Generated Coupon has been edited by user";
        objAudit.TimeStamp = DateTime.Now;
        #endregion

        try
        {


                #region Insert Voucher Geneartion Data
            String EditGenerateCouponData = DBStrings.EditGenerateCouponData;

            var ReturnGenId = db.Query<String>(EditGenerateCouponData, new
                {
                    Id = objVData.Id,
                    FirstName = objVData.FirstName,
                    LastName = objVData.LastName,
                    Email = objVData.Email

                }).FirstOrDefault();
                #endregion

       










            InsertAuditLog(objAudit);
            return getJSON("Success");
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }
    }

    public static string GetCouponById(Int32 CouponId)
    {

        try
        {
            String GetCouponById = DBStrings.GetCouponById;
            var data = (List<CouponGeneration>)db.Query<CouponGeneration>(GetCouponById, new { Id = CouponId });

            return getJSON(data);
        }
        catch (Exception ex)
        {


            return getJSON("Failure");

        }


    }

    

    #endregion

    public static string DeleteVoucherWithCampaignbyId(Int32 GenId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteVoucherCampaignGenerationbyId";
        objAudit.Text = "Existing voucher has been deleted by user. Deleted VoucherGenerationID=" + GenId;
        #endregion

        try
        {
            String DeleteVoucherWithCampaignbyId = DBStrings.DeleteVoucherWithCampaignbyId;
            db.Query<String>(DeleteVoucherWithCampaignbyId, new { GenerationId = GenId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }


    }




    #endregion




    #region Voucher Campaign

    public static string GetCouponApprValueData()
    {
        try
        {
            Initialize();
            String GetCouponApprValueData = DBStrings.GetCouponApprValueData;
            var data = (List<Coupons.CouponValue>)db.Query<Coupons.CouponValue>(GetCouponApprValueData);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }


    public static string GetCouponApprFundingData()
    {
        try
        {
            Initialize();
            String GetCouponApprFundingData = DBStrings.GetCouponApprFundingData;
            var data = (List<Coupons.CouponFunding>)db.Query<Coupons.CouponFunding>(GetCouponApprFundingData);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }


    public static string GetCouponApprCategories()
    {
        try
        {
            Initialize();
            String GetCouponApprCategories = DBStrings.GetCouponApprCategories;
            var data = (List<Coupons.CouponCategory>)db.Query<Coupons.CouponCategory>(GetCouponApprCategories);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    public static string GetCouponApprTypes()
    {
        try
        {
            Initialize();
            String GetCouponApprTypes = DBStrings.GetCouponApprTypes;
            var data = (List<Coupons.CouponType>)db.Query<Coupons.CouponType>(GetCouponApprTypes);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }

    }

    public static string GetCouponApprBrands()
    {
        try
        {
            Initialize();
            String GetAllApprBrands = DBStrings.GetAllApprBrands;
            var data = (List<Coupons.CouponBrand>)db.Query<Coupons.CouponBrand>(GetAllApprBrands);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }

    }

    public static string GetCouponApprProductData()
    {
        try
        {
            Initialize();
            String GetApprProductData = DBStrings.GetApprProductData;
            var data = (List<Coupons.CouponProduct>)db.Query<Coupons.CouponProduct>(GetApprProductData);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }


    public static string InsertVoucherCampaign(VoucherCamp objData)
    {

        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "InsertVoucherCampaign";
        objAudit.TimeStamp = DateTime.Now;
        #endregion
        try
        {
            String Token = GenerateToken();
            var result = "[]";
            if (objData.Id > 0 && objData.Action.ToString().ToLower() == "edit")
            {


                string EditVoucherCampaignById = DBStrings.EditVoucherCampaignById;
                result = db.Query<String>(EditVoucherCampaignById, new
                {
                    TypeId = objData.CouponTypeId
                      ,
                    FundingId = objData.CouponFundingId
                      ,
                    CategoryId = objData.CouponCatId
                      ,
                    BrandId = objData.CouponBrandId
                      ,
                    CampaignName = objData.CampName
                      ,
                    incProduct = objData.InclProduct
                      ,
                    exclProduct = objData.ExclProduct
                      ,
                    CouponValue = objData.CouponValue
                      ,
                    StartDate = objData.StartDate
                      ,
                    EndDate = objData.EndDate,
                    DepartDate = objData.DepartDate
                      ,
                    ReturnDate = objData.ReturnDate,
                    Status = objData.Status,
                    Id = objData.Id,
                    htmlEnglish = objData.htmlEnglish,
                    htmlFrench = objData.htmlFrench
                }).First();
                objAudit.Text = "Existing voucher campaign has been edited by user. Edited VoucherCampaignId=" + objData.Id;
            }
            else if (objData.Id > 0 && objData.Action.ToString().ToLower() == "validate")
            {
                string ValidateVoucherCampaignById = DBStrings.ValidateVoucherCampaignById;
                result = db.Query<String>(ValidateVoucherCampaignById, new
                {
                    TypeId = objData.CouponTypeId
                      ,
                    FundingId = objData.CouponFundingId
                      ,
                    CategoryId = objData.CouponCatId
                      ,
                    BrandId = objData.CouponBrandId
                      ,
                    CampaignName = objData.CampName
                      ,
                    incProduct = objData.InclProduct
                      ,
                    exclProduct = objData.ExclProduct
                      ,
                    CouponValue = objData.CouponValue
                      ,
                    StartDate = objData.StartDate
                      ,
                    EndDate = objData.EndDate,
                    DepartDate = objData.DepartDate
                      ,
                    ReturnDate = objData.ReturnDate,

                    Status = objData.Status,
                    Id = objData.Id,
                    htmlEnglish = objData.htmlEnglish,
                    htmlFrench = objData.htmlFrench
                }).First();
                objAudit.Text = "Existing voucher campaign has been validated by user. Validated VoucherCampaignId=" + objData.Id;

            }
            else
            {
                string InsertVoucherCampaignData = DBStrings.InsertVoucherCampaignData;
                result = db.Query<String>(InsertVoucherCampaignData, new
                {
                    TypeId = objData.CouponTypeId
                      ,
                    FundingId = objData.CouponFundingId
                      ,
                    CategoryId = objData.CouponCatId
                      ,
                    BrandId = objData.CouponBrandId
                      ,
                    CampaignName = objData.CampName
                      ,
                    incProduct = objData.InclProduct
                      ,
                    exclProduct = objData.ExclProduct
                      ,
                    CouponValue = objData.CouponValue
                      ,
                    StartDate = objData.StartDate
                      ,
                    EndDate = objData.EndDate,
                    DepartDate = objData.DepartDate
                      ,
                    ReturnDate = objData.ReturnDate,

                    Status = objData.Status,

                    Created = DateTime.Now,
                    htmlEnglish = objData.htmlEnglish,
                    htmlFrench = objData.htmlFrench,
                    Token = Token
                }).First();


                objAudit.Text = "New voucher campaign has been added by user";

            }
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON(result);
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }
    }


    public static string GenerateToken()
    {
        return Guid.NewGuid().ToString();
    }

    public static string GetVoucherCampaign()
    {
        try
        {
            Initialize();
            String GetVoucherCampaign = DBStrings.GetVoucherCampaign;
            var data = (List<VoucherCamp>)db.Query<VoucherCamp>(GetVoucherCampaign);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    public static string GetCouponValueData()
    {
        try
        {
            Initialize();
            String GetCouponValueData = DBStrings.GetCouponValueData;
            var data = (List<Coupons.CouponValue>)db.Query<Coupons.CouponValue>(GetCouponValueData);
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }


    public static string GetVoucherCampaignById(Int32 CampId)
    {
        try
        {
            String GetVoucherCampaignById = DBStrings.GetVoucherCampaignById;
            var data = (List<VoucherCamp>)db.Query<VoucherCamp>(GetVoucherCampaignById, new { CampId = CampId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }
    }

    public static string DeleteVoucherCampaignbyId(Int32 CampId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteVoucherCampaignbyId";
        objAudit.Text = "Existing voucher campaign has been deleted by user.Deleted CampaignId= " + CampId;
        #endregion

        try
        {
            String DeleteVoucherCampaignById = DBStrings.DeleteVoucherCampaignById;
            db.Query<String>(DeleteVoucherCampaignById, new { CampId = CampId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }
    }


    #endregion

    #region Gift Certificates
    public static string GetCertificates()
    {
        Initialize();
        String GetCertificate = DBStrings.GetCertificateGrid;
        var data = (List<GiftCertificate>)db.Query<GiftCertificate>(GetCertificate);
        return getJSON(data);
    }

    public static string InsertCertificate(GiftCertificate objData)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "InsertCertificate";
        objAudit.Text = "New Certificate has been added by user";
        objAudit.TimeStamp = DateTime.Now;
        #endregion
        try
        {


            if (objData.Id > 0 && objData.Action.ToString().ToLower() == "edit")
            {
                string EditCertificateData = DBStrings.EditCertificateData;
                DateTime Created = DateTime.Now;
                DateTime IssueDate = DateTime.Now;

                var result = db.Execute(EditCertificateData, new
                {
                    Id = objData.Id,
                    TypeId = objData.CouponTypeId,
                    CategoryId = objData.CouponCatId,
                    objData.CampaignId,
                    objData.FirstName,
                    objData.LastName,
                    objData.CertificateAmount,
                    objData.CertificateValue,
                    IncProduct = objData.InclProduct,
                    objData.ExclProduct,
                    objData.ExpiryDate,
                    objData.Status,
                    BrandId = objData.CampaignBrandId
                });
                 objAudit.Text = "Existing GC  has been edited by user. Edited VoucherId=" + objData.Id;

            }
            else if (objData.Id > 0 && objData.Action.ToString().ToLower() == "validate")
            {
                string ValidateCertificateData = DBStrings.ValidateCertificateData;
                DateTime Created = DateTime.Now;
                DateTime IssueDate = DateTime.Now;

                var result = db.Execute(ValidateCertificateData, new
                {
                    
                    objData.Status,
                    objData.Id
                });
                objAudit.Text = "Existing GC  has been promoted by user. Edited VoucherId=" + objData.Id;

            }

            else
            {

                string insertCertificateData = DBStrings.InsertCertificateData;



                #region Get Campaign Category Prefix
                String GetCategoryPrefix = DBStrings.GetCategoryPrefix;
                String GetCategoryPrefixByCategoryId = DBStrings.GetCategoryPrefixByCategoryId;
                String CouponPrefix = "";

                CouponPrefix = db.Query<String>(GetCategoryPrefixByCategoryId, new
                {
                    CategoryId = objData.CouponCatId
                }).Single();

                #endregion

                #region Coupon Types
                string GetTypeDescById = DBStrings.GetCouponTypeById;
                string TypeDesc = db.Query<String>(GetTypeDescById, new
                {
                    TypeId = objData.CouponTypeId
                }).Single();
                #endregion

                #region Coupon Prefix for Code
                String NewPrefix = CouponPrefix.Trim();

                #endregion

                #region Pad Camapign Id with zero
                Int32 Length = Convert.ToInt32(objData.CampaignId.ToString().Length);
                Int32 DigitToAppend = 5;
                String PCampId;
                if (Length != 0)
                {
                    PCampId = objData.CampaignId.ToString();
                }
                else
                {
                    PCampId = "00000";
                }
                while (Length < DigitToAppend)
                {
                    PCampId = "0" + PCampId;
                    Length++;
                }
                #endregion




                #region checkifcodeexists
                String NewCertificateCode = "";
                String Exist = "0";

                while (NewCertificateCode == "")
                {
                    String GetCertificate = DBStrings.GetCertificateCode;
                    //Exist = db.Query<String>(GetCertificate, new { NewCertificateCode = NewCertificateCode }).DefaultIfEmpty().ToString();
                    Boolean NotExist = true;
                    while (NewCertificateCode == "" || Exist == "1")
                    {
                        byte[] gb = Guid.NewGuid().ToByteArray();


                        long l = 1;
                        foreach (byte b in Guid.NewGuid().ToByteArray())
                        {
                            l *= ((int)b + 1);
                        }
                        // NewCertificateCode = TypeDesc + NewPrefix + PCampId + l.ToString().Replace("-", "").Substring(1, 6);

                        String RandomNumb = l.ToString().Replace("-", "").Substring(1, 8);
                        Int32 INPrefix; Int32 IRandomNum;
                        Int32.TryParse(NewPrefix, out INPrefix);
                        Int32.TryParse(RandomNumb, out IRandomNum);
                        String DigitCode = (((INPrefix + IRandomNum) / INPrefix) % 100).ToString();
                        NewCertificateCode = NewPrefix + RandomNumb + DigitCode ; 


                        String InsertCertificate = DBStrings.InsertCertificate;
                        var data = db.Query<String>(InsertCertificate, new { NewCertificateCode = NewCertificateCode }).Single();
                        NotExist = data == "Repeat" ? true : false;
                    }
                }
                #endregion



                DateTime Created = DateTime.Now;
                DateTime IssueDate = DateTime.Now;

                var result = db.Execute(insertCertificateData, new
                {
                    TypeId = objData.CouponTypeId,
                    CategoryId = objData.CouponCatId,

                    objData.CampaignId,
                    NewCertificateCode,
                    objData.FirstName,
                    objData.LastName,
                    objData.CertificateAmount,
                    objData.CertificateValue,
                    IncProduct = objData.InclProduct,
                    objData.ExclProduct,
                    IssueDate,
                    objData.ExpiryDate,
                    objData.Status,
                    Created,
                    CampaignBrandId = objData.CampaignBrandId
                });
            }
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON("Success");
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }
    }

    public static string GetCertificateById(Int32 CertificateId)
    {
        try
        {
            String GetCertificateById = DBStrings.GetCertificateCodeById;
            var data = (List<GiftCertificate>)db.Query<GiftCertificate>(GetCertificateById, new { CertificateId = CertificateId });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }

    }

    public static string DeleteCertificatebyId(Int32 CertificateId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteCertificatebyId";
        objAudit.Text = "Existing Certificate has been deleted by user. Deleted CertificateID=" + CertificateId;
        #endregion

        try
        {
            String DeleteCertificate = DBStrings.DeleteCertificate;
            db.Query<String>(DeleteCertificate, new { CertificateId = CertificateId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }


    }


    //Get Compaign data to bind dropdown
    public static string GetCertificateCampaignData()
    {
        String GetCertificateCampaignData = DBStrings.GetCertificateCampaignData;
        var data = (List<GiftCertificate>)db.Query<GiftCertificate>(GetCertificateCampaignData);
        return getJSON(data);
    }

    #endregion

    #region Global Method
    public static void Initialize()
    {
        db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    }

    
    public static void InsertAuditLog(Audit objAudit)
    {
        try
        {
            Initialize();
           
            Int32 UserId = -1;
            Int32.TryParse(HttpContext.Current.Session["UserId"].ToString(), out UserId);
            objAudit.User = UserId;
            objAudit.TimeStamp = DateTime.Now;
            String InsertAudit = DBStrings.InsertAudit;
            db.Query<String>(InsertAudit, new { User = objAudit.User,Function = objAudit.Function,Text = objAudit.Text,TimeStamp = objAudit.TimeStamp,Status = objAudit.Status }).DefaultIfEmpty().ToString();
            
        }
        catch(Exception ex)
        {
            throw ex;
        }

    }

    public class Audit
    {
     public   Int32 User {get;set;}
     public String Function { get; set; }
     public String Text { get; set; }
     public DateTime TimeStamp { get; set; }
     public String Status { get; set; }

    }
    #endregion

    #region User

    public static string DeleteuserbyId(Int32 UserId)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteUserbyId";
        objAudit.Text = "Existing user has been deleted by user. Deleted UserID=" + UserId;
        #endregion
        Initialize();
        try
        {
            String DeleteUser= DBStrings.DeleteUser;
            db.Query<String>(DeleteUser, new { UserId = UserId }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }


    }

    #endregion


    #region BlackOut Dates
    #region New Dev created on 19/12/2017
    //Task - API V 1.5 Changes
    public static string GetBlackOutdates()
    {
        Initialize();
        String GetBlackOutdates = DBStrings.GetBlackOutdates;
        var data = (List<BDates>)db.Query<BDates>(GetBlackOutdates);
        return getJSON(data);
    }



    public static string InsertBlackoutDates(BDates objData)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "InsertBlackoutDates";
        objAudit.Text = "New voucher has been added by user";
        objAudit.TimeStamp = DateTime.Now;
        #endregion
        try
        {

            if (objData.Id > 0 && objData.Action.ToString().ToLower() == "edit")
            {
                string EditBlackOutData = DBStrings.EditBlackOutData;
                var result = db.Execute(EditBlackOutData, new
                {
                    Id = objData.Id,
                    objData.Name,
                    objData.StartDate,
                    objData.EndDate,
                    objData.Active

                });
                objAudit.Text = "Existing voucher  has been edited by user. Edited VoucherId=" + objData.Id;

            }

            else if (objData.Id > 0 && objData.Action.ToString().ToLower() == "validate")
            {
                string ValidateBlackOutDatesData = DBStrings.ValidateBlackOutDatesData;
                var result = db.Execute(ValidateBlackOutDatesData, new
                {
                    objData.Active,
                    Id = objData.Id,
                });
                objAudit.Text = "Existing Black Out Dates has been approved by user. Edited VoucherId=" + objData.Id;

            }
            else
            {

                string insertBlackOutDatesData = DBStrings.InsertBlackOutDatesData;


                DateTime Created = DateTime.Now;
                DateTime IssueDate = DateTime.Now;

                var result = db.Execute(insertBlackOutDatesData, new
                {


                    objData.Name,
                    objData.StartDate,
                    objData.EndDate,
                    objData.Active,
                    Created
                });

            }




            objAudit.Status = "Success";
            InsertAuditLog(objAudit);
            return getJSON("Success");
        }

        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);
            return getJSON("Failure");

        }
    }

    public static string GetBDatesById(Int32 Id)
    {
        try
        {
            String GetBDatesById = DBStrings.GetBDatesById;
            var data = (List<BDates>)db.Query<BDates>(GetBDatesById, new { Id = Id });
            return getJSON(data);
        }
        catch (Exception ex)
        {
            return getJSON("Failure");

        }

    }
    public static string DeleteBDatesbyId(Int32 Id)
    {
        #region Audit
        Audit objAudit = new Audit();
        objAudit.User = -1;
        objAudit.Function = "DeleteBDatesbyId";
        objAudit.Text = "Existing dates has been deleted by user. Deleted Date ID=" + Id;
        #endregion

        try
        {
            String DeleteBDatesbyId = DBStrings.DeleteBDatesbyId;
            db.Query<String>(DeleteBDatesbyId, new { Id = Id }).DefaultIfEmpty().ToString();
            objAudit.Status = "Success";
            InsertAuditLog(objAudit);

            return getJSON("Success");
        }
        catch (Exception ex)
        {
            objAudit.Status = ex.Message.ToString();
            InsertAuditLog(objAudit);

            return getJSON("Failure");

        }


    }


 
    #endregion

    #endregion
}