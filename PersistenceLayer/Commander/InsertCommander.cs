using System;
using System.Data;
//using System.Data.OleDb;

namespace PersistenceLayer
{
	/// <summary>
	/// InsertCommander封装构造Insert Command的功能
	/// </summary>
	class InsertCommander:SqlCommander
	{
		public InsertCommander(ClassMap cm):base(cm)
		{
			this.GetSqlInsert();
		}

		//生成 实体对象 对应的InsertCommand
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
                if (attrMap.Column.IsAutoValue) continue;
                IDataParameter p = cmd.CreateParameter();
                //p.SourceColumn=attrMap.Column.GetFullyQualifiedName();				//列名	
                if (this.clsMap.RelationalDatabase.Vendor == DatabaseVendor.MySql)
                    p.ParameterName = "?" + attrMap.Name;
                else
                    p.ParameterName = "@" + attrMap.Name;
                p.DbType = attrMap.Column.Type;										//列类型

                tmp = obj.GetAttributeValue(attrMap.Name);							//列值
                if (tmp == null || (tmp is DateTime && (DateTime)tmp == DateTime.MinValue))
                {
                    p.Value = DBNull.Value;
                }
                else
                {
                    p.Value = tmp;
                }

                //mif (p is OleDbParameter)
                cmd.Parameters.Add(p);
            }

            //tintown added at 2005-3-22 更新timestamp的值
            if (ThisClassMap.TimestampAttribute != null)
            {
                attrMap = ThisClassMap.TimestampAttribute;
                IDataParameter p = cmd.CreateParameter();
                //p.SourceColumn=attrMap.Column.GetFullyQualifiedName();					//列名												//列名
                if (this.clsMap.RelationalDatabase.Vendor == DatabaseVendor.MySql)
                    p.ParameterName = "?" + attrMap.Name;
                else
                    p.ParameterName = "@" + attrMap.Name;
                p.DbType = attrMap.Column.Type;										//列类型				
                p.Value = DateTime.Now.Ticks;	   			//此timestamp由now的ticks生成一串数字
                cmd.Parameters.Add(p);
            }

            return cmd;
        }

		//构造Sql语句前部分
		private void GetSqlInsert()
		{
			this.AddSqlClause("INSERT INTO ");
			this.AddSqlClause(ThisClassMap.RelationalDatabase.QuotationMarksStart+ThisClassMap.GetAttributeMap(0).Column.Table.Name+ThisClassMap.RelationalDatabase.QuotationMarksEnd);
			this.AddSqlClause(" ");
			string sqlParas="";

			//Add clauses for all attributes
			AttributeMap am;
			bool isFirst=true;
			this.AddSqlClause("(");
			int size = ThisClassMap.GetSize ();
			for(int i=0;i< size;i++)
			{
				am=ThisClassMap.GetAttributeMap(i);
				if (am.Column.IsAutoValue)continue;
				
				if (isFirst)
				{
					this.AddSqlClause(clsMap.RelationalDatabase.GetQuotationColumn(am.Column.Name));
					//sqlParas="?";
					sqlParas = this.clsMap.RelationalDatabase.GetStringParameter(am.Name,i);
				}
				else
				{
					this.AddSqlClause(",");
					this.AddSqlClause(clsMap.RelationalDatabase.GetQuotationColumn(am.Column.Name));
					//sqlParas+=",?";
					sqlParas += "," + this.clsMap.RelationalDatabase.GetStringParameter(am.Name,i);

				}
				isFirst=false;
 			}

			//tintown added at 2005 -3-22 添加对timestamp的添加
			if(ThisClassMap.TimestampAttribute!=null)
			{
				if (!isFirst)
				{
					this.AddSqlClause(",");
					sqlParas+=",";
				}
				this.AddSqlClause(clsMap.RelationalDatabase.GetQuotationColumn(ThisClassMap.TimestampAttribute.Column.Name));
				//sqlParas+=",?";
				sqlParas +=this.clsMap.RelationalDatabase.GetStringParameter(ThisClassMap.TimestampAttribute.Name,size);
			}


			this.AddSqlClause(")");
			//Add clause "VALUES" to the insertStatement
			this.AddSqlClause (" VALUES (");
			this.AddSqlClause(sqlParas);
			this.AddSqlClause(")");
		}

		
	}


}
