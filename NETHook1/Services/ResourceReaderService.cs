// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceReaderService.cs" company="TODO: Company Name">
//   Copyright (c) May 25, 2022 TODO: Company Name
// </copyright>
// <summary>
//  If this project is helpful please take a short survey at ->
//  http://ux.mastercam.com/Surveys/APISDKSupport 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NETHook1.Models;

using NETHookResources = NETHook1.Properties.Resources;

namespace NETHook1.Services
{
    /// <summary> The resource reader service. </summary>
    public class ResourceReaderService : SingletonBehaviour<ResourceReaderService>
    {
        /// <summary> Gets the resource string from our resources. </summary>
        ///
        /// <param name="name"> The resource name. </param>
        ///
        /// <returns> The value of the resource. </returns>
        public static Result<string> GetString(string name)
        {
            try
            {
                return Result.Ok(NETHookResources.ResourceManager.GetString(name));
            }
            catch
            {
                return Result.Fail<string>($"Missing resource {name} ");
            }
        }
    }
}