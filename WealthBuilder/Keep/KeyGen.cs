//Copyright 2017 McMillan Financial Solutions, LLC.  All rights reserved.
using System;
using System.Reflection;

namespace WealthBuilder
{
    public static class KeyGen
    {
        public static bool IsLicenseValid()
        {
            AppExecution.Trace(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name);

            //Contact the Keygen license server to see if the license is valid. If call to server fails, allow the user to move forward.
            try
            {
                //Call to KeyGen server is disabled for now.
                //var client = new RestClient("https://api.keygen.sh/v1/accounts/" + Constants.KeyGenAccountId);
                //var request = new RestRequest("licenses/actions/validate-key", Method.POST);
                //request.AddHeader("Content-Type", "application/vnd.api+json");
                //request.AddHeader("Accept", "application/vnd.api+json");
                //string values = AppSettings.License;
                //request.AddJsonBody(new {meta = new {key = AppSettings.License}});
                //var response = client.Execute<Dictionary<string, object>>(request);
                //var meta = (Dictionary<string, object>)response.Data["meta"];
                //var data = (Dictionary<string, object>)response.Data["data"];
                //if (data == null || data["relationships"] == null) return false;
                //if (meta == null || meta["valid"] == null) return false;
                //var relationships = (Dictionary<string, object>)data["relationships"];
                //var policy = (Dictionary<string, object>)relationships["policy"];
                //var policyData = (Dictionary<string, object>)policy["data"];
                //string policyId = (string)policyData["id"];
                //if (policyId==Constants.WealthBuilderPolicyId) AppSettings.LicenseType = "WealthBuilder";
                //if (policyId == Constants.PennyPincherPolicyId) AppSettings.LicenseType = "PennyPincher";
                AppSettings.LicenseType = "PennyPincher";    //"WealthBuilder";
                AppSettings.Update();
                //bool ProductActivated = ((bool)meta["valid"]);
                //return ProductActivated;
                return true;
            }
            catch (Exception ex)
            {
                Error.Log("KeyGen", "IsLicenseValid", ex);
                return true;
            }
        }
    } 
}