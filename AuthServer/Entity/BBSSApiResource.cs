using BBSS.Platform.Core.Entity;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Entity
{
    public class BBSSApiResource : IEntity<int>
    {
        public int Id { get; set; }
        public string Secret { get; set; }
        /// <summary>
        /// 表示唯一的资源名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 多个Scope用逗号连接
        /// </summary>
        public string Scope { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 表示资源是否可用
        /// </summary>
        public bool Enabled { get; set; }

        internal ApiResource ToApiResouce()
        {
            if (null == this) return null;

            return new ApiResource(Name, DisplayName);
        }

        /// <summary>
        /// 资源的展示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 资源包含的用户申明信息
        /// </summary>
        public string UserClaims { get; set; }
    }
}
