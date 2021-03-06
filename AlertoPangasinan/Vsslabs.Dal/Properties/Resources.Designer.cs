﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vsslabs.Dal.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Vsslabs.Dal.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE [AccessControl] WHERE [UserId] = @UserId.
        /// </summary>
        internal static string AccessControl_DeleteByUserId {
            get {
                return ResourceManager.GetString("AccessControl_DeleteByUserId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE [AccessControl] SET [RoleId] = @RoleId WHERE [UserId] = @UserId.
        /// </summary>
        internal static string AccessControl_UpdateRoleByUserId {
            get {
                return ResourceManager.GetString("AccessControl_UpdateRoleByUserId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT DISTINCT dbo.Menu.Id, dbo.Menu.ModuleName, dbo.Menu.ParentId, dbo.Menu.DisplayName, dbo.Menu.Controller, dbo.Menu.Action, dbo.Menu.Area, dbo.Menu.Icon
        ///FROM            dbo.Users INNER JOIN
        ///                         dbo.AccessControl ON dbo.Users.Id = dbo.AccessControl.UserId INNER JOIN
        ///                         dbo.Roles INNER JOIN
        ///                         dbo.RolePermissions INNER JOIN
        ///                         dbo.Permissions ON dbo.RolePermissions.PermissionId = dbo.Permissions.Id ON dbo.Roles.Id [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Menu_GetAllByUserId {
            get {
                return ResourceManager.GetString("Menu_GetAllByUserId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 1 FROM [Roles] WHERE ID &lt;&gt; @Id AND RoleName = @Name.
        /// </summary>
        internal static string Role_IsUnique {
            get {
                return ResourceManager.GetString("Role_IsUnique", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE [RolePermissions] WHERE [RoleId] = @RoleId.
        /// </summary>
        internal static string RolePermissions_DeleteByRoleId {
            get {
                return ResourceManager.GetString("RolePermissions_DeleteByRoleId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Roles r WHERE r.Id = @RoleId
        ///SELECT * FROM RolePermissions rp WHERE rp.RoleId = @RoleId.
        /// </summary>
        internal static string Roles_GetById {
            get {
                return ResourceManager.GetString("Roles_GetById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Roles WHERE RoleName LIKE @Filter OR [Description] LIKE @Filter.
        /// </summary>
        internal static string Roles_Search {
            get {
                return ResourceManager.GetString("Roles_Search", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT COUNT(*) FROM Roles WHERE RoleName LIKE @Filter OR [Description] LIKE @Filter.
        /// </summary>
        internal static string Roles_Search_Count {
            get {
                return ResourceManager.GetString("Roles_Search_Count", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE [Users] WHERE [TenantId] = @TenantId.
        /// </summary>
        internal static string Users_DeleteByTenantId {
            get {
                return ResourceManager.GetString("Users_DeleteByTenantId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT	dbo.Permissions.*
        ///FROM	dbo.RolePermissions INNER JOIN
        ///		dbo.Roles ON dbo.RolePermissions.RoleId = dbo.Roles.Id INNER JOIN
        ///		dbo.Permissions ON dbo.RolePermissions.PermissionId = dbo.Permissions.Id INNER JOIN
        ///		dbo.AccessControl ON dbo.Roles.Id = dbo.AccessControl.RoleId
        ///WHERE dbo.AccessControl.UserId = @UserId.
        /// </summary>
        internal static string Users_GetUserPermissions {
            get {
                return ResourceManager.GetString("Users_GetUserPermissions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Users WHERE ([FirstName] LIKE @filter OR [Lastname] LIKE @filter OR [Email] LIKE @filter OR [UserName] LIKE @filter OR [Address] LIKE @filter OR [Country] LIKE @filter OR [MobileNo] LIKE @filter OR [PhoneNo] LIKE @filter).
        /// </summary>
        internal static string Users_Search {
            get {
                return ResourceManager.GetString("Users_Search", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT COUNT(*) FROM [dbo].[Users] WHERE ([FirstName] LIKE @filter OR [Lastname] LIKE @filter OR [Email] LIKE @filter OR [UserName] LIKE @filter OR [Address] LIKE @filter OR [Country] LIKE @filter OR [MobileNo] LIKE @filter OR [PhoneNo] LIKE @filter).
        /// </summary>
        internal static string Users_Search_Count {
            get {
                return ResourceManager.GetString("Users_Search_Count", resourceCulture);
            }
        }
    }
}
