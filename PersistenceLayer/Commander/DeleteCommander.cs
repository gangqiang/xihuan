using System;
using System.Data;
//using System.Data.OleDb;

namespace PersistenceLayer
{
	/// <summary>
	/// DeleteCommander 的摘要说明。
	/// </summary>
	class DeleteCommander:SqlCommander
	{
		private string _deleteClause;

		public DeleteCommander(ClassMap cm):base(cm)
		{
			this.GetSqlDelete();
		}
		
		//生成 实体对象 对应的DeleteCommand
		public override IDbCommand BuildForObject(EntityObject obj)
		{
            return BuildForObject(obj, false);
		}

        public override IDbCommand BuildForObject(EntityObject obj,bool WithNoLock)
        {
            AttributeMap attrMap;
            IDbCommand cmd = clsMap.RelationalDatabase.GetCommand();

            cmd.CommandText = this.SqlString + this.partForObject;
            for (int i = 0; i < this.ThisClassMap.GetKeySize(); i++)
            {
                attrMap = this.ThisClassMap.GetKeyAttributeMap(i);
                //该列是否Primary Key
                if (attrMap.Column.KeyType == ColumnKeyTypes.PrimaryKey)
                {
                    IDataParameter p = cmd.CreateParameter();
                    //p.SourceColumn=attrMap.Column.GetFullyQualifiedName();					//列名
                    p.DbType = attrMap.Column.Type;											//列类型
                    p.Value = obj.GetAttributeValue(attrMap.Name);							//列值
                    if (this.ThisClassMap.RelationalDatabase.Vendor == DatabaseVendor.MySql)
                        p.ParameterName = "?" + attrMap.Name;
                    else
                        p.ParameterName = "@" + attrMap.Name;

                    cmd.Parameters.Add(p);
                }
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
                p.DbType = attrMap.Column.Type;										//列类型				
                p.Value = obj.GetAttributeValue(attrMap.Name);							//列值
                cmd.Parameters.Add(p);
            }

            return cmd;
        }
		
		//构造Delete Sql语句
		private void GetSqlDelete()
		{
			//Add 'DELETE FROM' clause to the select statement
			this.AddSqlClause("DELETE FROM ");
			this.AddSqlClause(ThisClassMap.RelationalDatabase.QuotationMarksStart+ ThisClassMap.GetAttributeMap(0).Column.Table.Name+ThisClassMap.RelationalDatabase.QuotationMarksEnd);
			//Add 'WHERE key= "some value"' to the select statement

			this._deleteClause=this.sql;

			this.AddSqlClause(" WHERE 1=1 ");
			AttributeMap attrMap;
			ClassMap thisClassMap=this.ThisClassMap;

            for(int i=0;i<thisClassMap.GetKeySize();i++)
			{
				attrMap=thisClassMap.GetKeyAttributeMap(i);
				//该列是否Primary Key
				if (attrMap.Column.KeyType==ColumnKeyTypes.PrimaryKey)
				{
					this.partForObject+=" AND "+this.clsMap.GetFullyQualifiedName(attrMap.Column.Name)+"="+
						this.clsMap.RelationalDatabase.GetStringParameter (attrMap.Name,i);
				}
			}

			//tintown add by 2005-3-21 添加timestamp功能
			if(ThisClassMap.TimestampAttribute!=null)
			{

				this.AddSqlClause(" AND "+this.clsMap.GetFullyQualifiedName(ThisClassMap.TimestampAttribute.Column.Name)+"=");
				this.AddSqlClause(ThisClassMap.RelationalDatabase.GetStringParameter(ThisClassMap.TimestampAttribute.Column.Name,ThisClassMap.GetKeySize()));
			}
		}


		public string DeleteClause 
		{
			get {return this._deleteClause;}
		}
	
	
	}
}
