﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.ResultResponse;

namespace Z.NetWiki.Application.ArticleModule.BlogServer.Dto
{
    public class ArticlePageQueryInput : Pagination
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 栏目ID
        /// </summary>
        public Guid? CategoryId { get; set; }
    }
}