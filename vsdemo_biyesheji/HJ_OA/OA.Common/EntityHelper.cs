using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace OA.Common
{
    public static class EntityHelper
    {
        public static T ToEntity<T>(this DataRow row) where T : new()
        {
            if (row == null) throw new ArgumentNullException("row");

            return SetValue(new T(), row, row.Table.Columns);
        }

        /// <summary>
        /// 转换为指定对象的List列表（实体对象属性名称与DataTable的ColumnName名称一致）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> ToEntity<T>(this DataTable table) where T : new()
        {
            if (table == null) throw new ArgumentNullException("table");

            IList<T> returnValue = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                returnValue.Add(SetValue(new T(), row, table.Columns));
            }
            return returnValue;
        }

        static T SetValue<T>(T entity, DataRow row, DataColumnCollection columns)
        {
            foreach (DataColumn col in columns)
            {
                if (row[col.ColumnName] == DBNull.Value)
                    continue;

                var property = entity.GetType().GetProperty(col.ColumnName);
                if (property != null)
                {
                    if (property.CanWrite)
                    {
                        if (property.PropertyType.IsEnum)
                        {
                            object enumName = Enum.ToObject(property.PropertyType, row[col.ColumnName]);
                            property.SetValue(entity, enumName, null);
                        }
                        else
                        {
                            switch (property.PropertyType.ToString())
                            {
                                case "System.String":
                                    property.SetValue(entity, Convert.ToString(row[col.ColumnName]), null);
                                    break;
                                case "System.Int32":
                                    property.SetValue(entity, Convert.ToInt32(row[col.ColumnName]), null);
                                    break;
                                case "System.Int64":
                                    property.SetValue(entity, Convert.ToInt64(row[col.ColumnName]), null);
                                    break;
                                case "System.DateTime":
                                    property.SetValue(entity, Convert.ToDateTime(row[col.ColumnName]), null);
                                    break;
                                case "System.Boolean":
                                    property.SetValue(entity, Convert.ToBoolean(row[col.ColumnName]), null);
                                    break;
                                case "System.Double":
                                    property.SetValue(entity, Convert.ToDouble(row[col.ColumnName]), null);
                                    break;
                                case "System.Decimal":
                                    property.SetValue(entity, Convert.ToDecimal(row[col.ColumnName]), null);
                                    break;
                                default:
                                    property.SetValue(entity, row[col.ColumnName], null);
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    var field = entity.GetType().GetField(col.ColumnName);
                    if (field != null)
                    {
                        field.SetValue(entity, row[col.ColumnName]);
                    }
                }
            }
            return entity;
        }

       
    }
}
