using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace Z.EntityFrameworkCore;

/// <summary>
/// Provides configration for EFCore BulkExtensions
/// </summary>
public class BulkConfig
{

    /// <summary>
    ///     Enum with [Flags] attribute which enables specifying one or more options.
    /// </summary>
    /// <value>
    ///     <c>Default, KeepIdentity, CheckConstraints, TableLock, KeepNulls, FireTriggers, UseInternalTransaction</c>
    /// </value>
    public SqlBulkCopyOptions SqlBulkCopyOptions { get; set; } = SqlBulkCopyOptions.Default;

    public string? FullTableName { get; set; }
    public int BatchSize { get; set; } = 2000;
    public int? BulkCopyTimeout { get; set; }
}
