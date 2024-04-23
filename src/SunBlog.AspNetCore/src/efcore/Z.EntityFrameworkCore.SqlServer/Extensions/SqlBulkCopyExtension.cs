using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Z.EntityFrameworkCore.SqlServer.Extensions;

public class SqlBulkCopyHelper
{
    public static SqlBulkCopy GetSqlBulkCopy(string sqlConnection, Action<BulkConfig>? bulkAction = null)
    {
        BulkConfig bulkConfig = new();
        bulkAction?.Invoke(bulkConfig);
        var sqlBulkCopy = new SqlBulkCopy(sqlConnection, bulkConfig.SqlBulkCopyOptions);
        sqlBulkCopy.DestinationTableName = bulkConfig.FullTableName;
        sqlBulkCopy.BulkCopyTimeout = bulkConfig.BulkCopyTimeout ?? sqlBulkCopy.BulkCopyTimeout;
        sqlBulkCopy.BatchSize = bulkConfig.BatchSize;
        return sqlBulkCopy;
    }

    /// <summary>
    /// Common logic for two versions of GetDataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <param name="type"></param>
    /// <param name="entities"></param>
    /// <param name="tableInfo"></param>
    /// <returns></returns>
    public static DataTable InnerGetDataTable<T>(DbContext context, IList<T> entities)
    {
        var dataTable = new DataTable();
        var columnsDict = new Dictionary<string, object?>();
        var ownedEntitiesMappedProperties = new HashSet<string>();

        var type = entities[0]!.GetType();
        var entityType = context.Model.FindEntityType(type) ?? throw new ArgumentException($"Unable to determine entity type from given type - {type.Name}");
        var entityTypeProperties = entityType.GetProperties();
        
        var entityNavigationOwnedDict = entityType.GetNavigations().Where(a => a.TargetEntityType.IsOwned()).ToDictionary(a => a.Name, a => a);
        var entityShadowFkPropertiesDict = entityTypeProperties.Where(a => a.IsShadowProperty() &&
                                                                           a.IsForeignKey() &&
                                                                           a.GetContainingForeignKeys().FirstOrDefault()?.DependentToPrincipal?.Name != null)
                                                                     .ToDictionary(x => x.GetContainingForeignKeys()?.First()?.DependentToPrincipal?.Name ?? string.Empty, a => a);

        var entityShadowFkPropertyColumnNamesDict = entityShadowFkPropertiesDict.ToDictionary(a => a.Key, a => a.Value.GetColumnName());
        
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var property in properties)
        {
            dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);

        }
        foreach (T entity in entities)
        {
            var propertiesToLoad = properties
                .Where(a => entityShadowFkPropertiesDict.ContainsKey(a.Name)); // omit virtual Navigation (except Owned and ShadowNavig.) since it's Getter can cause unwanted Select-s from Db

            foreach (var property in propertiesToLoad)
            {
                //columnsDict[property.Name] = property[property.Name];
            }

            var record = columnsDict.Values.ToArray();
            dataTable.Rows.Add(record);
        }

        return dataTable;
    }

}
