using System;
using Microsoft.Xrm.Sdk;
using System.Text.RegularExpressions;

namespace D365PackageProjectJeff
{
    public class PreOperationFormatFirstNamePhoneCreateUpdate: IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (!context.InputParameters.ContainsKey("Target"))
                throw new InvalidPluginExecutionException("No target found");


            var entity = context.InputParameters["Target"] as Entity;

            if (entity.Attributes.Contains("telephone1"))
            {
                string phoneNumber = (string)entity["telephone1"];
                var formattedNumber = Regex.Replace(phoneNumber, @"[^\d]", "");
                entity["telephone1"] = formattedNumber;
            }

            if (entity.Attributes.Contains("firstname"))
            {
                string firstname = (string)entity["firstname"];
                string formattedName = firstname[0].ToString().ToUpper() + firstname.Substring(1);
                entity["firstname"] = formattedName;
            }
        }
    }
}
