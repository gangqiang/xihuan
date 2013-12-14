using System;
using System.Collections;
using System.Data;

namespace PersistenceLayer
{
	/// <summary>
	///		SelectCommand 的摘要说明。
	/// </summary>
	class SelectCommander:SqlCommander
	{
		private ClassMap thisClassMap;						//保存ClassMap
		private string _stringForInherit = null;
		private string _selecFromClause;
		

		public SelectCommander(ClassMap cm):base(cm)
		{
			thisClassMap=cm;
			this.GetSqlSelect();
		}
		
		//构造Sql语句前部分
		private void GetSqlSelect()
		{
			this.AddSqlClause("SELECT ");
			//Add clauses for all attributes. Don't add "," before the first attribute
			bool isFirst=true;
			ClassMap clsMap=thisClassMap;
			AttributeMap attrMap ;
			int size;
			string stemp = " AS " + clsMap.RelationalDatabase.QuotationMarksStart;
			do
			{
				//attrMap = clsMap.GetAttributeMap (0);
				size = clsMap.SelfClassAttributes.Count;
				for(int i = 0;i < size;i ++)
				{
					attrMap = (AttributeMap)clsMap.SelfClassAttributes[i];
					this.AddSqlClause((isFirst?"":",")+ this.clsMap.GetFullyQualifiedName(attrMap.Column.Name));
					this.AddSqlClause( stemp + attrMap.Name + clsMap.RelationalDatabase.QuotationMarksEnd);
					isFirst=false;
				}
				clsMap=clsMap.SuperClass;
			}while(clsMap!=null);
	
			//Add the FROM clause to the statement
			this.AddSqlClause(" FROM " + ThisClassMap.RelationalDatabase.QuotationMarksStart+ thisClassMap.Table.Name+ThisClassMap.RelationalDatabase.QuotationMarksEnd) ;
         
            if (ThisClassMap.RelationalDatabase.Vendor == DatabaseVendor.MsAccess || ThisClassMap.RelationalDatabase.Vendor == DatabaseVendor.MsSqlServer)
            {
                this.AddSqlClause(" With(nolock)");
            }
            
			isFirst=true;
			clsMap = thisClassMap.SuperClass;
			while (clsMap != null)
			{
				this.AddSqlClause ("," + clsMap.Table.Name); 
				clsMap = clsMap.SuperClass;
			}while(clsMap!=null);
			//此语句为SELECT的前部
			this._selecFromClause = this.sql;



			//Add WHERE Clause to the statement
			this.AddSqlClause (" WHERE 1=1 ");
			for(int i=0;i<thisClassMap.GetKeySize();i++)
			{
				attrMap=thisClassMap.GetKeyAttributeMap(i);
				//该列是否Primary Key
				if (attrMap.Column.KeyType==ColumnKeyTypes.PrimaryKey)
				{
					this.AddSqlClause (" AND " + this.clsMap.GetFullyQualifiedName(attrMap.Column.Name) + "=" +
						base.clsMap.RelationalDatabase.GetStringParameter(attrMap.Name,i));
				}
			}
			
			//Add WHERE Clause table.column=superclass_table.column to the selectStatement
			clsMap = this.clsMap;
			isFirst = true;
			do
			{
				size = clsMap.GetReferenceSize ();
				for (int i = 0 ;i < size ;i ++)
				{
					attrMap = clsMap.GetReferenceAttributeMap (i);
					if (isFirst)
					{
						_stringForInherit = this.clsMap.GetFullyQualifiedName(attrMap.Column.Name) +
							"=" + this.clsMap.GetFullyQualifiedName(attrMap.Reference.Column.Name );
						isFirst = false;
					}
					else
					{
						_stringForInherit += " AND " + this.clsMap.GetFullyQualifiedName(attrMap.Column.Name) +
							"=" + this.clsMap.GetFullyQualifiedName(attrMap.Reference.Column.Name);
					}
				}
				if (this._stringForInherit != null)
				{
					this.AddSqlClause (" AND " + this._stringForInherit);
				}
				clsMap = clsMap.SuperClass;
			}while (clsMap != null);
		}
		

		//生成 实体对象 对应的SelectCommand
		public override IDbCommand BuildForObject(EntityObject obj)
		{
            return BuildForObject(obj, false);
		}

        public override IDbCommand BuildForObject(EntityObject obj,bool WithNoLock)
        {
            AttributeMap attrMap;
            IDbCommand cmd = thisClassMap.RelationalDatabase.GetCommand();

            cmd.CommandText = this.SqlString;
            //这里要判断是否是WithNoLock
            if (WithNoLock)
            {
                if (ThisClassMap.RelationalDatabase.Vendor == DatabaseVendor.MsAccess || ThisClassMap.RelationalDatabase.Vendor == DatabaseVendor.MsSqlServer)
                {

                    string sKey = " FROM " + ThisClassMap.RelationalDatabase.QuotationMarksStart + thisClassMap.Table.Name + ThisClassMap.RelationalDatabase.QuotationMarksEnd;
                    cmd.CommandText = cmd.CommandText.Replace(sKey, sKey + " with(nolock) ");
                }
            }
            int size = thisClassMap.GetKeySize();
            string strRealSql = cmd.CommandText;   //add by duhu
            for (int i = 0; i < size; i++)
            {
                attrMap = thisClassMap.GetKeyAttributeMap(i);
                //该列是否Primary Key
                if (attrMap.Column.KeyType == ColumnKeyTypes.PrimaryKey)
                {
                    IDataParameter p = cmd.CreateParameter();
                    if (this.ThisClassMap.RelationalDatabase.Vendor == DatabaseVendor.MySql)
                        p.ParameterName = "?" + attrMap.Name;
                    else
                        p.ParameterName = "@" + attrMap.Name;

                    p.DbType = attrMap.Column.Type;										//列类型
                    p.Value = obj.GetAttributeValue(attrMap.Name);							//列值
                    cmd.Parameters.Add(p);

                    strRealSql = strRealSql.Replace(p.ParameterName, p.Value.ToString());   //add by duhu
                }
                Logger.Write(strRealSql); // add by duhu
            }
            return cmd;
        }

		public IDbCommand BuildForSelectionCriteria (SelectionCriteria aSelectionCriteria)
		{
			string partOfCriteria = " WHERE 1=1 ";
			if (this.clsMap.SuperClass  != null)
			{
				partOfCriteria += " AND " + _stringForInherit;
			}
			partOfCriteria += " AND " + aSelectionCriteria.AsSqlClause ();
			IDbCommand cmd = clsMap.RelationalDatabase.GetCommand();
			cmd.CommandText = this._selecFromClause + partOfCriteria;
			return cmd;
		}


		public static string GetOrderSql(ArrayList orderList,ClassMap cm)
		{
			AttributeMap am;
			OrderEntry order;
			string orderString="";

			for(int i=0;i<orderList.Count;i++)
			{
				order=(OrderEntry)orderList[i];
				am=cm.GetAttributeMap(order.AttributeName);
				//如果是升序
				if(order.IsAscend)
				{
					orderString+=cm.GetFullyQualifiedName(am.Column.Name)+" ASC,";
				}
				else
				{
					orderString+=cm.GetFullyQualifiedName(am.Column.Name)+" DESC,";
				}
			}
			orderString=orderString.Substring(0,orderString.Length-1);
			return " ORDER BY " + orderString;		
		}

		public string SelectClause 
		{
			get {return this._selecFromClause;}
		}
		
		public string StringForInherit
		{
			get {return this._stringForInherit;}
		}

	}
}
