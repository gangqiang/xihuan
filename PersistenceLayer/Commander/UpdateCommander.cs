using System;
using System.Data;
//using System.Data.OleDb;

namespace PersistenceLayer
{
	/// <summary>
	/// UpdateCommander 的摘要说明。
	/// </summary>
	class UpdateCommander:SqlCommander
	{
		public UpdateCommander(ClassMap cm):base(cm)
		{
			this.GetSqlUpdate();
		}

		//生成 实体对象 对应的UpdateCommand
		public override IDbCommand BuildForObject(EntityObject obj)
		{
            return BuildForObject(obj,false);
		}

        public override IDbCommand BuildForObject(EntityObject obj,bool WithNoLock)
        {
            AttributeMap attrMap;
            IDbCommand cmd = this.clsMap.RelationalDatabase.GetCommand();
            object tmp;

            cmd.CommandText = this.SqlString;
            int size = ThisClassMap.GetSize();
            for (int i = 0; i < size; i++)
            {
                attrMap = ThisClassMap.GetAttributeMap(i);
                if (attrMap.Column.KeyType == ColumnKeyTypes.PrimaryKey) continue;
                //tintown add at 2004-09-23
                //如果此列为自动增长,则在生成update语句时跳过
                if (attrMap.Column.IsAutoValue == true) continue;
                //tintown add end
                IDataParameter p = cmd.CreateParameter();
                //p.SourceColumn=attrMap.Column.GetFullyQualifiedName();					//列名
                if (this.ThisClassMap.RelationalDatabase.Vendor == DatabaseVendor.MySql)
                    p.ParameterName = "?" + attrMap.Name;
                else
                    p.ParameterName = "@" + attrMap.Name;

                p.DbType = attrMap.Column.Type;										//列类型
                tmp = obj.GetAttributeValue(attrMap.Name);								//列值
                if (tmp == null || (tmp is DateTime && (DateTime)tmp == DateTime.MinValue))
                {
                    p.Value = DBNull.Value;
                }
                else
                {
                    p.Value = tmp;
                }
                cmd.Parameters.Add(p);
            }

            //tintown added at 2005-3-22 更新timestamp的值
            if (ThisClassMap.TimestampAttribute != null)
            {
                attrMap = ThisClassMap.TimestampAttribute;
                IDataParameter p = cmd.CreateParameter();
                //p.SourceColumn=attrMap.Column.GetFullyQualifiedName();					//列名
                if (this.ThisClassMap.RelationalDatabase.Vendor == DatabaseVendor.MySql)
                    p.ParameterName = "?Update" + attrMap.Name;
                else
                    p.ParameterName = "@Update" + attrMap.Name;
                //p.ParameterName = "@Update" + attrMap.Name;
                p.DbType = attrMap.Column.Type;										//列类型				
                p.Value = DateTime.Now.Ticks;	   			//此timestamp由now的ticks生成一串数字
                cmd.Parameters.Add(p);
            }

            for (int i = 0; i < ThisClassMap.GetKeySize(); i++)
            {
                attrMap = ThisClassMap.GetKeyAttributeMap(i);
                IDataParameter p = cmd.CreateParameter();
                //p.SourceColumn=attrMap.Column.GetFullyQualifiedName();					//列名
                if (this.ThisClassMap.RelationalDatabase.Vendor == DatabaseVendor.MySql)
                    p.ParameterName = "?" + attrMap.Name;
                else
                    p.ParameterName = "@" + attrMap.Name;
                //p.ParameterName = "@" + attrMap.Name;
                p.DbType = attrMap.Column.Type;										//列类型				
                p.Value = obj.GetAttributeValue(attrMap.Name);							//列值
                cmd.Parameters.Add(p);
            }


            //tintown add by 2005-3-21 添加timestamp功能
            if (ThisClassMap.TimestampAttribute != null)
            {
                attrMap = ThisClassMap.TimestampAttribute;
                IDataParameter p = cmd.CreateParameter();
                //p.SourceColumn=attrMap.Column.GetFullyQualifiedName();					//列名
                if (this.ThisClassMap.RelationalDatabase.Vendor == DatabaseVendor.MySql)
                    p.ParameterName = "?" + attrMap.Name;
                else
                    p.ParameterName = "@" + attrMap.Name;
                //p.ParameterName = "@" + attrMap.Name;
                p.DbType = attrMap.Column.Type;										//列类型				
                p.Value = obj.GetAttributeValue(attrMap.Name);							//列值
                cmd.Parameters.Add(p);
            }
            return cmd;
        }
		//构造Update Sql语句
		private void GetSqlUpdate()
		{
			this.AddSqlClause("UPDATE ");
			this.AddSqlClause(ThisClassMap.RelationalDatabase.QuotationMarksStart+ThisClassMap.GetAttributeMap(0).Column.Table.Name+ThisClassMap.RelationalDatabase.QuotationMarksEnd);
			this.AddSqlClause(" ");
			//Add clauses for all attributes but key attributes
			this.AddSqlClause("SET ");
			AttributeMap attrMap;
			bool isFirst=true;
			for(int i=0;i<ThisClassMap.GetSize();i++)
			{
				attrMap=ThisClassMap.GetAttributeMap(i);
				if(attrMap.Column.KeyType==ColumnKeyTypes.PrimaryKey) continue;
				//add by tintown at 2004-09-23
				//对于自动增长的列,不进行update
				if(attrMap.Column.IsAutoValue==true) continue;
				//add by tintown end
				if (isFirst)
				{
					//this.AddSqlClause(ThisClassMap.GetAttributeMap(i).Column.GetFullyQualifiedName()+"=?");
					this.AddSqlClause(clsMap.GetFullyQualifiedName(ThisClassMap.GetAttributeMap(i).Column.Name)+"=");
					this.AddSqlClause(ThisClassMap.RelationalDatabase.GetStringParameter (attrMap.Name,i));
					isFirst=false;
				}
				else
				{
					this.AddSqlClause(",");
					//this.AddSqlClause(ThisClassMap.GetAttributeMap(i).Column.GetFullyQualifiedName()+"=?");
					this.AddSqlClause(clsMap.GetFullyQualifiedName(ThisClassMap.GetAttributeMap(i).Column.Name)+"=");
					this.AddSqlClause(ThisClassMap.RelationalDatabase.GetStringParameter (attrMap.Name,i));
				}
				
			}
			
			//tintown added at 2005-3-22 SQL语句中添加更新timestamp
			if(ThisClassMap.TimestampAttribute!=null)
			{
				this.AddSqlClause(",");
				this.AddSqlClause(clsMap.GetFullyQualifiedName(ThisClassMap.TimestampAttribute.Column.Name)+"=");
				this.AddSqlClause(ThisClassMap.RelationalDatabase.GetStringParameter("Update"+ThisClassMap.TimestampAttribute.Column.Name,ThisClassMap.GetSize()+1));
			}


			//Add clause "WHERE" to the insertStatement
			this.AddSqlClause(" WHERE 1=1 ");
			for(int i=0;i<ThisClassMap.GetKeySize();i++)
			{
				attrMap=ThisClassMap.GetKeyAttributeMap(i);
				//该列是否Primary Key
				if (attrMap.Column.KeyType==ColumnKeyTypes.PrimaryKey)
				{
					this.AddSqlClause(" AND "+clsMap.GetFullyQualifiedName(attrMap.Column.Name)+"=");
					this.AddSqlClause(clsMap.RelationalDatabase.GetStringParameter(attrMap.Name,ThisClassMap.GetSize()+i+1));
				}

				
			}

			//tintown add by 2005-3-21 添加timestamp功能
			if(ThisClassMap.TimestampAttribute!=null)
			{

				this.AddSqlClause(" AND "+clsMap.GetFullyQualifiedName(ThisClassMap.TimestampAttribute.Column.Name)+"=");
				this.AddSqlClause(ThisClassMap.RelationalDatabase.GetStringParameter(ThisClassMap.TimestampAttribute.Column.Name,ThisClassMap.GetKeySize()+ThisClassMap.GetSize()+2));
			}
			
		}
	}
}
